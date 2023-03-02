using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public string txt = "Hola mundo";
    public float speed = 1f;
    public Vector3 movement;

    void Update(){
        CalcMovement();
        transform.position += movement * speed;
    }

    public void CalcMovement()
    {
        Debug.Log("Aquí no hay nada");
    }
}
