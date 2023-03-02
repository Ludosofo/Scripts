using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float timer;
    public float timeEnding = 2f;
    public Vector2 size = new Vector2(12f, 36f);
    public float textSize;
    public float lerp;
    public Gradient gradient;

    public Spawner spawner;

    void Start(){
        spawner = GameObject.Find("PoolingSystem").GetComponent<Spawner>();
    }

    void Update() {

        lerp = timer / timeEnding;

        if (timer < timeEnding){
            textSize = Mathf.Lerp(size[0], size[1], lerp);
            SetColor( gradient.Evaluate(lerp));
            SetFontSize(textSize);
        }

        if(timer > timeEnding){
            timer = 0f;
            DeactiveObjectPooling();
        }

        timer += Time.deltaTime;
    }

    public void SetColor(Color color){
        gameObject.GetComponent<TextMeshProUGUI>().color = color;
    }

    public void SetFontSize(float size){
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = size;
    }


    public void SetText(int number){
        // No sé puede crear una referencia porque entonces crea problemas de instancia
        gameObject.GetComponent<TextMeshProUGUI>().SetText(number.ToString());
    }

    public void UpdatePositionInCanvas(Vector3 positionInCamera){
        // Debug.Log("UpdatePositionInCanvas(Vector3 "+positionInCamera+")");
        transform.position = Camera.main.WorldToScreenPoint(positionInCamera );
    }

    public void DeactiveObjectPooling(){
        ObjectPooling.RecicleObject( spawner.damageText, gameObject);
    }

    /*
    public Gradient GetGrandientStyle(int style){
        switch (style){
            case 0: return ; break;
            case 1: return ; break;
            case 2: return ; break;
            default: return GetGrandientStyle(0); break;
        }
    }
    

    public Vector2 GetSizeFontStyle(int style)
    {
        switch (style)
        {
            case 0: return new Vector2(20f, 20f);break;
            case 1: return new Vector2(24f, 24f);break;
            case 2: return new Vector2(24f, 48f);break;
            case 3: return new Vector2(52f, 96f);break;
            default: GetSizeFontStyle(0); break;
        }
    }
    */

}
