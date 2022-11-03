using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum StatusGame{ MENU, PLAY, PAUSE, GAMEPAUSE };
    public enum StatusPlayer{ NORMAL, DEATH, INMORTAL };
    public StatusGame statusGame;
    public StatusPlayer statusPlayer;
    public int frameRate = 60;
    
    public List<GameObject> objectsToPool;

    void Awake(){
        Application.targetFrameRate = frameRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPools();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateObjectPools(){
        foreach(GameObject obj in objectsToPool){
            ObjectPooling.PreLoad(obj,1);
        }
    }
}
