using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    public SceneLoadEventSO senceLoadEvent;

    public VoidEventSO afterSceneLoadedEvent;

    public PlayerInputControl inputContronl;

    public VoidEventSO loadDataEvent;

    public VoidEventSO backToMenuEvent;
    //public Rigidbody2D rb;
    private Rigidbody2D rb;
    private PhsicsCheck phsicscheck;
    private CapsuleCollider2D coll;
    private PlayerAnimation playerAnimation; 
    public Vector2 inputDirection;
    
    [Header("基本参数")]
    public float hurtForce;
    public float jumpForce;
    public float speed;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        phsicscheck = GetComponent<PhsicsCheck>();
        inputContronl = new PlayerInputControl();
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        //跳跃
        inputContronl.Gameplay.Jump.started += jump;
        //攻击
        inputContronl.Gameplay.Attack.started += PlayerAttack;
    }

    

    private void OnEnable()
    {
        inputContronl.Enable();
        senceLoadEvent.LoadRequestEvent += onLordEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAftersceneLoadedEvent;
        loadDataEvent.OnEventRaised += OnloadDataEvent;
        backToMenuEvent.OnEventRaised += OnloadDataEvent;
    }

    

    private void OnDisable()
    {
        inputContronl.Disable();
        senceLoadEvent.LoadRequestEvent -= onLordEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAftersceneLoadedEvent;
        loadDataEvent.OnEventRaised -= OnloadDataEvent;
        backToMenuEvent.OnEventRaised -= OnloadDataEvent;

    }
    private void Update()
    {
        inputDirection = inputContronl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();

    }
    private void FixedUpdate()
    {
        if(!isHurt&&!isAttack!&&!isDead)
        Move();
    }

    //测试
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(other.name);
    //}

    //场景加载过程停止控制
    private void onLordEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputContronl.Gameplay.Disable();
    }

    private void OnloadDataEvent()
    {
        isDead = false;
    }
    //读取游戏进度

    //场景结束之后启动控制
    private void OnAftersceneLoadedEvent()
    {
        inputContronl.Gameplay.Enable();
        // 复位状态
        isDead = false;
        isHurt = false;
        isAttack = false;
        rb.velocity = Vector2.zero;
    }


    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);
        int faceDirection = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDirection = 1;
        if(inputDirection.x<0)
            faceDirection = -1;
        //人物翻转
        transform.localScale = new Vector3(faceDirection,1,1);
    }


    private void jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("jump");
        if (isDead) return;
        if(phsicscheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        GetComponent<AudioDefination>().PlayAudioClip();
    }
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        //if (!phsicscheck.isGround)
        //    return;
        if (isDead) return;
        playerAnimation.PlayAttack();
        isAttack = true;
        
    }

    #region UnityEvent

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputContronl.Gameplay.Disable();
    }
    #endregion
    private void CheckState()
    {
        coll.sharedMaterial = phsicscheck.isGround ? normal : wall;
    }
}
