using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AliveState")]
public class AlivePlayerDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        return controller.GetComponent<PlayerHealth>().Alive;
    }
}
