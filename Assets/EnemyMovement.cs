using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition; // Vị trí mục tiêu trong đội hình
    [SerializeField] private float moveSpeed; // Tốc độ di chuyển vào đội hình
    public bool doneMove;
    public Vector3 GetTargetPosition() => targetPosition;
    void Start()
    {
        doneMove = false;
        transform.position = new Vector3(Random.Range(-20f, 20f), 10, 0);
    }

    public void Move()
    {
        StartCoroutine(MoveToPosition());
    }

    public void SetTargetPosition(Vector3 position, float speed)
    {
        targetPosition = position; // Thiết lập vị trí mục tiêu
        moveSpeed = speed; // Thiết lập tốc độ di chuyển
        
    }
    private IEnumerator MoveToPosition()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        doneMove = true;
    }

}
