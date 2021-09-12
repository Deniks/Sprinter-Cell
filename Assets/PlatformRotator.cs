using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{

    [SerializeField]
    private float platformRotationSpeed = 7;

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.levelDifficulty == "hard")
        {
            ApplySmoothRotationToPlatform(gameObject);
        }
    }

    private void ApplySmoothRotationToPlatform(GameObject platform)
    {
        platform.transform.Rotate(Time.deltaTime , Time.deltaTime, Time.deltaTime * platformRotationSpeed);
    }
}
