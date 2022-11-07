using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet : MonoBehaviour
{
    public enum TypeBullet{ ENEMY, PLAYER };
    
    [Header("Estadistas")]
    public GameObject collideEffect;
    public float localTimeScale = 1f;
    public TypeBullet typeBullet = TypeBullet.ENEMY;
    public bool activeRotation = true;
    public float radial = 0f;
    public float speed  = 1f;
    public float damage = 10f;
    public Vector3 updatePosition;
    public List<GameObject> collisionSpawn; // Cositas que spawneariamos al colisionar

    // Constructor de Bullet customizado
    public void SetBullet(TypeBullet _typeBullet, bool _activeRotation, float _radial, float _speed, float _damage){
        // Debug.Log("SetBullet("+_typeBullet+" "+_activeRotation+" "+_radial+" "+_speed+" "+_damage);
        this.typeBullet = _typeBullet;
        this.activeRotation = _activeRotation;
        this.radial = _radial;
        this.speed = _speed;
        this.damage = _damage;
        this.localTimeScale = 1f;
        Start();
    }

    void Start(){
        CalculateUpdatePosition();
        if(activeRotation){ transform.eulerAngles = new Vector3(0f, 0f, radial); }
    }

    void Update(){
        PlayerBulletMovement();
    }

    void PlayerBulletMovement(){
        transform.position += updatePosition * ( localTimeScale * Time.deltaTime);
    }

    public void CalculateUpdatePosition(){
        float x = speed/2;
        float y = speed/2;
        float angle = radial + 45; // + distorsion; // 1,1 por defecto es 45 grados, por lo que mejor parchearlo aquí
		float rad = angle * Mathf.PI/180;
		float newX = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
		float newY = y * Mathf.Cos(rad) + x * Mathf.Sin(rad);
        updatePosition = new Vector3(newX, newY, 0f);
    }


    public void OnTriggerEnter2D(Collider2D other){
        Debug.Log("OnTriggerEnter2D: "+other.gameObject.tag);
        switch(other.gameObject.tag){
            case "Player":      DamageCollisionPlayer(other);    break;
            case "Enemy":       DamageCollisionEnemy(other);     break;
            // case "Cloth":       DamageCollisionClothes(other);   break;
            // case "Effector":    DamageCollisionEffect(other);    break;
            default: Debug.LogWarning("Bullet > OnTriggerEnter2D > No configurado"); break;
        }
    }

    public void DamageCollisionPlayer(Collider2D other){
        if(typeBullet==TypeBullet.ENEMY){
            if( other.gameObject.CompareTag("Player") ){
                other.GetComponent<PlayerManager>().DealDamage(damage);
            }
        }
    }

    public void DamageCollisionEnemy(Collider2D other){
        if(typeBullet==TypeBullet.PLAYER){
            other.GetComponent<Enemy>().DealDamage(damage);
            InstanceCollideEffect();
        }
    }

    // Cuando te choques con algo instanciamos una explosión o algo
    public void InstanceCollideEffect(){
        Debug.Log("-- InstanceCollideEffect");
        // Codigo inspirado en PlayerShooterController.cs
        if(collideEffect!=null){
            GameObject go = ObjectPooling.GetObject(collideEffect); // Si no le pasas el transform position no sabe donde rehubicarlo
            go.transform.position = transform.position;
            StartCoroutine(ObjectPooling.DeSpawn(collideEffect, go, 1f));
        }else{
            Debug.Log("No tenemos colisionador");
        }
    }

/***
    
    public void DamageCollisionClothes(Collider2D other){
        if(typeBullet=="PLAYER"){}
    }
    public void DamageCollisionEffect(Collider2D other){
        if(typeBullet=="PLAYER"){}
    }
***/
}

/**
    public float localTimeScale = 1f; // La escala local de tiempo
    public float radial = 0f;
    public float speed = 10f;
    public float damage = 10f;
    public Vector3 updatePosition;
    public GameObject bulletExplosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        CalculateUpdatePosition();
        transform.eulerAngles = new Vector3( 0f, 0f, radial);    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBulletMovement();
    }

    void CalculateUpdatePosition(){
        float x = speed/2;
        float y = speed/2;
        float angle = radial + 45; // + distorsion; // 1,1 por defecto es 45 grados, por lo que mejor parchearlo aquí
		float rad = angle * Mathf.PI/180;
		float newX = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
		float newY = y * Mathf.Cos(rad) + x * Mathf.Sin(rad);
        updatePosition = new Vector3(newX, newY, 0f);
    }

    void PlayerBulletMovement(){
        transform.position += updatePosition * ( localTimeScale * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        // Comportamiento contra enemigos
        if(other.gameObject.CompareTag("Player")){
            // Debug.Log( gameObject.name + " esta colisionando con el Player");
            other.gameObject.GetComponent<PlayerManager>().DealDamage(damage);
        }

        if(other.gameObject.CompareTag("PlayerAttractor")){
            // Debug.Log("Estamos colisionando con el Player Attractor");
            // other.gameObject.GetComponent<PlayerManager>().DealDamage(damage);
        }        
    }

    public void KillBullet(){
        // Eliminamos la bala y ponemos una animación con su color y rotación original
        Instantiate(bulletExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    // Variables
    public float updateY = 0f;
    public float updateX = 0f;
    public float damage = 0f;
    public float autoDestroyTime = 10f;

    // Components
    private Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();

        Destroy(gameObject, autoDestroyTime);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3( updateX, updateY, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other){
        // Comportamiento contra enemigos
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.GetComponent<Enemy>().DealDamage(damage, transform.position);
            Destroy(gameObject);
        }

        // Comportamiento contra ropa
        if(other.gameObject.CompareTag("Clothing")){
            other.gameObject.GetComponent<ClothesManager>().DealDamage(damage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("BossWeakPoint")){
            other.gameObject.GetComponent<BossWeakPoint>().DealDamage(damage);
            Destroy(gameObject);
        }
    }
    
**/
