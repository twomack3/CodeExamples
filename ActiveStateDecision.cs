using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        return controller.playerAITransform.gameObject.activeSelf;
    }
}
