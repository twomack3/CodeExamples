using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GetBall")]
public class GetBallAction : Action
{
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    * Performs the Get ball action for computer controlled avatars
    * T.Womack 8-2017
    * 
    * Make use fo new Class AvatarProperties for 'grabbing' ball - TTW 9/2017
    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private PlayerStateController thisAIPlayer;
    private GameObject closestBall = null;
    private Vector3 position;
    //private ArenaMatchManager matchManager;

    public override void Act(PlayerStateController controller)
    {
        thisAIPlayer = controller;
        GetBall();
    }

    private void GetBall()
    {
        //Find Closest
        Vector3 nearBallPosition = new Vector3();
        //Find closest idle or inactive ball on the playing floor
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");
        float distance = Mathf.Infinity;
        position = thisAIPlayer.transform.position;
        foreach (GameObject ball in balls)
        {
            if ((ball.GetComponent<CommonBallClass>().BallState == ArenaMatchManager.BallStates.Idle ||
                 ball.GetComponent<CommonBallClass>().BallState == ArenaMatchManager.BallStates.Inactive) &&
                 (ball.transform.position.y > thisAIPlayer.surface))
            {
                float curDistance = Vector3.Distance(ball.transform.position, position);
                if (curDistance < distance)
                {
                    distance = curDistance;
                    closestBall = ball;
                    nearBallPosition = ball.transform.position;
                    //Debug.Log("GB " + (ArenaMatchManager.BallStates)ball.GetComponent<CommonBallClass>().BallState + " " +
                    //          ball.transform.position.y + " " + thisAIPlayer.surface + " " + curDistance + " " + distance);
                }
            }
        }
        if (closestBall == null)
        {
            //Debug.Log("GBA_GB No ball found");
            return;
        }
        //Move to closest ball
        thisAIPlayer.playerNavMeshAgent.destination = nearBallPosition;
        thisAIPlayer.playerNavMeshAgent.stoppingDistance = 0.22f;
        //Exit if not close enough
        if ((Vector3.Distance(nearBallPosition, position)) > thisAIPlayer.GetComponent<PlayerMovement>().grabDistance)
        {
            thisAIPlayer.GetComponent<PlayerMovement>().hasBall = false;
            return;  //no ball in range to grab/catch
        }
        //Put ball in hand
        closestBall.transform.SetParent(thisAIPlayer.GetComponent<PlayerMovement>().myRightHand);
        closestBall.GetComponent<Rigidbody>().isKinematic = true;
        closestBall.transform.localPosition = Vector3.zero;
        //Set ball status to player's color at this time
        CommonBallClass theBall = closestBall.GetComponent<CommonBallClass>();
        if (thisAIPlayer.GetComponent<PlayerMovement>().redTeam)
            theBall.BallState = ArenaMatchManager.BallStates.Red;
        else
            theBall.BallState = ArenaMatchManager.BallStates.Blue;

        thisAIPlayer.stateTimeElapsed = 0.0f;
        thisAIPlayer.GetComponent<PlayerMovement>().hasBall = true;
        thisAIPlayer.GetComponent<PlayerMovement>().myBall = closestBall;
        //Debug.Log("GBA-GB "+ (ArenaMatchManager.BallStates)closestBall.GetComponent<CommonBallClass>().BallState +" "+ closestBall);
    }
}
