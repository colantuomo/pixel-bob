using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager : MonoBehaviour
{
    public Transform cameraPosition;
    private CameraFollow cameraFollow;
    void Start()
    {
        cameraFollow = GameObject.FindObjectOfType<CameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals(Tags.PLAYER))
        {
            cameraFollow.UpdateYAxis(cameraPosition.position.y);
        }
        if (gameObject.name == "FaseCheckerShoot")
        {
            ReleaseShoots();
        }
    }

    private void ReleaseShoots()
    {
        GameObject.FindObjectOfType<GameManager>().hasReachShootingFase = true;
    }
}
