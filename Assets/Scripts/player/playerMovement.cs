using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [Header("Player Parent Script")]
    [SerializeField] private Player player;
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera Camera;
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
public void look(Vector3 input)
    {
        Vector3 worldPos = Camera.ScreenToWorldPoint(new Vector3(input.x, input.y, Camera.nearClipPlane));
        Vector3 rotateDir = (worldPos - playerOBJ.transform.position).normalized;
        rotateDir.z = 0;
        float angle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg; //TODO; Subtract offset degrees of the sprites facing direction
        playerOBJ.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

       
        
    }
}
