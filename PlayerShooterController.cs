using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Experimental.Rendering.Universal; // TODO: La zona de ChangeColor parece estar relacionada 
// using ObjectPooling;

public class PlayerShooterController : MonoBehaviour {

    public bool canShoot = true;
    public bool shooting = false;

    public float cooldown = 0.33f;          // Tiempo de disparo
    [SerializeField] GameObject bullet;     // [SerializeField] para no tocar la bala
    Spawner spawner;                        // Clase spawner

    [Header("Propiedades de las balas")]
    public float speed = 10f;
    public Color color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
    public float timeActive = 1.0f;
    public float directionsToShoot;
    public int bulletCount = 0; // Vamos a contar las balas que disparamos ¿por qué no?

    [Header("Propiedades del disparo")]
    public float angleRange = 30f;
    public int numberOfBullets = 5;
    public List<Vector3> listShootingPositionAndAngles; // 0,1 son posiciones relativas y 2 el angulo



    public void Start() {
        // PoolingSystem
        spawner = GameObject.Find("PoolingSystem").GetComponent<Spawner>();
        
        // Configuración de un triple shooter
        // SetListPositionsTriple();
    }

    public void Update()
    {


        SetListPositionCustom(0f, angleRange, numberOfBullets);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shooting = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shooting = false;
        }


        if (shooting)
        {
            Shoot();
        }
    }

    public void Shoot() {
        if (canShoot)
        {
            Debug.Log("Disparamos!!!");
            CreateBullets();
        }
    }

    IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

    private void CreateBullets()
    {
        for (int n = 0; n < listShootingPositionAndAngles.Count; n++) {
            CreatePlayerBulletWithPoolingSystem(n);
            StartCoroutine(Cooldown(cooldown));
            canShoot = false;
            bulletCount++;
            
        }
    }


    private void SetListPositionsTriple()
    {
        listShootingPositionAndAngles.Add(new Vector3( 0.0f,  0.0f,  0.0f));
        listShootingPositionAndAngles.Add(new Vector3(-0.3f, -0.2f,  2.0f));
        listShootingPositionAndAngles.Add(new Vector3( 0.3f, -0.2f, -2.0f));
    }

    private void SetListPositionCustom(float direction, float angleRange, int numberOfBullets)
    {
        float steps = angleRange / numberOfBullets;
        listShootingPositionAndAngles.Clear();
        for (int n = 0; n <= numberOfBullets; n++){
            float calculateAngle = -(angleRange / 2) + (n * steps); 
            listShootingPositionAndAngles.Add(new Vector3(0f, 0f, calculateAngle));
        }
    }

    // Sistema de balas basado en Pooling
    private void CreatePlayerBulletWithPoolingSystem(int n){
        
        GameObject c = spawner.SpawnPlayerBullet();// ObjectPooling.GetObject(bullet);
        c.name = "PlayerBulletPooling";
        
        // c.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        c.transform.position = new Vector3(
            transform.position.x + listShootingPositionAndAngles[n][0],
            transform.position.y + listShootingPositionAndAngles[n][1],
            transform.position.z
        );

        SpriteRenderer s = c.transform.GetComponent<SpriteRenderer>();
        s.color = this.color;
        
        Bullet b = c.transform.GetComponent<Bullet>();
        b.SetAngleDirection(listShootingPositionAndAngles[n][2]); // angle en el vector3
        b.speed = speed;
        b.CalculateUpdatePosition();

        StartCoroutine( spawner.DeSpawn( spawner.playerBullet, c, timeActive));
    }


    /**
    Vector3 infoBullet = listShootingPositionAndAngles[n];
    ///// GameObject go = ObjectPooling.GetObject(bullet);
    Bullet bullet = go.GetComponent<Bullet>();
    
    go.transform.position = transform.position;
    bulletScript.SetBullet(Bullet.TypeBullet.PLAYER, false, infoBullet[2], speed, 10f);
    bulletScript.CalculateUpdatePosition();


    // RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    StartCoroutine(ObjectPooling.RecicleObject(primitive, go));
    **/



    /***
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
            CreateBulletWithEmiter();
            // CreateBulletWithRadialAndDestroy (bullet, shoots[i], timeActive);
        }
        canShootBullet = false;
        StartCoroutine(Cooldown(cooldownTime));
    }

    void CreateBulletWithEmiter() { 
    
    }

    void TimerController()
    {
        timer += Time.deltaTime;
        if (timer > timeGradient)
        {
            timer = 0f;
        }
    }
    ***/


    /*
     * ANTERIORMENTE
     * Creabamos balas con un sistema de pool
     * Calculabamos las radiales de forma relativa y más
     * Este metodo vamos a sustituirlo por gameObjects que sean efectos de particulas
     * Dejo este codigo por si en el futuro necesito revisarlo
     * 
    void CreateBulletWithRadialAndDestroy (GameObject primitive, float radial, float time) {
        
        GameObject go = ObjectPooling.GetObject(bullet);
        go.transform.position = transform.position;
        go.GetComponent<Bullet>().SetBullet(Bullet.TypeBullet.PLAYER, true, radial, speed, 10f);
        StartCoroutine (ObjectPooling.DeSpawn (primitive, go, time));

    }
    */


    /* 
        void ChangeColor(GameObject go){
            go.GetComponent<SpriteRenderer>().color = gradient.Evaluate(timer/timeGradient);
            go.GetComponent<Light2D>().color = gradient.Evaluate(timer/timeGradient);
        }
     */
    /*     // 90º y 3 // -45, 0, 45
        float CalcActualDegree(int shoot){
            return shooters<=1 ? 0f : degrees/2 - shoot*(degrees/(shooters-1));
        }
    */

}