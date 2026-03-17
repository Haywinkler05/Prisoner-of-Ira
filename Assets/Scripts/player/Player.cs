using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float Health;
    public float Dmg;
    [Range(0f, 1f)] public float Rage;
    public float moveSpeed;
    public float sprintSpeed;
    [Header("Player Object")]
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
