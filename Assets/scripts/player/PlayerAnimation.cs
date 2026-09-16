using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhsicsCheck phsicsCheck;
    private PlayerController playerController;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        phsicsCheck = GetComponent<PhsicsCheck>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    { 
        SetAnimation();
        
    }
    private void SetAnimation() 
    {
        
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", phsicsCheck.isGround);
        
        anim.SetBool("isDead", GetComponent<PlayerController>().isDead);
        anim.SetBool("isAttack", playerController.isAttack);
        
    }
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }

    
}
