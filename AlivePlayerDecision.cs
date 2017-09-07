using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AliveState")]
public class AlivePlayerDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        //if (controller.stateTimeElapsed > controller.debugPrintTime)
        //{
        //    //Debug.Log("APD " + controller.GetComponent<PlayerHealth>().Alive);
        //}
        return controller.GetComponent<PlayerHealth>().Alive;
    }
}
