using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    public GameObject playerBullet;
    [SerializeField]
    public GameObject enemyBullet;
    [SerializeField]
    public GameObject damageText;

    // public float timer = 0f;

    // Start is called before the first frame update

    public static void PrintHola(){
        Debug.Log("Hola mundo soy Spawner.cs");
    }

    private void Start()
    {
        // Precargamos unos 10 porque si vamos a hacer pooling es para grandes cantidades
        ObjectPooling.PreLoad(playerBullet, 1);
        ObjectPooling.PreLoad(enemyBullet, 1);
        ObjectPooling.PreLoad(damageText, 1);

        // El DamageText tiene que estar dentro del canvas
        GameObject.Find("DamageText Pool").transform.SetParent(GameObject.Find("[Graphy]").transform);
    }

    // Update is called once per frame
    private void Update()
    {
        // if (timer > 1.5f) { SpawnGameObject(playerBullet); timer = 0f; } timer += Time.deltaTime;
        // SpawnGameObject(enemyBullet);


    }

    public GameObject SpawnPlayerBullet() {
        return SpawnGameObject(playerBullet);
    }

    public GameObject SpawnDamageText() {
        return SpawnGameObject(damageText);
    }

    public GameObject SpawnGameObject(GameObject go){
        GameObject c = ObjectPooling.GetObject(go);
        c.transform.position = RandomSpawnPosition();
        // StartCoroutine(DeSpawn(go, c, 2.0f)); // NO ACTIVAR PORQUE TOCA CON OTRO
        return c;
    }

    private Vector3 RandomSpawnPosition(){
        return new Vector3(Random.Range(-10.0f, 10.0f), 0.5f, Random.Range(-10.0f, 10.0f));
    }

    public IEnumerator DeSpawn(GameObject primitive, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        // Debug.Log("Desactivamos el objecto: >"+go.name);
        ObjectPooling.RecicleObject(primitive, go);

    }

}
