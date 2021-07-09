using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed;
    public float gravity;
    public ParticleSystem movementEffect;
    public Joystick joystick;

    Vector3 moveDirection;

    private bool canMove;
    private RaycastHit hit;

    [SerializeField] private Transform rayPoint;
    [SerializeField] private float distance;

    [SerializeField] private Transform[] bombEjectPoints;
    [SerializeField] private GameObject missile;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameController.Instance.isGameRunning)
            return;

        DetectItems();

        canMove = true;

        Vector2 direction = joystick.direction;

        if (controller.isGrounded) 
        {
            moveDirection = new Vector3(direction.x, 0f, transform.forward.z);
            moveDirection *= speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        
        if (canMove)
        {
            anim.SetBool("Moving", true);
            movementEffect.Play();
        }
        else 
        {
            anim.SetBool("Moving", false);
            movementEffect.Stop();
        }
       

    }


    public void DetectItems() 
    {
        if(Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, distance)) 
        {
            if (hit.collider.CompareTag("Collectible")) 
            {
                Collectible collectible = hit.transform.GetComponent<Collectible>();

                if(collectible != null) 
                {
                    collectible.GiveCollectible();

                    if (collectible.GetCollectibleType() == Collectible.CollectibleType.Bomb)
                    {
                        for (int i = 0; i < bombEjectPoints.Length; i++)
                        {
                            TrashMan.spawn(missile, bombEjectPoints[i].position, bombEjectPoints[i].rotation);
                        }
                    }
                }

            }
            else if (hit.collider.CompareTag("Ball"))
            {
                Die();
            }
            else if (hit.collider.CompareTag("Finish")) 
            {
                LevelManager.Instance.CallFinish();
                anim.SetTrigger("Celebrate");
                LevelManager.Instance.PlayConfettiParticle(transform, new Vector3(0f, 2.5f, 0f));
            }
        }
    }

    public void Die() 
    {
        LevelManager.Instance.PlayCollideParticle(transform);
        LevelManager.Instance.uiMan.GameOver();
        gameObject.SetActive(false);
    }
   
   

}
