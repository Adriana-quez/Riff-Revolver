using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlayerPosition : MonoBehaviour
{
    public Transform cameraTransform;  
    public Vector3 offset;             
    void Start()
    {
        offset = transform.position - cameraTransform.position;
    }

    void Update()
    {
        transform.position = cameraTransform.position + offset;
    }
}
