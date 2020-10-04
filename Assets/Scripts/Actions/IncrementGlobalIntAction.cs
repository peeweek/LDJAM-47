using GameplayIngredients.Actions;
using UnityEngine;

public class IncrementGlobalIntAction : ActionBase
{
    public Globals.Scope Scope = Globals.Scope.Global;
    public string VariableName = "Integer";
    public int increment = 1;
    public override void Execute(GameObject instigator = null)
    {
        Globals.SetInt(VariableName, Globals.GetInt(VariableName, Scope) + increment, Scope);
    }
}
