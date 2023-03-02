using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialController : MonoBehaviour
{

    public SpriteRenderer sr;
    public Material baseMaterial;
    public Material whiteMaterial;
    public float timer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (sr == null) {
            sr = this.gameObject.GetComponent<SpriteRenderer>();
        }
        if (sr != null) {
            baseMaterial = sr.material;
        }

        ActivateParpadeo();
    }

    // Update is called once per frame
    public void ActivateParpadeo(){
        StartCoroutine(CoroutineChangeMaterial());
    }

    IEnumerator CoroutineChangeMaterial(){
        sr.material = baseMaterial;
        yield return new WaitForSeconds(0.1f);
        sr.material = whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        sr.material = baseMaterial;
    }
}
