using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private bool explo;
    private Coroutine returnPool;

    private void OnEnable()
    {
        returnPool = StartCoroutine(lifeTimeReturn());
        explo = false;
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        if (returnPool != null)
        {
            StopCoroutine(returnPool);
            returnPool = null;
        }
    }

    void Update()
    {
        if (explo)
            return;
            
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(ReturnToPoolCollide());
        }
    }

    private IEnumerator lifeTimeReturn()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    private IEnumerator ReturnToPoolCollide()
    {
        // Hủy Coroutine trước khi trả đạn về pool
        if (returnPool != null)
        {
            StopCoroutine(returnPool);
            returnPool = null;
        }
        explo = true;
        spriteRenderer.enabled = false;
        VFXController.instance.PlayExplosion(explosion);
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }

    void ReturnToPool()
    {
        // Hủy Coroutine trước khi trả đạn về pool
        if (returnPool != null)
        {
            StopCoroutine(returnPool);
            returnPool = null;
        }
        bulletPool.ReturnBullet(gameObject);
    }
}
