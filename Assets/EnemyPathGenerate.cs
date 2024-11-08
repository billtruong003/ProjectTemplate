using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyPathGenerate : MonoBehaviour
{   
    [SerializeField] private List<PathPoints> pathPoints;
    [SerializeField] private Vector2 rangeX = new Vector2(-20f, 20f);
    [SerializeField] private Vector2 rangeY = new Vector2(2, 10);
    [SerializeField] private int pathPointsNum; 

    [Serializable]
    public class PathPoints
    {
        public List<Transform> PathPointContainer = new List<Transform>();
        public Color Color;
        public Vector3 Position;
        
        public PathPoints(Vector2 limitX, Vector2 limitY, Transform container)
        {
            GameObject parent = new GameObject("PathParent");
            parent = Instantiate(parent, Vector3.zero, Quaternion.identity, container);
            for (int i = 0; i < 10; i++)
            {
                GameObject point = new GameObject("PathPoint");
                
                Position = new Vector3(UnityEngine.Random.Range(limitX.x, limitX.y), 
                                        UnityEngine.Random.Range(limitY.x, limitY.y), 0);
                point = Instantiate(point, Position, Quaternion.identity, parent.transform);
                PathPointContainer.Add(point.transform);
            }
        }
    }

    [Button]
    public void GeneratePathPoint()
    {
        
        for (int i = 0; i < pathPointsNum; i++)
        {
            PathPoints pathPoint = new PathPoints(rangeX, rangeY, transform);
            pathPoints.Add(pathPoint);
        }
    }

    [Button]
    public void ClearPathPoint()
    {
        foreach (PathPoints pathPoint in pathPoints)
        {
            foreach (Transform point in pathPoint.PathPointContainer)
            {
                DestroyImmediate(point.gameObject);
            }
        }
        pathPoints.Clear();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        // Duyệt qua từng PathPoints
        foreach (PathPoints pathPoint in pathPoints)
        {
            Gizmos.color = pathPoint.Color;
            for (int i = 0; i < pathPoint.PathPointContainer.Count - 1; i++)
            {
                // Vẽ đường nối giữa các điểm liên tiếp
                if (pathPoint.PathPointContainer[i] != null && pathPoint.PathPointContainer[i + 1] != null)
                {
                    Gizmos.DrawLine(pathPoint.PathPointContainer[i].position, pathPoint.PathPointContainer[i + 1].position);
                }
            }
        }
    }
}
