using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public Spawnpoint playerSpawnpoint;
    public RPGCameraManager cameraManager;

    public static RPGGameManager sharedInstance = null;

    void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetUpScene();
    }

    public void SetUpScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerSpawnpoint != null)
        {
            GameObject player = playerSpawnpoint.SpawnObject();

            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
