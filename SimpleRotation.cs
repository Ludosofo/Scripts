using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float speed = 0f;
    void LateUpdate(){ transform.eulerAngles += new Vector3(0f,0f,speed)*Time.deltaTime; }
}
