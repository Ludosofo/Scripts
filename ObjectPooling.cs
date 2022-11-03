using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    // Tutorial: https://www.youtube.com/watch?v=ZYs5RwHyTSU
    
    public static ObjectPooling instance;
    public static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>> ();
    public static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject> ();

    // Start is called before the first frame update
    void Awake () {
        if (instance == null) { instance = this; }else { Destroy(this); }
    }

    // Precargamos el objeto unas cuantas veces
    public static void PreLoad (GameObject objectToPool, int amount)
    {
        Debug.Log("PreLoad(GameObject name: "+objectToPool.name+", int amount: "+amount+")");
        int id = objectToPool.GetInstanceID();
        GameObject parent = new GameObject();
        parent.name = objectToPool.name + " Pool";
        parents.Add(id, parent); // Lo añadimos al diccionario de los padres
        pool.Add(id, new Queue<GameObject>()); // Lo añadimos al diccionario

        // Creamos todos los objetos que nos requieren y creamos que vayamos a necesitar
        for (int i = 0; i < amount; i++) {
            CreateObject (objectToPool);
        }
    }

    static void CreateObject(GameObject objectToPool)
    {
        Debug.Log("CreateObject(GameObject objectToPool: "+objectToPool.name+")");
        int id = objectToPool.GetInstanceID(); // GetInstanceID nos da la ID de Unity para funcionar
        GameObject go = Instantiate(objectToPool) as GameObject; // Instancia el objeto y obtiene go = gameObject
        go.transform.SetParent(GetParent (id).transform); // Obtiene el padre con la ID ¿¿¿¿COMO????
        go.SetActive (false);

        pool[id].Enqueue (go);
    }

    static GameObject GetParent(int parentID){
        Debug.Log("GetParent(int parentID: "+parentID+")");
        GameObject parent; // Variable local
        parents.TryGetValue (parentID, out parent); // <------ ¿COMO FUNCIONA ESTO?!!!
        // <------ 
        return parent; // Devolvemos la variable local
    }

    // Dame un objeto / crea uno nuevo si no hay
    public static GameObject GetObject(GameObject objectToPool)
    {
        Debug.Log("GetObject(GameObject objectToPool: "+objectToPool.name+")");
        int id = objectToPool.GetInstanceID();
        Debug.Log ("--id:" + id);
        if (pool[id].Count == 0) { CreateObject(objectToPool); }
        GameObject go = pool[id].Dequeue ();
        go.SetActive (true);
        return go;
    }

    public static void RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    {
        Debug.Log("RecicleObject(GameObject objectToPool: "+objectToPool.name+", GameObject objectToRecicle: "+objectToRecicle.name+")");
        int id = objectToPool.GetInstanceID();
        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive (false);
    }

    public static IEnumerator DeSpawn(GameObject primitive, GameObject _gameObject, float time) {
        Debug.Log("DeSpawn(GameObject primitive: "+primitive.name+", GameObject _gameObject: "+_gameObject.name+", float time: "+time+")");
        yield return new WaitForSeconds (time);
        ObjectPooling.RecicleObject(primitive, _gameObject);
    }
}