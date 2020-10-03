using GameplayIngredients;
using GameplayIngredients.Actions;
using NaughtyAttributes;
using UnityEngine;

public class DialogueAction : ActionBase
{
    public Dialogue Dialogue;

    [ReorderableList]
    public Callable[] PostDialogue;

    public override void Execute(GameObject instigator = null)
    {
        Manager.Get<DialogueManager>().SetDialogue(Dialogue, PostDialogue, instigator);
    }
}
