using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public static LevelManager LevelManager {get; private set;}
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 

        LevelManager = FindObjectOfType<LevelManager>();
        if (LevelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }
}