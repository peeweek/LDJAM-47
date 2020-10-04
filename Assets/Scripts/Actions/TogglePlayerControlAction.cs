using GameplayIngredients.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerControlAction : ActionBase
{
    public bool ControlEnabled = true;

    public override void Execute(GameObject instigator = null)
    {
        Player.instance.SetControl(ControlEnabled);
    }
}
