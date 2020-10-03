using GameplayIngredients.Actions;
using NaughtyAttributes;
using UnityEngine;

public class SetLayerAction : ActionBase
{
    [ReorderableList]
    public GameObject[] Targets;
    public string layer;

    public override void Execute(GameObject instigator = null)
    {
        if (Targets == null)
            return;

        foreach (var target in Targets)
        {
            if(target != null)
                target.layer = LayerMask.NameToLayer(layer);
        }
    }
}
