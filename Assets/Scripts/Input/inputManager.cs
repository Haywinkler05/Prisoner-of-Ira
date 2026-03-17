using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    private InputSystem_Actions playerInput;
    private InputSystem_Actions.PlayerActions playerAction;

    [SerializeField]private playerMovement movement;

    private void Awake()
    {
       
        playerInput = new InputSystem_Actions();
        playerAction = playerInput.Player;
        
        playerAction.Sprint.performed += ctx => movement.isSprinting = true;
        playerAction.Sprint.canceled += ctx => movement.isSprinting = false;
        playerAction.Look.performed += ctx => movement.look(playerAction.Look.ReadValue<Vector2>());

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        movement.Move(playerAction.Move.ReadValue<Vector2>());
       
    }
    private void LateUpdate()
    {
        
    }
    private void OnEnable()
    {
        playerAction.Enable();
    }
    private void OnDisable()
    {
        playerAction.Disable();
    }
}
