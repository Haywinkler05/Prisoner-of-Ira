using UnityEngine;

public class SkirmisherEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        difficulty = EnemyDifficulty.Easy;
        
    }
}
