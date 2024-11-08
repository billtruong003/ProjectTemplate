using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void Start()
    {
        // Khởi tạo pool với số lượng đạn nhất định
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.GetComponent<Bullet>().SetPool(this);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // Lấy đạn từ pool, nếu không có thì tạo thêm
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
