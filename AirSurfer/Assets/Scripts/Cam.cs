using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // The target we are following
    public Transform player;
    private Rigidbody playerRb;
    private Camera cam;

    // The distance in the x-z plane to the target
    public float distance = 6.0f;

    // the height we want the camera to be above the target
    public float height = 2.0f;

    public float heightOffset = .75f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public bool useSmoothRotation = true;

    public float minimumFOV = 50f;
    public float maximumFOV = 70f;

    public float maximumTilt = 15f;
    private float tiltAngle = 0f;

    private Transform tr;

    //speed effect
    [SerializeField] private ParticleSystem[] speedWarp;

    private bool lerp;

    private void Awake()
    {
        tr = transform;
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            playerRb = player.root.GetComponent<Rigidbody>();
        }
    }

    void Init()
    {
        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            playerRb = player.root.GetComponent<Rigidbody>();
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    public void SetTargetAndInit(Transform target)
    {
        player = target;
        Init();
    }

    void Update()
    {

        //Tilt Angle Calculation.
        tiltAngle = Mathf.Lerp(tiltAngle, (Mathf.Clamp(-player.InverseTransformDirection(playerRb.velocity).x, -35, 35)), Time.deltaTime * 2f);

        if (!cam)
        {
            cam = GetComponent<Camera>();

            if (!cam)
                cam = tr.GetComponentInChildren<Camera>();
        }

        cam.fieldOfView = Mathf.Lerp(minimumFOV, maximumFOV, (playerRb.velocity.magnitude * 3f) / 150f);

        if (lerp) 
        {
            transform.position = Vector3.Lerp(transform.position, toTr.position, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, toTr.rotation, 0.1f);

        }

    }

    void LateUpdate()
    {

        // Early out if we don't have a target
        if (!player || !playerRb || lerp)
            return;

        float speed = (playerRb.transform.InverseTransformDirection(playerRb.velocity).z) * 3f;

        // Calculate the current rotation angles.
        float wantedRotationAngle = player.eulerAngles.y;
        float wantedHeight = player.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        if (useSmoothRotation)
            rotationDamping = Mathf.Lerp(0f, 3f, (playerRb.velocity.magnitude * 3f) / 40f);

        if (speed < -10)
            wantedRotationAngle = player.eulerAngles.y + 180;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight + Mathf.Lerp(-1f, 0f, (playerRb.velocity.magnitude * 3f) / 20f), heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        tr.position = player.position;
        tr.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        tr.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        tr.LookAt(new Vector3(player.position.x, player.position.y + heightOffset, player.position.z));
        tr.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(tiltAngle, -10f, 10f));
    }

    public void TurnOnSpeedFx() 
    {
        for (int i = 0; i < speedWarp.Length; i++)
        {
            speedWarp[i].Play();
        }
    }

    public void TurnOffSpeedFx() 
    {
        for (int i = 0; i < speedWarp.Length; i++)
        {
            speedWarp[i].Stop();
        }
    }

    private Transform toTr;

    public void FaceMeCam(Transform myTr) 
    {
        toTr = myTr;
        lerp = true;
    }
}
