using GameplayIngredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManagerDefaultPrefab("DialogueManager")]
public class DialogueManager : Manager
{
    public bool isPlayingDialogue { get; private set; }
    Coroutine routine;
    public void SetDialogue(Dialogue dialogue, Callable[] postDialogue, GameObject instigator = null)
    {
        if (routine != null)
        {
            Debug.LogWarning("Dialogue coroutine already running. Aborting.");
            StopCoroutine(routine);
        }

        routine = StartCoroutine(PlayDialogue(dialogue, postDialogue, instigator));
    }

    bool justClicked;

    private void Update()
    {
        justClicked = Input.GetMouseButtonDown(0);
    }

    IEnumerator PlayDialogue(Dialogue dialogue, Callable[] postDialogue, GameObject instigator = null)
    {
        isPlayingDialogue = true;

        if (dialogue.protagonist == Protagonist.Player)
        {
            Player.instance.SetDialogueText(string.Empty);

            while (!Player.instance.StepOpenDialogue())
            {
                yield return new WaitForEndOfFrame();
            }
        }

        if(dialogue.lines != null)
        {
            foreach (var line in dialogue.lines)
            {
                float count = line.Length;
                float t = 0.0f;

                while(t <= count)
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

                    yield return new WaitForEndOfFrame();
                }

                while (!justClicked)
                {
                    Manager.Get<CursorManager>().SetCursor(CursorType.Dialogue);
                    yield return new WaitForEndOfFrame();
                }
                justClicked = false;
            }
        }


        if (dialogue.protagonist == Protagonist.Player)
        {
            while (!Player.instance.StepCloseDialogue())
            {
                yield return new WaitForEndOfFrame();
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
       [Multiline]
    public string[] lines;
}