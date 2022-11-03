using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransform : MonoBehaviour
{
    public Vector3 addTransform = new Vector3(0f,0f,0f);
    public Vector3 addRotation = new Vector3(0f,0f,0f);

    void LateUpdate()
    {
        transform.position += addTransform * Time.deltaTime;
        transform.eulerAngles += addRotation * Time.deltaTime;
    }
}

// INFO: addRotation[0] suele caer en un punto de estabilidad, no se recomienda usar