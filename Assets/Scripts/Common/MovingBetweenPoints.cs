using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;
    public float offset = 5f;

    private bool reachPointA;
    private bool reachPointB;

    private void Start()
    {
        pointA.position = new Vector2(pointA.position.x + offset, pointA.position.y);
        pointB.position = new Vector2(pointB.position.x + offset, pointB.position.y);
    }
    private void FixedUpdate()
    {
        LoopingPosition();
    }

    private void LoopingPosition()
    {
        if (!reachPointA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
        if (hasReachPointA())
        {
            reachPointA = true;
            reachPointB = false;
        }

        if (reachPointA && !reachPointB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        if (hasReachPointB())
        {
            reachPointB = true;
            reachPointA = false;
        }
    }

    private bool hasReachPointA()
    {
        return Mathf.Abs(transform.position.x) == Mathf.Abs(pointA.position.x);
    }

    private bool hasReachPointB()
    {
        return Mathf.Abs(transform.position.x) == Mathf.Abs(pointB.position.x);
    }
}
