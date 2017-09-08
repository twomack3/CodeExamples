using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/HasBall")]
public class HasBallDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        return controller.GetComponent<PlayerMovement>().hasBall;
    }
}
