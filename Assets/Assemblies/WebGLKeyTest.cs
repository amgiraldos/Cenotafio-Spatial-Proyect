using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLKeyTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Tecla F presionada en WebGL");
        }
    }
}

