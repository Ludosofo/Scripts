using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet : MonoBehaviour
{
    public enum TypeBullet { ENEMY, PLAYER };

    [Header("Estadistas")]
    public GameObject collideEffect;
    public float localTimeScale = 1f;
    public TypeBullet typeBullet = TypeBullet.ENEMY;
    public bool activeRotation = true;
    public float angleDirection = 0f;
    public float speed = 1f;
    public Vector2 damage = new Vector2(10f, 10f); // Daño minimo y daño maximo
    public Vector3 updatePosition;
    public List<GameObject> collisionSpawn; // Cositas que spawneariamos al colisionar

    // Constructor de Bullet customizado
    public void SetBullet(TypeBullet _typeBullet, bool _activeRotation, float _angleDirection, float _speed, Vector2 _damage)
    {
        // Debug.Log("SetBullet("+_typeBullet+" "+_activeRotation+" "+_angleDirection+" "+_speed+" "+_damage);
        this.typeBullet = _typeBullet;
        this.activeRotation = _activeRotation;
        this.angleDirection = _angleDirection;
        this.speed = _speed;
        this.damage = _damage; // Esto tiene que ser un vector2
        this.localTimeScale = 1f;
        CalculateUpdatePosition();
        Start();
    }

    public void SetAngleDirection(float _angleDirection){
        this.angleDirection = _angleDirection;
        CalculateUpdatePosition();
    }

    void Start()
    {
        CalculateUpdatePosition();
        if (activeRotation) { transform.eulerAngles = new Vector3(0f, 0f, angleDirection); }
    }

    void Update()
    {
        PlayerBulletMovement();
    }

    void PlayerBulletMovement()
    {
        transform.position += updatePosition * (localTimeScale * Time.deltaTime);
    }

    public void CalculateUpdatePosition()
    {
        float x = speed / 2;
        float y = speed / 2;
        float angle = angleDirection + 45; // + distorsion; // 1,1 por defecto es 45 grados, por lo que mejor parchearlo aquí
        float rad = angle * Mathf.PI / 180;
        float newX = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
        float newY = y * Mathf.Cos(rad) + x * Mathf.Sin(rad);
        updatePosition = new Vector3(newX, newY, 0f);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Bullet>OnTriggerEnter2D():: " + other.gameObject.name);
        switch (other.gameObject.tag)
        {
            case "Player": DamageCollisionPlayer(other); break;
            case "Enemy": DamageCollisionEnemy(other); break;
            case "Cloth": DamageCollisionClothes(other); break;
            case "Effect": DamageCollisionEffect(other); break;
            default: Debug.LogWarning("Bullet > OnTriggerEnter2D > No configurado"); break;
        }
    }

    public void DamageCollisionPlayer(Collider2D other)
    {
        if (typeBullet == TypeBullet.ENEMY)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("DamageCollisionPlayer()");
                /// other.GetComponent<PlayerManager>().DealDamage(damage);
            }
        }
    }

    public void DamageCollisionEnemy(Collider2D other)
    {
        if (typeBullet == TypeBullet.PLAYER && this.gameObject!=null)
        {
            int damageInt = (int) Random.Range(damage[0], damage[1]);
            other.GetComponent<Enemy>().TakeDamage(damageInt, this.transform.position);

            // InstanceCollideEffect(); // <---- ¿QUE DEMONIOS HACE ESTO AQUÍ?
        }
    }

    private void DamageCollisionClothes(Collider2D other)
    {
        Debug.Log("TODO:DamageCollisionClothes");
    }

    private void DamageCollisionEffect(Collider2D other)
    {
        Debug.Log("TODO:DamageCollisionEffect");
    }

    // Cuando te choques con algo instanciamos una explosión o algo
    public void InstanceCollideEffect()
    {
        Debug.Log("InstanceCollideEffect");
        // Codigo inspirado en PlayerShooterController.cs
        if (collideEffect != null)
        {
            GameObject go = ObjectPooling.GetObject(collideEffect); // Si no le pasas el transform position no sabe donde rehubicarlo
            go.transform.position = transform.position;
            // StartCoroutine( ObjectPooling.RecicleObject(collideEffect, go)); // , 1f));
        }
        else
        {
            Debug.Log("No tenemos colisionador");
        }
    }
}
