﻿using GameplayIngredients.Rigs;
using NaughtyAttributes;
using UnityEngine;

namespace GameplayIngredients.Actions
{
    public class ReachPositionRigSetTargetAction : ActionBase
    {
        [NonNullCheck]
        public ReachPositionRig reachPositionRig;

        [DisableIf("UseInstigatorAsTarget"), NonNullCheck]
        public Transform target;
        
        public bool UseInstigatorAsTarget;

        public override void Execute(GameObject instigator = null)
        {
            if (reachPositionRig == null)
            {
                Debug.LogWarning($"{gameObject.name}: ReachPositionRigSetTarget action could not set target : ReachPositionRig is null");
                return;
            }

            if (UseInstigatorAsTarget)
            {
                if (instigator != null)
                    reachPositionRig.SetTarget(instigator.transform);
                else
                    Debug.LogWarning($"{gameObject.name}: ReachPositionRigSetTarget action could not set target : instigator is null");
            }
            else
            {
                if (target != null)
                    reachPositionRig.SetTarget(target);
                else
                    Debug.LogWarning($"{gameObject.name}: ReachPositionRigSetTarget action could not set target : Target is null");
            }
        }
    }
}

