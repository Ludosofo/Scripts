using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Experimental.Input;

// Codigo extraido de Unity: https://docs.unity3d.com/Packages/com.unity.inputsystem@0.9/manual/ActionAssets.html
// IGameplayActions is an interface generated from the "gameplay" action map
// we added (note that if you called the action map differently, the name of
// the interface will be different). This was triggered by the "Generate Interfaces"
// checkbox.

public class PlayerInputSystem : MonoBehaviour
{
    public void OnUse(InputAction.CallbackContext context)
    {
        Debug.Log("OnUse");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }
}