using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class CameraShake : MonoBehaviour
{
    public float magnitude = 2f;
    public float roughness = 3f;
    public float fadeInTime = .1f;
    public float fadeOutTime = 1f;
    public void Shake() {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
