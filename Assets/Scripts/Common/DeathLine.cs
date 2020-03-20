using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLine : MonoBehaviour
{
    // Start is called before the first frame update
    SceneManagement sceneM;
    void Start()
    {
        sceneM = GameObject.FindObjectOfType<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals(Tags.PLAYER))
        {
            sceneM.ReloadScene();
        }
    }
}
