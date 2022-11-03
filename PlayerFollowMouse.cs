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
    
    private Vector3 playerPosition;

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
        sr              = GetComponent<SpriteRenderer>();
        rb2D            = GetComponent<Rigidbody2D>();
        gameController  = GameObject.Find("Game").GetComponent<GameController>();
        mainCamera      = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        UpdateMousePositionInCamera();
        rb2D.MovePosition( playerPosition );
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    void UpdateMousePositionInCamera(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector3( mousePosition[0], mousePosition[1], -5f);
    }

    IEnumerator FixPlayerInsideGame(){
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3( mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
    }
}
