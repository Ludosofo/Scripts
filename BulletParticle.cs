using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    // SOURCE: https://www.youtube.com/watch?v=lkq8iLOr3sw
    // DEFINE: Aplicar logica de bala a las particulas

/*
    public ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    public object sparks;

    private void OnParticleCollision(GameObject other){
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        Debug.Log("Colision detectada");
        for(int i = 0; i < events; i++){

            if(sparks!=null){
                Instantiate(sparks, colEvents[i].intersection, Quaternion.LookRotation(colEvents[i].normal));
            }
        }
    }
    */
}
