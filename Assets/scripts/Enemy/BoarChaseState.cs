using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //Debug.Log("chase");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NPCstate.patrol);

        if (!currentEnemy.phsicsCheck.isGround || (currentEnemy.phsicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.phsicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            //transform.localScale = new Vector3(faceDir.x,1,1);
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x , 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run", false);
    }
}
