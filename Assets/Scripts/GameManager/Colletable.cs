using UnityEngine;
using EZCameraShake;

public class Colletable : MonoBehaviour
{
    public ParticleSystem grabColletableFX;

    private float magnitude = 6f;
    private float roughness = 2f;
    private float fadeInTime = .1f;
    private float fadeOutTime = 1f;

    private ColletableManager cManager;

    private void Start()
    {
        cManager = GameObject.FindObjectOfType<ColletableManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Tags.PLAYER))
        {
            cManager.AddColletable();
            // CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            Instantiate(grabColletableFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
