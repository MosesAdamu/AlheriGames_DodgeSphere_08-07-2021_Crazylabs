using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed;
    public Transform rayPoint;
    public float distance = 0.5f;

    private RaycastHit hit;

    private float deactTimer;
    [SerializeField]private float deacTimeLimit = 3f;


    // Update is called once per frame
    void Update()
    {
        deactTimer += Time.deltaTime;

        DetectItems();

        transform.Translate(transform.forward * speed * Time.deltaTime);

        if (deactTimer > deacTimeLimit)
        {
            deactTimer = 0f;
            LevelManager.Instance.PlayCollideParticle(transform);
            gameObject.SetActive(false);
        }
    }

    public void DetectItems()
    {
        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, distance))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                hit.collider.GetComponent<Balls>().Explode();
            }
        }
    }
}