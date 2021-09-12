using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;

    [SerializeField]
    GameObject collectableObject;

    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    public Vector3 spawnPoint;

    [SerializeField]
    private Vector3 collectableSpawnPoint;

    [SerializeField]
    private float collectableObjectYOffset = 1;

    [SerializeField]
    private float distanceBetweenPlatforms;

    private float xPosition;
    private float yPosition;
    private float zPosition;
    
    [SerializeField]
    Vector3 objRotation;

    [SerializeField]
    public bool isRotationApplied;

    private int platformCount;

    int[] rotationVariants = new int[] { 0, -15, 15};
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        xPosition = targetObject.transform.position.x;
        yPosition = targetObject.transform.position.y;
        zPosition = targetObject.transform.position.z;

        objRotation.x = 90;//gameObject.transform.rotation.eulerAngles.x;
        objRotation.z = targetObject.transform.rotation.z;
        ResetPlatforms();
    }

    private void Update()
    {
        platformCount = GameObject.FindGameObjectsWithTag("Platform").Length;

        if (CheckPlayer.isAllowedToSpawn && platformCount < 2)
        {
            StartCoroutine(SpawnPlatform());
        }


    }


    IEnumerator SpawnPlatform()
    {
  
            xPosition += 2 * Random.Range(-4, 4);
            zPosition += 5 * Random.Range(3, 5);
            yPosition = 0;
            spawnPoint = new Vector3(xPosition, yPosition, zPosition);
            if (GameManager.levelDifficulty == "medium")
            {
                isRotationApplied = true;
            }
            if (isRotationApplied) objRotation.y = rotationVariants[Random.Range(0, rotationVariants.Length)];



            GameObject temp = Instantiate(targetObject, spawnPoint, Quaternion.Euler(objRotation));
            

            if (GameManager.collectables)
            {
                SpawnCollectable();
            }

            Debug.Log("spawny");
            CheckPlayer.isAllowedToSpawn = false;
            yield return null;

    }


    private void SpawnCollectable()
    {
        collectableSpawnPoint = spawnPoint;
        collectableSpawnPoint.y += collectableObjectYOffset;
        if (Random.Range(1, 13) % 3 == 0)
        {
            Debug.Log("Bingo");
            GameObject temp = Instantiate(collectableObject, collectableSpawnPoint, Quaternion.Euler(objRotation));
        }
    }

    public void ResetPlatforms()
    {
        Destroy(GameObject.FindWithTag("Platform"));
        StartCoroutine(SpawnPlatform());

        playerManager.setPlayerPosition(spawnPoint);
    }
}
