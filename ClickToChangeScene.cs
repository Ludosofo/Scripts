using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ClickToChangeScene : MonoBehaviour
{
    public Button btn;
    public string scene = ""; // { get => scene; set => scene = value; }
    // public Scene level;

    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Cargamos la escena ("+scene+")");
        SceneManager.LoadScene(scene); // LoadSceneMode.Additive);
    }
}
