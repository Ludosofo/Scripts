using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{

    // Componentes requeridos
    public Camera mainCamera;
    public GameController gameController;
    public Rigidbody2D rb2D;
    public SpriteRenderer sr;


    public enum ControllerTypes{ MOUSE, KEYBOARD }
    public ControllerTypes TypeControl;

    public Vector3 playerPosition;

    void Awake()
    {
        // Antes aquí estableciamos sr y rb2D
    }

    void Start()
    {
        SetComponents();
        StartCoroutine("FixPlayerInsideGame");
    }

    void SetComponents(){
        if (sr == null) { sr = GetComponent<SpriteRenderer>(); }
        if (rb2D == null) { rb2D = GetComponent<Rigidbody2D>(); }
        if (gameController == null) { gameController = GameObject.Find("Game").GetComponent<GameController>(); }
        if (mainCamera == null) { mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();  }
    }

    void FixedUpdate()
    {
        if (TypeControl == ControllerTypes.MOUSE)
        {
            UpdateMousePositionInCamera();
            rb2D.MovePosition(playerPosition);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else {
            Debug.LogWarning("TODO: Tienes que hacer el control por mando");
        }
    }

    void UpdateMousePositionInCamera(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector3( mousePosition[0], mousePosition[1], -9);
    }

    IEnumerator FixPlayerInsideGame(){
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3( mainCamera.transform.position.x, mainCamera.transform.position.y, -9);
    }
}
