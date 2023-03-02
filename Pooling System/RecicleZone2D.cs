using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyOnGround : MonoBehaviour
{
    // OBJETIVO: Reciclar objeto cuando se encuentre con el colisionador 2D

    public string prompt = "PlayerBullet";

    private void OnCollisionEnter2D(Collision other){
        if (isTagOrNameInTarget(other)) {
            other.transform.gameObject.SetActive(false);
        }
    }

    private bool isTagOrNameInTarget(Collision other){
        return other.gameObject.tag.Equals( prompt, StringComparison.InvariantCulture)
            || other.gameObject.name.Equals(prompt, StringComparison.InvariantCulture);
    }


    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground", StringComparison.InvariantCulture))
        {

            // TODO
            // Add points
            StartCoroutine(MarkInactive(2.0f));

        }
    }


    IEnumerator MarkInactive( float time)
    {

        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);

    }

    */

}
