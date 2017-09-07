using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/CanThrow")]
public class CanThrowDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        return controller.GetComponent<PlayerMovement>().canThrow;
    }
}
