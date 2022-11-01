using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Hidden enums & variables
    public enum STATUS{ PLAY, PAUSED, ZAWARUDO };
    private Scene scene;

    [Header("Config & variables")]
    [SerializeField] public int framerate = 60;
    public float localTimeScale = 1f;
    public string sceneName;
    public STATUS status;

    [Header("Dictionaries of objects")]
    public Dictionary<string, GameObject> CanvasDictionary = new Dictionary<string, GameObject>();
    public List<GameObject> GameObjectsToPooling;


    void Start(){
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        Application.targetFrameRate = framerate;
        CreatePools();
    }

    void CreatePools(){
        foreach(GameObject obj in GameObjectsToPooling){
            ObjectPooling.PreLoad(obj,10);
        }
    }
}