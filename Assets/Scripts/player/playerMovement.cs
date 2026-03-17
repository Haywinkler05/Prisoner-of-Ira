using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [Header("Player Parent Script")]
    [SerializeField] private Player player;
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    private GameObject playerOBJ;
    public bool isSprinting;
    void Start()
    {
       if(rb == null) rb = GetComponent<Rigidbody2D>();
       playerOBJ = rb.gameObject;
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

    }
public void look(Vector2 input)
    {
        Vector2 lookDirection = Vector2.zero;
        lookDirection = input - (Vector2)player.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x);
        angle = Mathf.Rad2Deg * angle;
       player.transform.rotation =  Quaternion.Euler(0, 0, angle);
        
    }
}
