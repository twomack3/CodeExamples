using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    * Performs the Move to and attack actions for computer controlled avatars
    * T.Womack 8-2017
    * 
    * Make use fo new Class AvatarProperties for aiming - TTW 9/2017
    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
    private PlayerMovement thisAIPlayer;
    private GameObject playersBall = null;
    private NavMeshAgent thisAIAgent;
    private ArenaMatchManager matchManager;

    public override void Act(PlayerStateController controller)
    {
        thisAIPlayer = controller.GetComponent<PlayerMovement>();
        playersBall = thisAIPlayer.myBall;
        //playerPosition = controller.GetComponent<Transform>().position;
        thisAIAgent = controller.playerNavMeshAgent;
        Attack(controller);
    }

    private void Attack(PlayerStateController controller)
    {
        GameObject enemy1 = null;
        GameObject enemy2 = null;
        GameObject closeEnemy = null;
        matchManager = FindObjectOfType<ArenaMatchManager>();
        //If not in attack zone move to attack zone
        if (!thisAIPlayer.inAttackArea)
        {
            thisAIAgent.destination = thisAIPlayer.myStart;
            thisAIAgent.stoppingDistance = 2.0f;
        }
        else
        {
            //Find closest enemy
            if (thisAIPlayer.redTeam)
            {
                //if (GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>().Alive)
                if (matchManager.player1.GetComponent<PlayerHealth>().Alive)
                {
                    enemy1 = matchManager.player1;
                }
                if (matchManager.player3.activeSelf && matchManager.player3.GetComponent<PlayerHealth>().Alive)
                {
                    enemy2 = matchManager.player3;
                }
            }
            else
            {
                if (matchManager.player2.GetComponent<PlayerHealth>().Alive)
                {
                    enemy1 = matchManager.player2;
                }
                if (matchManager.player4.activeSelf && matchManager.player4.GetComponent<PlayerHealth>().Alive)
                {
                    enemy2 = matchManager.player4;
                }
            }
            if (controller.stateTimeElapsed < thisAIPlayer.powerTime) //only attack at full power
            {
                thisAIPlayer.UpdatePlayerUI();
                return; //no enemies found do nothing more
            }
            // check all cases of available enemies
            if (enemy1 == null && enemy2 == null)
            {
                thisAIPlayer.UpdatePlayerUI();
                return; //no enemies found do nothing more
            }

            if (enemy2 != null && enemy1 != null)
            {
                if (Vector3.Distance(enemy1.transform.position, thisAIAgent.transform.position) <=
                    Vector3.Distance(enemy2.transform.position, thisAIAgent.transform.position))
                {
                    closeEnemy = enemy1;
                }
                else
                {
                    closeEnemy = enemy2;
                }
            }
            else if (enemy2 == null)
            {
                closeEnemy = enemy1;
            }
            else
            {
                closeEnemy = enemy2;
            }

            //Face and aim
            Vector3 faceDirection = closeEnemy.transform.position - thisAIAgent.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(faceDirection);
            thisAIAgent.transform.rotation = Quaternion.Lerp(thisAIAgent.transform.rotation, lookRotation, Time.deltaTime * thisAIPlayer.rotateSpeed);
            float angletoTarget = Vector3.Angle(faceDirection, thisAIPlayer.transform.forward);
            //Throw ball
            if (angletoTarget < 1.0f) // aiming at target
            {
                //Debug.Log("AA-A " + angletoTarget + " "+ playersBall);
                //throw ball - sound from avatar
                Rigidbody ballRB = playersBall.GetComponent<Rigidbody>();
                float ballImpulse = playersBall.GetComponent<CommonBallClass>().initialImpulse;
                ballRB.isKinematic = false;
                //holding += holdTime; //force time ring off
                float effort = ballImpulse;
                ballRB.constraints = RigidbodyConstraints.None;
                //Debug.Log ("AA_A throw ball " + playersBall +" " +effort);
                ballRB.velocity = thisAIPlayer.transform.forward * effort;
                thisAIPlayer.ReleaseBall();
                controller.stateTimeElapsed = 0.0f;
            }
        }
        //Update hold UI for player and drop ball if stateTime > hold time
        thisAIPlayer.powerClock.value = 100.0f * controller.stateTimeElapsed / thisAIPlayer.powerTime;
        thisAIPlayer.holdClock.value = 100.0f * (thisAIPlayer.holdTime - controller.stateTimeElapsed) / thisAIPlayer.holdTime;
        if (controller.stateTimeElapsed > thisAIPlayer.holdTime)
        {
            //Debug.Log("PM DropBall: " + holding + " " + holdTime);
            //Drop ball - move ball to start point
            Rigidbody ballRB = playersBall.GetComponent<Rigidbody>();
            ballRB.isKinematic = false;
            CommonBallClass theBall = playersBall.GetComponent<CommonBallClass>();
            theBall.BallState = ArenaMatchManager.BallStates.Inactive;
            thisAIPlayer.ReleaseBall();
            controller.stateTimeElapsed = 0.0f;
            //thisAIPlayer.UpdatePlayerUI();
        }
    }
}
