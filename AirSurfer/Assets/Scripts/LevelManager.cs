using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager lm;
    public static LevelManager Instance 
    {
        get 
        {
            if (lm == null)
                lm = FindObjectOfType(typeof(LevelManager)) as LevelManager;

            return lm;
        }
    }


    public ParticleSystem collideParticle;
    public ParticleSystem collectibleParticle;
    public ParticleSystem confetti;
    public UIManager uiMan;

    public Cannon[] cannons;

    //debug variables
    public float ballSpeed;

    public enum ObstacleType 
    {
        Cube,
        Sphere
    }

    public ObstacleType currentObstacleType = ObstacleType.Sphere;

    private void Awake()
    {
        lm = this;
    }

    public void PlayCollectibleParticle(Transform tr) 
    {
        collectibleParticle.Stop();
        collectibleParticle.transform.position = tr.position;
        collectibleParticle.Play();
    }

    public void PlayCollideParticle(Transform tr)
    {
        collideParticle.Stop();
        collideParticle.transform.position = tr.position;
        collideParticle.Play();
    }

    public void PlayConfettiParticle(Transform tr, Vector3 offset)
    {
        confetti.Stop();
        confetti.transform.position = tr.position + offset;
        confetti.Play();
    }

    public void CallFinish() 
    {
        GameController.Instance.isGameRunning = false;

        StartCoroutine(Finish());
    }

    IEnumerator Finish() 
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].Explode();
            yield return new WaitForSeconds(1f);
        }

        uiMan.PlayerWin();
    }
}
