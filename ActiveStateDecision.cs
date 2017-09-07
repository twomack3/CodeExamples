using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(PlayerStateController controller)
    {
        //if (controller.stateTimeElapsed > controller.debugPrintTime)
        //{
        //    //Debug.Log("ASD " + controller +" "+ controller.playerAITransform.gameObject.activeSelf);
        //}
        return controller.playerAITransform.gameObject.activeSelf;
    }
}
