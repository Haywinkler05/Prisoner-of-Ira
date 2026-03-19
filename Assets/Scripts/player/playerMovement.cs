using System;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
   
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera Camera;
    
    
    public bool isSprinting;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
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
            player.anim.SetBool("1_Move", true);
            player.anim.SetFloat("animSpeed", isSprinting ? 1.5f : 1f);
            bool isMovingVertical = Mathf.Abs(input.y) > 0.5f;
            player.anim.SetFloat("animSpeed", isMovingVertical ? 1.5f : 1f);
            if (input.x > 0) player.player.transform.localScale = new Vector3(-1, 1, 1);
            else if (input.x < 0) player.player.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            player.anim.SetBool("1_Move", false);
            player.anim.SetFloat("animSpeed", 1f);
        }

    }

}
