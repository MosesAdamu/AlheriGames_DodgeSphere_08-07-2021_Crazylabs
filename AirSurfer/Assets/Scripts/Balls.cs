using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public Transform _ballObject;
    public float _ballObjectRotateSpeed;
    public Vector3 _ballRotateVector;

    private bool canMove;

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        //rotate the ball
        _ballObject.Rotate(_ballRotateVector * _ballObjectRotateSpeed * Time.deltaTime);

        //move the ball parent
        transform.Translate(transform.forward * LevelManager.Instance.ballSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End")) 
        {
            canMove = false;
            TrashMan.despawn(gameObject);
        }
    }

    public void Move() 
    {
        canMove = true;
    }

    public void Explode() 
    {
        LevelManager.Instance.PlayCollideParticle(transform);
        gameObject.SetActive(false);

    }

}
