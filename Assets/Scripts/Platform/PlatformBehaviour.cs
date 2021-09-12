using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Vector3 position;

    [SerializeField]
    string axis = "y";

    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float speed = 3;

    [SerializeField]
    float bandwidth = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        direction = GetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        position = transform.position + direction * speed * Time.deltaTime;

        if (position.x > bandwidth || position.x < -bandwidth)
        {
            direction *= -1;
        }
        rb.MovePosition(position);
    }



    private Vector3 GetDirection()
    {
        if (axis == "y")
        {
            return transform.right;

        }
        else if (axis == "x")
        {
            return transform.up;
        }
        else if (axis == "z")
        {
            return transform.forward;
        }

        return new Vector3(0,0,0);
    }
}
