using UnityEngine;

public class playerRage : MonoBehaviour
{
    [Header("Rage Stats")]
    [SerializeField] private float rageBuildUp;
    [Header("Scripts")]
    [SerializeField] private Player player;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rageBuildUp = player.Rage;
       
    }
}
