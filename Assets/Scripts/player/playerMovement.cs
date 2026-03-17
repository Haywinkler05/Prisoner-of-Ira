using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Player Parent Script")]
    [SerializeField] private Player player;
    private GameObject playerObj;
    [Header("Movement Components")]
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    void Start()
    {
       if(rb == null) rb = GetComponent<Rigidbody2D>();
        playerObj = player.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void processMove(Vector2 input)
    {
        Vector2 moveDirection = Vector2.zero;
        input = input.normalized;
        moveDirection.y = input.y;
        moveDirection.x = input.x;
        rb.linearVelocity = moveDirection * moveSpeed;
        Debug.Log("Player Moved!");
    }
}
