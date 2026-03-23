using UnityEngine;
using static Enemy;

public class BerserkerEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        difficulty = EnemyDifficulty.Hard;
    }

    // Update is called once per frame
   
}
