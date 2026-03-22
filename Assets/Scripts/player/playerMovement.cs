using System;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
   
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera Camera;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioClip footStep;
    private float footstepTimer;
    [SerializeField] private float footstepInterval = 0.3f;

    public bool isSprinting;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
    [SerializeField] private playerCombat combat;
    void Start()
    {
       if(rb == null) rb = GetComponent<Rigidbody2D>();
       
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
  public void Move(Vector2 input)
    {
         Vector2 moveDirection = Vector2.zero;
         input = input.normalized;
         moveDirection.y = input.y;
         moveDirection.x = input.x;
        float currentSpeed = isSprinting ? player.sprintSpeed : player.moveSpeed;
        rb.linearVelocity = moveDirection * currentSpeed;
        if(input.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                footsteps.PlayOneShot(footStep);
                footstepTimer = isSprinting ? footstepInterval * 0.65f : footstepInterval;
            }
  
            player.anim.SetBool("1_Move", true);
            player.anim.SetFloat("animSpeed", isSprinting ? 1.5f : 1f);
            bool isMovingVertical = Mathf.Abs(input.y) > 0.5f;
            player.anim.SetFloat("animSpeed", isMovingVertical ? 1.5f : 1f);
            if (input.x > 0) player.player.transform.localScale = new Vector3(-1, 1, 1);
            else if (input.x < 0) player.player.transform.localScale = new Vector3(1, 1, 1);
            if(input.x > 0) { combat.attackPoint.localPosition = new Vector3(0.5f, 0, 0); }
            else if(input.x < 0) { combat.attackPoint.localPosition = new Vector3(-0.5f, 0 , 0); }   
        }
        else
        {

            footstepTimer = 0f;
            player.anim.SetBool("1_Move", false);
            player.anim.SetFloat("animSpeed", 1f);
        }

    }

}
