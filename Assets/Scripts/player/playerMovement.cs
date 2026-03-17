using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Player Parent Script")]
    [SerializeField] private Player player;
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    public bool isSprinting;
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

    }
}
