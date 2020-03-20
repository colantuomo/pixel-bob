using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasFinishStage;
    public bool hasReachShootingFase;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishStage()
    {
        hasFinishStage = true;
    }
}
