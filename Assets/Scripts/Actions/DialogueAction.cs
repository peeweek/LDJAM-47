using GameplayIngredients;
using GameplayIngredients.Actions;
using NaughtyAttributes;
using UnityEngine;

public class DialogueAction : ActionBase
{
    [ReorderableList]
    public Dialogue[] Dialogues;

    [ReorderableList]
    public Callable[] PostDialogue;

    public override void Execute(GameObject instigator = null)
    {
        Manager.Get<DialogueManager>().SetDialogue(Dialogues, PostDialogue, instigator);
    }
}
