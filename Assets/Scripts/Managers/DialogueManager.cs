using GameplayIngredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManagerDefaultPrefab("DialogueManager")]
public class DialogueManager : Manager
{
    public bool isPlayingDialogue { get; private set; }

    public GameObject AudioSFX;

    Coroutine routine;
    public void SetDialogue(Dialogue[] dialogues, Callable[] postDialogue, GameObject instigator = null)
    {
        if (routine != null)
        {
            Debug.LogWarning("Dialogue coroutine already running. Aborting.");
            StopCoroutine(routine);
        }

        routine = StartCoroutine(PlayDialogues(dialogues, postDialogue, instigator));
    }

    bool justClicked;

    private void Update()
    {
        justClicked = Input.GetMouseButtonDown(0);
    }

    IEnumerator PlayDialogues(Dialogue[] dialogues, Callable[] postDialogue, GameObject instigator = null)
    {
        isPlayingDialogue = true;

        foreach(var dialogue in dialogues)
        {
            if (dialogue.protagonist == Protagonist.Player)
            {
                Player.instance.BubbleScale = dialogue.BubbleScale;
                Player.instance.SetDialogueText(string.Empty);

                while (!Player.instance.StepOpenDialogue())
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                justClicked = false;
            }

            if (dialogue.lines != null)
            {
                foreach (var line in dialogue.lines)
                {
                    float count = line.Length;
                    float t = 0.0f;
                    AudioSFX.SetActive(true);
                    while (t <= count)
                    {
                        if (justClicked)
                        {
                            t = count + Time.deltaTime;
                            justClicked = false;
                        }

                        else
                            t += Time.deltaTime * 32;
                        string currentline = line.Substring(0, (int)t);

                        if (dialogue.protagonist == Protagonist.Player)
                            Player.instance.SetDialogueText(currentline);
                        else
                            Erica.instance.SetDialogueText(currentline);

                        yield return new WaitForEndOfFrame();
                    }

                    AudioSFX.SetActive(false);
                    while (!justClicked)
                    {
                        Manager.Get<CursorManager>().SetCursor(CursorType.Dialogue);
                        yield return new WaitForEndOfFrame();
                    }
                    justClicked = false;
                }
            }

            yield return new WaitForEndOfFrame();

            if (dialogue.protagonist == Protagonist.Player)
            {
                while (!Player.instance.StepCloseDialogue())
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                Erica.instance.SetDialogueText(string.Empty);
            }
        }

        isPlayingDialogue = false;
        routine = null;
        Callable.Call(postDialogue, instigator);
    }
}
public enum Protagonist
{
    Player,
    Erica
}

[System.Serializable]
public struct Dialogue
{
    public Protagonist protagonist;
    public Vector2 BubbleScale;
       [Multiline]
    public string[] lines;
}