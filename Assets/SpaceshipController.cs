using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxVelocity = 5f; // Giới hạn vận tốc tối đa
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;

        // Clamp vận tốc của tàu
        rb.velocity = Vector2.ClampMagnitude(movement, maxVelocity);
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();
        VFXController.instance.PlayMuzzleFlash(bulletSpawnPoint.position);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
    }
}
