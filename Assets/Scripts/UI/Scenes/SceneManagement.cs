using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator sceneAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScene("Demo", 0.2f, "start");
        }
    }
    public void LoadScene(string scene, float waitSeconds, string trigger)
    {
        StartCoroutine(TriggerTransition(scene, waitSeconds, trigger));
    }

    private IEnumerator TriggerTransition(string scene, float waitSeconds, string trigger)
    {
        sceneAnimator.SetTrigger(trigger);
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        LoadScene(scene.name, 0.2f, "start");
    }
}
