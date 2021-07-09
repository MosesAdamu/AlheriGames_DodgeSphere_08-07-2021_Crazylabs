using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject[] balls;
    public GameObject[] cubeBalls;
    public Transform ejectPoint;

    //spawning of cannon balls
    private float timer;
    [SerializeField]private float spawnIntervals;

    private void Start()
    {
        spawnIntervals = spawnIntervals = Random.Range(2.0f, 3.5f); 
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isGameRunning)
            return;

        timer += Time.deltaTime;

        if(timer >= spawnIntervals) 
        {
            SpawnBall();
            timer = 0f;
            spawnIntervals = Random.Range(2.0f, 3.5f);
        }
    }


    private void SpawnBall() 
    {
        GameObject ball = null;

        if (LevelManager.Instance.currentObstacleType == LevelManager.ObstacleType.Sphere) 
        {
            ball = TrashMan.spawn(balls[Random.Range(0, balls.Length)]);
        }
        else 
        {
            ball = TrashMan.spawn(cubeBalls[Random.Range(0, balls.Length)]);
        }
        ball.transform.position = ejectPoint.position;
        ball.transform.rotation = Quaternion.Euler(0, 180f, 0);
        ball.GetComponent<Balls>().Move();
    }

    public void Explode() 
    {
        LevelManager.Instance.PlayCollideParticle(transform);
        gameObject.SetActive(false);
    }

   
}
