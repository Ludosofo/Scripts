using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    // SOURCE: https://www.youtube.com/watch?v=lkq8iLOr3sw
    // DEFINE: Aplicar logica de bala a las particulas
    // public ParticleSystem particleSystem; List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>(); private void OnParticleCollision(GameObject other){ particleSystem.GetCollisionEvents(other, c){} }

    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] aliveParticles;
    public List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    public object sparks;

    private void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            particleSystem.Play();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            particleSystem.Stop();
        }
    }

    private void OnParticleCollision2D(GameObject other)
    {
        Debug.Log(other.tag);

        int events = particleSystem.GetCollisionEvents(other, colEvents);

        Debug.Log("Colision detectada");
        for (int i = 0; i < events; i++)
        {

            if (sparks != null)
            {
                // Instantiate(sparks, colEvents[i].intersection, Quaternion.LookRotation(colEvents[i].normal));
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnParticleTrigger()
    {
        // enter es una lista de particulas
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        
        Debug.Log("OnParticleTrigger—>numEnter:"+numEnter);
        for (int i = 0; i < numEnter; i++)
        {
            Debug.Log("Hemos conseguido detectar la entrada!!!");
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }

        // re-assign the modified particles back into the particle system
        particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }


}
