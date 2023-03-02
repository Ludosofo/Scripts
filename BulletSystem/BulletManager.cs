using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Bullet Manager se dedica a gestionar el movimiento de multiples balas en un solo scripts siguiendo un patron flyweight
    // Este es solo un experimento, el gran objetivo es actualizar 500 balas activas sin problemas
    // Esto exige control de los scripts / objetos en el registro con la activación y desactivación
    // Otra capa de optimización sería que las colisiones de balas sean solas las cercanas al player

    public List<Bullet> listBullets;



    // Start is called before the first frame update
    public void AddBulletToList(Bullet bullet)
    {
        // TODO: listBullets.add(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        // Procesamiento de todas las balas haciendo llamadas correspondientes  
    }
}
