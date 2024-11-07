using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public static VFXController instance;
    [SerializeField] private ParticleSystem muzzleFlash;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void PlayMuzzleFlash(Vector3 bulletSpawnPos)
    {
        muzzleFlash.transform.position = bulletSpawnPos;
        muzzleFlash.Play();
    }
    
    public void PlayExplosion(ParticleSystem explosionPos)
    {
        explosionPos.Play();
    }
}
