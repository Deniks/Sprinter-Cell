using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdTransformer : MonoBehaviour
{
    void Start()
    {
        transform.localScale = transform.parent.transform.localScale;
        transform.position = transform.parent.position;
    }
}
