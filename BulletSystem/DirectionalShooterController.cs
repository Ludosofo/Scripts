using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooterController : MonoBehaviour
{
    public enum TYPE{ LOOPER, TRIGGER };
    public enum STATUS{ RELOADING, READY, SHOOTING };
    public TYPE typeShooter;        // Tipo de shooter
    public STATUS modeShooter;      // Status del disparador

    public GameObject[] objectList; // Objetos que contendrían shooters
    public float timerReload    = 0.99f; // Tiempo de recarga
    public float timerCadence   = 0.33f; // Tiempo entre disparos
    public int numberOfShoots = 3;

    /*
    void Start(){
        SetStatus(0); // Empezamos Ready
    }

    void SetStatus(int newStatus){
        switch(newStatus){
            case 0: StartCoroutine("ModeReady"); break;
            case 1: StartCoroutine("ModeReloading"); break;
            case 2: StartCoroutine("ModeShooting"); break;
            default: break;
        }
    }

    IEnumerator ModeReady(){
        // modeShooter = ;TYPE;
        // if(modeShooter == TYPE.LOOPER){ SetStatus(2); }
    }

    IEnumerator ModeReloading(){
        modeShooter = STATUS.RELOADING;
        yield return new WaitForSeconds(timerReload);
        SetStatus(0); // Ready
    }
    
    IEnumerator ModeShooting(){
        modeShooter = STATUS.SHOOTING;
        ActiveShoot();
    }

    IEnumerator ActiveShoot(){
        for(int i = 0; i < numberOfShoots; i++){
            yield return new WaitForSeconds(timerCadence);
            SendChildsToShoot();
        }
        SetStatus(1); // Reloading
    }

    public void SendChildsToShoot(){
        foreach (GameObject child in objectList) { child.GetComponent <DirectionalShooter>().Shoot(); }
    }

    void OnTriggerStay2D(Collider2D other){
        if( other.CompareTag("Player") && modeShooter = STATUS.READY && modeShooter == TYPE.TRIGGER ){
            SetStatus(2);
        }
    }


    /*
    public string status;               // Informacion para dev
    public GameObject[] objectList;     // Grupo de objetos para toquetear
    public float timer = 0f;            // Contador de tiempo
    public float localTimeScale = 1f;   // Escala de tiempo local
    public float timerReload = 3f;      // Tiempo de recarga
    public float timerCadence = 0.2f;   // Cadencia entre disparos
    public float numberOfShoots = 3;    // Numero de disparos
    
    // Start when the script is enabled
    void Start(){
        SetStatus(0);
    }

    // Update is called once per frame
    void Update()
    {
        ReloadingChanger(); // En modo reloading carga
        ShooterControl();   // En modo
        timer += Time.deltaTime * localTimeScale;        
    }

    void ReloadingChanger(){
        if(status=="reloading" && timer >= timerReload){
            SetStatus(1); // Empezamos a disparar
        }
    }

    void ShooterControl(){
        if(status=="shoot"){
            StartCoroutine("ShootManager");
        }
    }

    IEnumerator ShootManager(){
        SetStatus(2); // Status en el que no haremos nada
        for(int i = 0; i < numberOfShoots; i++){
            yield return new WaitForSeconds(timerCadence);
            SendChildsToShoot();
        }
        SetStatus(0);
    }

    // Disparar los object
    public void SendChildsToShoot(){
        foreach (GameObject child in objectList)
        {
            child.GetComponent <DirectionalShooter>().Shoot();
        }
    }

    void SetStatus(int _status){
        switch (_status)
        {
            case 0:  status = "reloading";  break;
            case 1:  status = "shoot";      break;
            case 2:  status = "shooting";   break;
            default: status = "reloading";  break;
        }
        timer = 0f;
    }

    */
}
