using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    PlatformManager spawnPlatform;

    public static bool isAllowedToSpawn = false;

    [SerializeField]
    private float timeTillDestroy = 1;
    void Start()
    {
        spawnPlatform = FindObjectOfType<PlatformManager>();
        if (GameManager.levelDifficulty == "medium")
        {
            timeTillDestroy = 0.5f;
        }
        else if (GameManager.levelDifficulty == "hard")
        {
            timeTillDestroy = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAllowedToSpawn = true;
            //spawnPlatform.Spawn();
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAllowedToSpawn = false;
            //spawnPlatform.DestroyPlatform(5);
            Debug.Log("Exit");
            Destroy(gameObject.transform.parent.gameObject, timeTillDestroy);
        }
    }
}
