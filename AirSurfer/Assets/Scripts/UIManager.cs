using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject gameOverObj;
    public GameObject playObject;
    public GameObject winObj;

    public GameObject debugMenu;

    public Slider speedSlider;

    // Use this for initialization
    void Start()
    {
        InitialiseDebugMenu();
        playObject.SetActive(true);
    }

    public void Play() 
    {
        playObject.SetActive(false);
        GameController.Instance.isGameRunning = true;
    }

    public void Restart() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit() 
    {
        Application.Quit();
        
    }

    public void PlayerWin() 
    {
        GameController.Instance.isGameRunning = false;
        winObj.SetActive(true);
    }

    public void GameOver() 
    {
        GameController.Instance.isGameRunning = false;
        gameOverObj.SetActive(true);

    }

    public void Debug()
    {
        //InitialiseDebugMenu();
        Time.timeScale = 0f;
        debugMenu.SetActive(true);
    }

    public void IncreaseSpeed() 
    {
        LevelManager.Instance.ballSpeed = speedSlider.value * 40.0f;
        PlayerPrefs.SetFloat("BallSpeed", LevelManager.Instance.ballSpeed);
    }

    public void ChangeObstacleToSphere() 
    {
        LevelManager.Instance.currentObstacleType = LevelManager.ObstacleType.Sphere;
    }

    public void ChangeObstacleToCube()
    {
        LevelManager.Instance.currentObstacleType = LevelManager.ObstacleType.Cube;
    }


    public GameObject sphereToggle;
    public GameObject cubeToggle;

    
    private void InitialiseDebugMenu()
    {
        if (PlayerPrefs.HasKey("BallSpeed"))
        {
            LevelManager.Instance.ballSpeed = PlayerPrefs.GetFloat("BallSpeed");
            speedSlider.value = LevelManager.Instance.ballSpeed / 40.0f;
        }
        else 
        {
            LevelManager.Instance.ballSpeed = 20.0f;
            speedSlider.value = LevelManager.Instance.ballSpeed / 40.0f;
            PlayerPrefs.SetFloat("BallSpeed", LevelManager.Instance.ballSpeed);
        }

        if (PlayerPrefs.HasKey("ObstacleType")) 
        {
            int i = PlayerPrefs.GetInt("ObstacleType");

            if(i == 1) 
            {
                LevelManager.Instance.currentObstacleType = LevelManager.ObstacleType.Sphere;
                sphereToggle.SetActive(true);
                cubeToggle.SetActive(false);
            }
            else 
            {
                LevelManager.Instance.currentObstacleType = LevelManager.ObstacleType.Cube;
                sphereToggle.SetActive(false);
                cubeToggle.SetActive(true);
            }
             
        }
        else 
        {
            LevelManager.Instance.currentObstacleType = LevelManager.ObstacleType.Sphere;
            PlayerPrefs.SetInt("ObstacleType", 1);
            sphereToggle.SetActive(true);
            cubeToggle.SetActive(false);
        }
    }

    public void SelectSphereObstacle() 
    {
        if (sphereToggle.activeSelf) 
        {
            sphereToggle.SetActive(false);
            cubeToggle.SetActive(true);
           // PlayerPrefs.SetInt("ObstacleType", 2);
        }
        else 
        {
            sphereToggle.SetActive(true);
            cubeToggle.SetActive(false);
            PlayerPrefs.SetInt("ObstacleType", 1);
        }
    }

    public void SelectCubeObstacle()
    {
        if (cubeToggle.activeSelf) 
        {
            sphereToggle.SetActive(true);
            cubeToggle.SetActive(false);
            PlayerPrefs.SetInt("ObstacleType", 1);
        }
        else 
        {
            sphereToggle.SetActive(false);
            cubeToggle.SetActive(true);
            PlayerPrefs.SetInt("ObstacleType", 2);
        }

        
    }

    public void CloseDebug() 
    {
        debugMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}