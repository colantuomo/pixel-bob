using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPoint : MonoBehaviour
{
    public float speed = 5f;
    public Transform pointToMove;
    private bool canMove;


    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove.position, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Tags.IsPlayer(collision))
        {
            canMove = true;
        }
    }
}
