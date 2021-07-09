using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController gc;
    public static GameController Instance 
    {
        get 
        {
            if (gc == null)
                gc = FindObjectOfType(typeof(GameController)) as GameController;

            return gc;
        }
    }

    public bool isGameRunning;

    private void Awake()
    {
        gc = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}