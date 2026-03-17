using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Player Parent Script")]
    [SerializeField] private Player player;
    private GameObject playerObj;
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float sprintSpeed = 5f;
    public bool isSprinting;
    void Start()
    {
       if(rb == null) rb = GetComponent<Rigidbody2D>();
        playerObj = player.player;
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
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
         rb.linearVelocity = moveDirection * currentSpeed;

    }
}
