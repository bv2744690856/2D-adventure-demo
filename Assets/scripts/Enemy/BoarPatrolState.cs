using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }


    public override void LogicUpdate()
    {
        //∑¢œ÷player,«–ªªµΩchase
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCstate.chase);
        }


        if (!currentEnemy.phsicsCheck.isGround||(currentEnemy.phsicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.phsicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            //transform.localScale = new Vector3(faceDir.x,1,1);
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }

   
    

    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
        
    }
}
