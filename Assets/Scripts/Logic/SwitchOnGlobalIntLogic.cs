using GameplayIngredients;
using GameplayIngredients.Logic;
using NaughtyAttributes;
using UnityEngine;

public class SwitchOnGlobalIntLogic : LogicBase
{
    public string GlobalIntName = "INTEGER";
    public Globals.Scope Scope = Globals.Scope.Global;


    [ReorderableList]
    public Callable[] Cases;
    
    [Header("Default Case")]
    public Callable DefaultCase;

    public override void Execute(GameObject instigator = null)
    {
        int value = Globals.GetInt(GlobalIntName, Scope);
        if (value >= 0 && value < Cases.Length && Cases[value] != null)
            Callable.Call(Cases[value], instigator);
        else if(DefaultCase != null)
            Callable.Call(DefaultCase, instigator);
    }
}
