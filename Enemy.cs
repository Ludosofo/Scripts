using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public enum Direction {North, East, South, West};
    
    [Header("Health Settings")]
    public string name = "Enemy";
    public int maxLife = 1;
    public int life = 1;
    public float localTimeScale = 1f;
    public List<GameObject> listDrop;

    protected void Awake(){
        this.life = this.maxLife; 
    }

    public void AddLife(int damage){
        life += damage;
    }

    private void CheckAlive(){
        if(life<0){
            SendListDrop();
            Destroy(this.gameObject);
        }
    }

    private void SendListDrop(){
        // TODO: Enviar información a un generador de drops
    }
}


/*** Codigo antiguo

    [Header("Estadistas del enemigo")]
    public bool enemyCollisionDamage = true; // Decide si la colision daña al jugador
    public int status = 0;
    public string enemyName = "enemy"; // Nombre del enemigo
    public float life = 200f; // Vida del personaje
    public float defense = 0f; // Porcentaje que bajara la interpretacion de vida
    
    // Auto Set
    public float maxLife = 0f; // Auto Set, solo para tener una referencia
    
    // Datos para alteraciones de estado
    public float localTimeScale = 1f; // Altera TODOS LOS DELTA TIME QUE UTILICE CON ENEMIES

    // Instancias
    [Header("GameObjects")]
    public GameObject itemDrop;                     // Objeto que instanciara al morir
    public GameObject damageText;                   // Objeto con un TextMesh para representar el daño
    public GameObject thisEnemy;                    // Objeto que destruir al morir
    // Componentes clave
    
    [Header("Components")]
    public Collider2D collider;                  // El componente que maneja las colisiones simples
    public EnemyMaterialManager materialManager; // Controlador de materials
    public EnemyMovementManager movementManager; // Controlador global de movimientos, lo tenemos aquí para manejar fuerzas
    
    // Default
    private Vector3 nullVector3 = new Vector3(0f,0f,0f);

    // Death Configuration
    public GameObject dieAnimation; // Objeto que se instancia al morir; explosión basica / explosión constante / explosión de carne / explosión de energia / etc...
    public float timeToDie = 0f; // Tiempo que va a tardar el enemigo en morir (usado para que los snakeManager no se bugeen)
    public bool isDead = false; // Para evitar colisiones

    // CODIGO SIN USAR <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    // public float acumulativeDamage = 0f; // Daño acumulado entre la ultima representación
    // public float pointsToDie = 100; // Puntos al morir
    // public GameObject gameController; // Comunicación con el controlador de juego principal
    
    // Start is called before the first frame update
    void Start()
    {
        maxLife = life; // La vida inicial será el indicador de vida maxima
        
        // Seteamos solo si no tenemos
        if(materialManager==null){ materialManager = GetComponent<EnemyMaterialManager>(); }
        if(movementManager==null){ movementManager = GetComponent<EnemyMovementManager>(); }
        if(collider==null){ collider = GetComponent<Collider2D>(); }
        if(thisEnemy==null){ thisEnemy = gameObject; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float damage, Vector3 collisionPosition = default(Vector3), int forcedStyle = 0, Vector2 externalForces = default(Vector2)){
        // Debug.Log("DealDamage(damage: "+damage+", collisionPosition: "+collisionPosition+", forcedStyle: "+forcedStyle+", externalForces: "+externalForces+")");
        
        // SELECCIÓN DEL ESTILO DE DAÑO
        int _style = 0;
        if(forcedStyle != 0){
            _style = forcedStyle; // Forzar un estilo
        }else if(damage<=3f){
            _style = 0;
        }else if(damage>3f && damage <=5f){
            _style = 1;
        }else if(damage>7f && damage <=9f){
            _style = 2;
        }else if(damage>9f && damage <=12f){
            _style = 3;
        }else if(damage>12f){
            _style = 4;
        }

        // CreateDamageText(damage, _style, collisionPosition);

        if(materialManager != null){
            materialManager.activateWhiteMaterial();
        }

        life += -damage;

        if(life<0){
            StartCoroutine( ExecuteCharacter());    
        }
        
        if(externalForces != null && movementManager !=null){
            movementManager.AddExternalForces(externalForces.x, externalForces.y);
        }
    }

    public IEnumerator  ExecuteCharacter(){

        if(!isDead){
            yield return new WaitForSeconds(timeToDie); // Esto se usa para retrasar la muerte en casos en los que se necesita activar algún codigo especial
            GameObject temp = Instantiate(dieAnimation, transform.position, Quaternion.identity);

            if(thisEnemy==null){ 
                Destroy(gameObject);
            }else{
                Destroy(thisEnemy);
            }
        }
    }

    // Como no podemos usar destroy nativo, creamos esta función para no tener que usar executeCharacter
    public void DirectDestroy(GameObject objectToDestroy){
        Destroy(objectToDestroy);
    }

    void CreateDamageText(float damage, int _style, Vector3 collisionPosition){
        // No  creamos damageText en Enemy.cs
        collisionPosition += new Vector3(0f, 1.2f, 0f);
        GameObject child = Instantiate( damageText, collisionPosition, Quaternion.identity);
        child.GetComponent<DamageText>().text = damage.ToString();
        child.GetComponent<DamageText>().style = _style;
    }

    // Al colisionar con el jugador hacer daño
    private void OnTriggerEnter2D(Collider2D other){
        float damage = 1f;
        
        // Hacer daño al jugador con colision
        if(other.gameObject.CompareTag("Player") && enemyCollisionDamage){
            other.gameObject.GetComponent<PlayerManager>().DealDamage(damage);
        }
    }

    public void DamageText(){

    }

    // ESTO SE USA PARA ALGO ??
    public void SetStatus(float _status){
        switch (_status)
        {
            case 0: break; // Normal
            case 1: break; // Slow-Dying
            case 2: break; // Freeze
            case 3: break; // Burning A.K.A FireEnemy
            case 4: break; // TimeStop
            case 5: break; // 
            case 6: break;
            default: break;
        }
    }

***/