using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{

    [SerializeField] private Vector3 rotAxis;
    [SerializeField] private bool worldSpace;

    // Update is called once per frame
    void Update()
    {
        if(!worldSpace)
            transform.Rotate(rotAxis * Time.deltaTime);
        else
            transform.Rotate(rotAxis * Time.deltaTime, Space.World);
    }
}
