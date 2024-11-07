using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PatternEnemy : MonoBehaviour
{
    [Title("Enemy Settings")]
    [SerializeField, Required] private GameObject enemyPrefab;

    [Title("Pattern Configuration")]
    [EnumToggleButtons]
    [SerializeField] private PatternType patternType = PatternType.Grid;

    [ShowIf("@this.patternType == PatternType.Grid || this.patternType == PatternType.Line || this.patternType == PatternType.Triangle")] 
    [SerializeField, Min(1)] private int rows = 3;

    [ShowIf("@this.patternType == PatternType.Grid")] 
    [SerializeField, Min(1)] private int columns = 3;

    [ShowIf("@this.patternType == PatternType.Circle")] 
    [SerializeField, Min(3)] private int pointsInCircle = 8; // Số lượng điểm trên đường tròn

    [SerializeField, Min(0.1f)] private float spacing = 2f;

    [Title("Preview Settings")]
    [ToggleLeft, LabelText("Show Pattern Preview")]
    [SerializeField] private bool showPreview;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private string enemyContainerName = "EnemyContainer";
    public enum PatternType { Grid, Circle, Line, Spiral, Triangle }
    private Transform parent;

    [Button("Generate Pattern", ButtonSizes.Medium)]
    public void GeneratePattern()
    {
        ClearPattern();

        switch (patternType)
        {
            case PatternType.Grid:
                GenerateGridPattern();
                break;
            case PatternType.Circle:
                GenerateCircularPattern();
                break;
            case PatternType.Line:
                GenerateLinePattern();
                break;
            case PatternType.Spiral:
                GenerateSpiralPattern();
                break;
            case PatternType.Triangle:
                GenerateTrianglePattern();
                break;
        }
    }

    [Button("Clear", ButtonSizes.Medium)]
    private void ClearPattern()
    {
        foreach (GameObject enemy in enemies)
        {
            DestroyImmediate(enemy);
        }
        enemies.Clear();
    }

    private Transform CreateContainer()
    {
        GameObject container = new GameObject(enemyContainerName);
        container.transform.SetParent(transform);
        return container.transform;
    }

    private void GenerateGridPattern()
    {
        float width = (columns - 1) * spacing;
        float height = (rows - 1) * spacing;
        Vector3 bottomCenter = transform.position; 
        parent = CreateContainer();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = bottomCenter + new Vector3(j * spacing - width / 2, i * spacing, 0);
                GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, parent);
            }
        }
        enemies.Add(parent.gameObject);
    }

    private void GenerateCircularPattern()
    {
        float radius = spacing * pointsInCircle / (2 * Mathf.PI); // Bán kính dựa trên số điểm và khoảng cách
        Vector3 centerPosition = transform.position + new Vector3(0, radius, 0); // Tạo vòng tròn ở trên điểm gốc
        parent = CreateContainer();

        for (int i = 0; i < pointsInCircle; i++)
        {
            float angle = i * Mathf.PI * 2 / pointsInCircle;
            Vector3 position = centerPosition + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, parent);
        }
        enemies.Add(parent.gameObject);
    }

    private void GenerateLinePattern()
    {
        float totalWidth = (rows - 1) * spacing;
        Vector3 startPosition = transform.position - new Vector3(totalWidth / 2, 0, 0);
        parent = CreateContainer();

        for (int i = 0; i < rows; i++)
        {
            Vector3 position = startPosition + new Vector3(i * spacing, 0, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, parent);
        }
        enemies.Add(parent.gameObject);
    }

    private void GenerateSpiralPattern()
    {
        int enemyCount = rows * columns;
        float angleIncrement = Mathf.PI * 2 / enemyCount; 
        float currentRadius = 0;
        float radiusIncrement = spacing / (2 * Mathf.PI);
        Vector3 centerPosition = transform.position;
        parent = CreateContainer();

        for (int i = 0; i < enemyCount; i++)
        {
            float angle = i * angleIncrement;
            Vector3 position = centerPosition + new Vector3(Mathf.Cos(angle) * currentRadius, Mathf.Sin(angle) * currentRadius, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, parent);
            currentRadius += radiusIncrement;
        }
        enemies.Add(parent.gameObject);
    }

    private void GenerateTrianglePattern()
    {
        Vector3 bottomCenter = transform.position;
        parent = CreateContainer();

        for (int i = 0; i < rows; i++)
        {
            int enemiesInRow = rows - i;
            float rowWidth = (enemiesInRow - 1) * spacing;
            Vector3 rowStart = bottomCenter + new Vector3(-rowWidth / 2, i * spacing, 0);

            for (int j = 0; j < enemiesInRow; j++)
            {
                Vector3 position = rowStart + new Vector3(j * spacing, 0, 0);
                GameObject enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, parent);
            }
        }
        enemies.Add(parent.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!showPreview || enemyPrefab == null) return;

        Gizmos.color = Color.green;

        if (patternType == PatternType.Grid)
        {
            float width = (columns - 1) * spacing;
            float height = (rows - 1) * spacing;
            Vector3 bottomCenter = transform.position;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = bottomCenter + new Vector3(j * spacing - width / 2, i * spacing, 0);
                    Gizmos.DrawWireSphere(position, 0.2f);
                }
            }
        }
        else if (patternType == PatternType.Circle)
        {
            float radius = spacing * pointsInCircle / (2 * Mathf.PI);
            Vector3 centerPosition = transform.position + new Vector3(0, radius, 0);

            for (int i = 0; i < pointsInCircle; i++)
            {
                float angle = i * Mathf.PI * 2 / pointsInCircle;
                Vector3 position = centerPosition + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                Gizmos.DrawWireSphere(position, 0.2f);
            }
        }
        else if (patternType == PatternType.Line)
        {
            float totalWidth = (rows - 1) * spacing;
            Vector3 startPosition = transform.position - new Vector3(totalWidth / 2, 0, 0);

            for (int i = 0; i < rows; i++)
            {
                Vector3 position = startPosition + new Vector3(i * spacing, 0, 0);
                Gizmos.DrawWireSphere(position, 0.2f);
            }
        }
        else if (patternType == PatternType.Spiral)
        {
            int enemyCount = rows * columns;
            float angleIncrement = Mathf.PI * 2 / enemyCount;
            float currentRadius = 0;
            float radiusIncrement = spacing / (2 * Mathf.PI);
            Vector3 centerPosition = transform.position;

            for (int i = 0; i < enemyCount; i++)
            {
                float angle = i * angleIncrement;
                Vector3 position = centerPosition + new Vector3(Mathf.Cos(angle) * currentRadius, Mathf.Sin(angle) * currentRadius, 0);
                Gizmos.DrawWireSphere(position, 0.2f);
                currentRadius += radiusIncrement;
            }
        }
        else if (patternType == PatternType.Triangle)
        {
            Vector3 bottomCenter = transform.position;
            
            for (int i = 0; i < rows; i++)
            {
                int enemiesInRow = rows - i;
                float rowWidth = (enemiesInRow - 1) * spacing;
                Vector3 rowStart = bottomCenter + new Vector3(-rowWidth / 2, i * spacing, 0);

                for (int j = 0; j < enemiesInRow; j++)
                {
                    Vector3 position = rowStart + new Vector3(j * spacing, 0, 0);
                    Gizmos.DrawWireSphere(position, 0.2f);
                }
            }
        }
    }
}
