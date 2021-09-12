using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 100;

    [SerializeField]
    float offset;

    [SerializeField]
    GameObject particleObject;

    
    private Vector3 particlePosition;

    private void Start()
    {
        particlePosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z); particlePosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }
    void Update()
    {
        if (!GameManager.isPaused)
        {
            Rotate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.playerCoins += 1;
            Instantiate(particleObject, particlePosition, Quaternion.identity);
            Destroy(gameObject); 
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}
