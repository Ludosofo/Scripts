using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Experimental.Rendering.Universal; // TODO: La zona de ChangeColor parece estar relacionada 
// using ObjectPooling;

public class PlayerShooterController : MonoBehaviour {

    [Header("Test")]
    public bool canShootBullet = true;
    public float cooldownTime = 0.15f;
    [SerializeField] GameObject bullet; // SerializeField: Es para que se vea y no se pueda tocar

    public float timeActive = 1f;
    public float speed = 10f;
    public float width = 1f; // Area en la que se iniciaran los disparos ¿WHAT?
    public Color color = new Color (1f, 1f, 1f, 1f);

    public List<float> shoots = new List<float>(); // Cantidad de disparos y dirección

        public Gradient gradient;
        public float timeGradient = 1.5f;
        public float timer = 0f;


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton (0) && canShootBullet) { ShootBullets (); }
        TimerController ();
    }

    IEnumerator Cooldown (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        canShootBullet = true;
    }

    void ShootBullets () {
        for (int i = 0; i < shoots.Count; i++) {
            CreateBulletWithRadialAndDestroy (bullet, shoots[i], timeActive);
        }
        canShootBullet = false;
        StartCoroutine (Cooldown (cooldownTime));
    }

    void CreateBulletWithRadialAndDestroy (GameObject primitive, float radial, float time) {
        
        GameObject go = ObjectPooling.GetObject(bullet);
        go.transform.position = transform.position;
        go.GetComponent<Bullet>().SetBullet(Bullet.TypeBullet.PLAYER, true, radial, speed, 10f);
        StartCoroutine (ObjectPooling.DeSpawn (primitive, go, time));

    }
    /* 
        void ChangeColor(GameObject go){
            go.GetComponent<SpriteRenderer>().color = gradient.Evaluate(timer/timeGradient);
            go.GetComponent<Light2D>().color = gradient.Evaluate(timer/timeGradient);
        }
     */

    void TimerController(){
            timer += Time.deltaTime;
            if(timer>timeGradient){
                timer = 0f;
            }
        }
    /*     // 90º y 3 // -45, 0, 45
        float CalcActualDegree(int shoot){
            return shooters<=1 ? 0f : degrees/2 - shoot*(degrees/(shooters-1));
        }
    */

}