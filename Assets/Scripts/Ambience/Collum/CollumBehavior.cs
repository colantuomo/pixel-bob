using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollumBehavior : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectile;
    public float shootingForce = 2f;
    public float destroyBulletTimer = 0.5f;
    public ParticleSystem trailFX;
    public float projectileOverlayRange = 2f;

    private float timer = 0f;
    private float delayTimer = 2f;
    private bool canShoot;

    private Transform playerPosition;
    private List<GameObject> bullets = new List<GameObject>();
    private GameManager gm;

    private Vector3 gizmosDraw;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        playerPosition = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Transform>();
    }
    void Update()
    {

        if (!gm.hasReachShootingFase)
        {
            return;
        }
        if(Time.time > timer)
        {
            timer = Time.time + delayTimer;
            canShoot = true;
        }
        if(canShoot)
        {
            GameObject bullet = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            Shoot(bullet);
            bullets.Add(bullet);
            canShoot = false;
        }
        if(!gm.hasFinishStage)
        {
            bullets.ForEach(bullet => Shoot(bullet));
        }
    }

    private void Shoot(GameObject bullet)
    {
        if(bullet == null)
        {
            return;
        }
        trailFX.Play();
        bullet.GetComponent<Transform>().position = Vector3.MoveTowards(bullet.GetComponent<Transform>().position, TargetPosition(bullet), shootingForce * Time.deltaTime);
        StartCoroutine(DestroyBullet(bullet));
    }

    private Vector3 TargetPosition(GameObject projectile)
    {
        Vector3 pos = playerPosition.position;
        gizmosDraw = projectile.GetComponent<Transform>().position;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(projectile.GetComponent<Transform>().position, projectileOverlayRange);

        foreach(Collider2D collision in collisions)
        {
            if (collision.gameObject.tag.Equals(Tags.PLAYER))
            {
                Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
                if(projectileRB == null)
                {
                    projectile.AddComponent(typeof(Rigidbody2D));
                }
                projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

        }
        return pos;
    }

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(destroyBulletTimer);
        Destroy(bullet);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmosDraw, projectileOverlayRange);
    }
}
