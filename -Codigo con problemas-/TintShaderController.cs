using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintShaderController : MonoBehaviour
{
    /* Extraido de Code Monkey */
    /* Por algún mmotivo el codigo se desconecta */
    public Material material;
    public Color colorTint;

    private void Awake()
    {
        SetMaterial(this.gameObject.GetComponent<SpriteRenderer>().material);
    }

    private void Update() {
        material.SetColor("_Color", colorTint);
        material.SetColor("_Tint", colorTint);
    }

    public void SetMaterial(Material material)
    {
        this.material = material;
    }



    /*
    private void Update()
    {
        // Intentamos establecer color actualmente en materialTintColor en el material individual
        SetTintColor(materialTintColor);

        if (materialTintColor.a > 0)
        {
            Debug.Log("materialTintColor.a activado");
            materialTintColor.a = Mathf.Clamp01(materialTintColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor("_Tint", materialTintColor);
        }
    }



    public void SetTintColor(Color color)
    {
        materialTintColor = color;
        material.SetColor("_Tint", materialTintColor);
    }

    public void SetTintFadeSpeed(float tintFadeSpeed)
    {
        this.tintFadeSpeed = tintFadeSpeed;
    }
    */
}
