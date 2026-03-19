using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float Health;
    public float Dmg;
    public float maxRage;
    [SerializeField]private float rage;
    public float Rage
    {
        get { return rage; }
        set { rage = Mathf.Clamp(value, 0f, maxRage); }
    }
    public float moveSpeed;
    public float sprintSpeed;
    [Header("Player components")]
    public GameObject player;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
