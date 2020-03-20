using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform toFollow;
    public float minBounds;
    public float maxBounds;
    private Vector3 pos;

    void Update()
    {
        pos = new Vector3(toFollow.position.x, transform.position.y, transform.position.z);
        if(pos.x <= minBounds)
        {
            pos.x = minBounds;
        } else if (pos.x >= maxBounds)
        {
            pos.x = maxBounds;
        }
    }

    private void LateUpdate()
    {
        transform.position = pos;
    }

    public void UpdateYAxis(float posY)
    {
        transform.position = new Vector3(toFollow.position.x, posY, transform.position.z);
    }
}
