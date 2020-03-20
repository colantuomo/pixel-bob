using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float health = 100f;
    public float speed;
    public float jumpForce;
    public ParticleSystem dustFX;
    public LayerMask groundLayer;
    public float groundLength = 1.2f;
    public Vector3 colliderOffset;
    public float raycastOffset = 0.4f;
    public SpriteRenderer dashSprite;
    public ParticleSystem bloodFX;

    private int totalHealth = 100;
    private Animator anim;
    private Rigidbody2D rb;
    private bool facingRight;
    private float moveInput;
    private bool jump;
    private AttackCtrl attackCtrl;
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackCtrl = GetComponent<AttackCtrl>();
    }

    private void Update()
    {
        jump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W);
        moveInput = Input.GetAxisRaw("Horizontal");
        DashTextCtrl();
        CheckHealth();
        Flip(moveInput);
        Jump();
    }
    void FixedUpdate()
    {
        Move();
    }

    private void DashTextCtrl()
    {
        dashSprite.enabled = false;
        if (attackCtrl.CanDash())
        {
            dashSprite.enabled = true;
        }
        if(CanTurn(moveInput)) {
            dashSprite.flipX = false;
        }
        if (CanTurn(moveInput) && !dashSprite.flipX)
        {
            dashSprite.flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead())
        {
            return;
        }
        if (collision.gameObject.tag.Equals(Tags.ENEMY))
        {
            if(!attackCtrl.isDash)
            {
                anim.SetTrigger("hit");
            }
            if(health != 0)
            {
                health -= collision.gameObject.GetComponent<Enemy>().damage;
            }
        }
        if (collision.gameObject.tag.Equals(Tags.PROJECTILE))
        {
            if (!IsCrouching())
            {
                Instantiate(bloodFX, transform.position, transform.rotation);
                anim.SetTrigger("hit");
                health -= collision.gameObject.GetComponent<BulletBehavior>().damage; ;
            }
        }
    }

    private void CheckHealth()
    {
        if(IsDead())
        {
            anim.SetTrigger("dead");
            health = 0;
            gameObject.GetComponent<PlayerCtrl>().enabled = false;
            gameObject.GetComponent<AttackCtrl>().enabled = false;
        }
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    void Move()
    {
        if(IsCrouching())
        {
            return;
        }
        anim.SetBool("isRunning", false);
        anim.SetFloat("speed", moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(moveInput != 0)
        {
            anim.SetBool("isRunning", true);
        }
    }

    void Jump()
    {
        if (OnGround() && !jump)
        {
            anim.SetBool("isJumping", false);
        }
        if (CanJump())
        {
            dustFX.Play();
            anim.SetBool("isJumping", true);
            anim.SetTrigger("takeOf");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool OnGround()
    {
        Vector3 raycastPosition = RaycastJumpOffset() + colliderOffset;
        Vector3 raycastPositionNegative = transform.position - colliderOffset;
        return Physics2D.Raycast(raycastPosition, Vector3.down, groundLength, groundLayer) || 
            Physics2D.Raycast(raycastPositionNegative, Vector3.down, groundLength, groundLayer);
    }

    private Vector3 RaycastJumpOffset()
    {
        return new Vector3(transform.position.x + raycastOffset, transform.position.y, transform.position.z);
    }

    private bool CanJump()
    {
        return jump && OnGround() && !IsCrouching();
    }

    private void Flip(float horizontal)
    {
        if (CanTurn(horizontal))
        {
            dustFX.Play();
            facingRight = !facingRight;
            transform.Rotate(transform.rotation.x, 180f, transform.rotation.z);
            raycastOffset *= -1;
        }
    }

    private bool CanTurn(float horizontal)
    {
        return horizontal > 0 && !facingRight || horizontal < 0 && facingRight;
    }

    public int GetTotalHealth()
    {
        return totalHealth;
    }

    public void IncreaseTotalHealth(int health)
    {
        totalHealth += health;
    }

    private bool IsCrouching()
    {
        return anim.GetBool("isCrounching");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var raycastPosition = RaycastJumpOffset() + colliderOffset;
        var raycastPositionNegative = RaycastJumpOffset() - colliderOffset;
        Gizmos.DrawLine(raycastPosition, raycastPosition + Vector3.down * groundLength);
        Gizmos.DrawLine(raycastPositionNegative, raycastPositionNegative + Vector3.down * groundLength);
    }
}
