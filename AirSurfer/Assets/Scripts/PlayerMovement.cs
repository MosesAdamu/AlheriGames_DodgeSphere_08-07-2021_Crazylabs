using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charControl;

    private float baseSpeed = 6.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    private Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //give the player forward velocity
        Vector3 moveVec = transform.forward * baseSpeed;

        //get player input

    }
}
