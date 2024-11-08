using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFormationController : MonoBehaviour
{
    [Title("Enemy Formation Settings")]
    [SerializeField] private List<EnemyMovement> enemyMovements = new List<EnemyMovement>(); 
    [SerializeField] private float moveSpeed = 2f; 
    [SerializeField] private Vector2 randomSpawnRange = new Vector2(-20f, 20f); 
    private Vector3 checkTarget;
    private Vector3 tempTarget;
    void Start()
    {
        StartCoroutine(MoveEnemySequentially());
    }
    private IEnumerator MoveEnemySequentially()
    {
        while (enemyMovements.Exists(e => !e.doneMove)) 
        {   
            float xMin = float.MaxValue;
            float yMax = float.MinValue;
            EnemyMovement nextEnemy = null;

            foreach (EnemyMovement enemyMovement in enemyMovements)
            {
                if (enemyMovement.doneMove)
                    continue;

                tempTarget = enemyMovement.GetTargetPosition();
                
                if (tempTarget.x < xMin && tempTarget.y > yMax)
                {
                    xMin = tempTarget.x;
                    yMax = tempTarget.y;
                    nextEnemy = enemyMovement;
                }
            }

            if (nextEnemy != null)
            {
                nextEnemy.Move();
                // yield return new WaitUntil(() => nextEnemy.doneMove); 
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                yield return null; 
            }
        }
    }

    [Button]
    public void SetInfo()
    {
        foreach(Transform child in transform)
        {
            EnemyMovement enemyMovement = child.gameObject.AddComponent<EnemyMovement>();
            enemyMovement.SetTargetPosition(enemyMovement.transform.position, moveSpeed);
            enemyMovements.Add(enemyMovement);

            Vector3 randomPosition = new Vector3(
                Random.Range(randomSpawnRange.x, randomSpawnRange.y),
                10,
                0
            );
            child.transform.position = randomPosition; // Đặt vị trí spawn random ban đầu
        }
    }

    [Button]
    public void ClearInfo()
    {
        foreach (EnemyMovement enemyMovement in enemyMovements)
        {
            DestroyImmediate(enemyMovement);
        }
        enemyMovements.Clear();
    }
}
