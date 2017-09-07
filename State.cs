using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(PlayerStateController controller)
    {
        //Debug.Log("S-US " + controller);
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(PlayerStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(PlayerStateController controller)
    {
        //if (controller.stateTimeElapsed > controller.debugPrintTime)
        //{
        //    //Debug.Log("S-CT " + transitions.Length + " " + controller);
        //}
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            //if (controller.stateTimeElapsed > controller.debugPrintTime)
            //{
            //    //Debug.Log("S-CT " + i + " " + controller + decisionSucceeded);
            //}
            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
