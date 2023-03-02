using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static void HelloWorld() { Debug.Log("using static Common; Common.Hello();"); }

    public static Vector2 getCameraRelativePosition(float x, float y){
        return new Vector2(x,y);
    }

    public static void movement(Vector2 v2){

    }

    

}
