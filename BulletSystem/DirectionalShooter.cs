using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooter : MonoBehaviour
{
    // Yonky, si lees esto en el futuro, continua trabajando para escapar de los boomers
    // Estoy escribiendo esto para subir como minimo algo
    // Yonky del 22/12/2022, me he distraido, lo siento, pero continuaré con el trabajo

    // private float x;
    // private float y;
    // public float radial = 0f;                               // De 0 a 360 grados
    // public float distorsion;                                // Se le añade la rotación del objeto
    public Vector2 direction = new Vector2(0f, 0f);
    public GameObject bullet;
    public float childLifeTime = 4f;
    public float speed = 1f;
    public Vector2 damage = new Vector2(1f,0f); // Las balas enemigas no tienen rango de daño

    public Spawner spawner;


    void Start()
    {
        spawner = GameObject.Find("PoolingSystem").GetComponent<Spawner>();
        InvokeRepeating("Shoot", 5f, 0.1f);
    }

    void Update(){
        // distorsion = transform.eulerAngles[2];
    }

    /*
    public void CalculateDirectionWithRadius(){
        x = speed/2;
        y = speed/2;
        float angle = radial + 45 + distorsion; // 1,1 por defecto es 45 grados, por lo que mejor parchearlo aquí
		float rad = angle * Mathf.PI/180;
		float newX = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
		float newY = y * Mathf.Cos(rad) + x * Mathf.Sin(rad);
        direction = new Vector2(newX, newY); 
    }
    */

    public void Shoot(){
        if(bullet){
            float timeToRecycle = 2f;
            CreateBulletWithRadialAndDestroy(bullet, transform.eulerAngles.z-7.5f, timeToRecycle);
            CreateBulletWithRadialAndDestroy(bullet, transform.eulerAngles.z-3.25f, timeToRecycle);
            CreateBulletWithRadialAndDestroy(bullet, transform.eulerAngles.z, timeToRecycle);
            CreateBulletWithRadialAndDestroy(bullet, transform.eulerAngles.z+3.25f, timeToRecycle);
            CreateBulletWithRadialAndDestroy(bullet, transform.eulerAngles.z+7.5f, timeToRecycle);
        }
    }

    void CreateBulletWithRadialAndDestroy(GameObject primitive, float radial, float time){
        GameObject go = ObjectPooling.GetObject(bullet); // Si no le pasas el transform position no sabe donde rehubicarlo
        Bullet bulletScript = go.GetComponent<Bullet>();
        go.transform.position = transform.position;
        bulletScript.SetBullet( Bullet.TypeBullet.ENEMY, true, radial , speed, damage);
        bulletScript.CalculateUpdatePosition();
        StartCoroutine( spawner.DeSpawn( spawner.playerBullet, go, childLifeTime));
    }
}
