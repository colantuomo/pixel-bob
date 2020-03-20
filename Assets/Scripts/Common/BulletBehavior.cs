using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BulletBehavior : MonoBehaviour
{
    public float damage = 10f;
    public ParticleSystem defaultFX;
    public LayerMask deathLine;

    private float magnitude = 2f;
    private float roughness = 3f;
    private float fadeInTime = .1f;
    private float fadeOutTime = 1f;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(defaultFX, transform.position, transform.rotation);
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        Destroy(gameObject);
    }
}
