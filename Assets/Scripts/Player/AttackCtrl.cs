using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCtrl : MonoBehaviour
{
    public float dashCooldown = 5f;
    public float dashDuration = 0.2f;
    public float dashSpeed = 5f;
    public ParticleSystem dustFX;
    public PlayerCtrl playerCtrl;

    public Transform hitPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private bool useFire1;
    private bool useFire2;
    private bool useDash;
    private bool InputS_UP;
    private bool InputS_DOWN;

    private float nextDash = 1;
    public bool isDash;

    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        useFire1 = Input.GetButtonDown("Fire1");
        useFire2 = Input.GetButtonDown("Fire2");
        useDash = Input.GetKeyDown(KeyCode.LeftShift);
        InputS_UP = Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
        InputS_DOWN = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        if (!anim.GetBool("isCrounching"))
        {
            Attack();
        }
        CrouchControl();
    }

    private void FixedUpdate()
    {
        DashControl();
    }

    void Attack()
    {
        float fxDuration = 0.5f;
        if (useFire1)
        {
            AttackHitBox(10);
            anim.SetTrigger("attack1");
            StartCoroutine(StopParticleFX(fxDuration));
        }

        if (useFire2 && playerCtrl.OnGround())
        {
            AttackHitBox(25);
            anim.SetTrigger("attack2");
            rb.velocity = Vector2.up * 15;
            StartCoroutine(StopParticleFX(fxDuration));
        }

        if (useDash && CanDash())
        {
            isDash = true;
            nextDash = Time.time + dashCooldown;
            anim.SetBool("dashAtk", isDash);
            StartCoroutine(ResetDash());
            StartCoroutine(StopParticleFX(fxDuration));
        }

    }

    public bool CanDash()
    {
        return Time.time > nextDash;
    }

    private void CrouchControl()
    {
        if (InputS_DOWN)
        {
            anim.SetBool("isCrounching", true);
        }
        else if (InputS_UP)
        {
            anim.SetBool("isCrounching", false);
        }
        if (anim.GetBool("isCrounching"))
        {
            if(!anim.GetBool("isJumping"))
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            Collider2D[] collisions = PointCollisions();
            Defense(collisions);
        }
    }

    private void Defense(Collider2D[] collisions)
    {
        foreach (Collider2D collision in collisions)
        {
            GameObject collisionObj = collision.gameObject;
            if (collisionObj.tag.Equals(Tags.PROJECTILE))
            {
                collisionObj.GetComponent<Rigidbody2D>().AddForce(transform.right * 50f, ForceMode2D.Impulse);
            }
        }
    }

    private void AttackHitBox(float damage)
    {
        Collider2D[] hitEnemies = PointCollisions();
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private Collider2D[] PointCollisions()
    {
        return Physics2D.OverlapCircleAll(hitPoint.position, attackRange, enemyLayers);
    }

    IEnumerator StopParticleFX(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDash = false;
        rb.velocity = Vector2.zero;
        anim.SetBool("dashAtk", isDash);
    }

    private void DashControl()
    {
        if (isDash)
        {
            float dashDamage = 20f;
            dustFX.Play();
            AttackHitBox(dashDamage);
            rb.velocity = transform.right * dashSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(hitPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }

}
