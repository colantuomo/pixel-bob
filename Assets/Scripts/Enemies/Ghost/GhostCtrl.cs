using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCtrl : MonoBehaviour
{
    public float speed;
    public GameObject effect;
    public float distanteToStopFollow = 2;
    public LayerMask playerLayer;
    public float findRange = 5f;

    public Transform pointA;
    public Transform pointB;

    private Transform player;
    private bool facingRight;
    private Rigidbody2D rb;

    private bool reachPointA;
    private bool reachPointB;
    private bool chasingPlayer;
    void Start()
    {
        facingRight = false;
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Idle();
        FollowPlayer();
    }

    private void Idle()
    {
        if (chasingPlayer)
        {
            return;
        }
        if (!reachPointA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
        if(Mathf.Abs(transform.position.x) == Mathf.Abs(pointA.position.x))
        {
            reachPointA = true;
            reachPointB = false;
            Flipping();
        }

        if(reachPointA && !reachPointB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        if(Mathf.Abs(transform.position.x) == Mathf.Abs(pointB.position.x))
        {
            reachPointB = true;
            reachPointA = false;
            Flipping();
        }
    }


    private void FollowPlayer()
    {
        Vector3 from = new Vector3(transform.position.x + -findRange, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(transform.position.x + findRange, transform.position.y, transform.position.z);
        RaycastHit2D target = Physics2D.Linecast(from, to, playerLayer);
        float acceleration = 0.2f;
        if(target.transform != null)
        {
            LookAtPlayer();
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (speed + acceleration) * Time.deltaTime);
            chasingPlayer = true;
        } else
        {
            chasingPlayer = false;
        }
    }

    private void LookAtPlayer()
    {
        if (HasToFlip())
        {
            Flipping();
        }
    }

    private void Flipping()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool HasToFlip()
    {
        float horizontalPosition = transform.position.x - player.position.x;
        return horizontalPosition < 0 && !facingRight || horizontalPosition > 0 && facingRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.position == null)
        {
            return;
        }
        Vector3 from = new Vector3(transform.position.x + -findRange, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(transform.position.x + findRange, transform.position.y, transform.position.z);
        Gizmos.DrawLine(from, to);
    }
}
