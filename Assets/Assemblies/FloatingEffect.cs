using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float amplitude = 0.25f; // Altura del movimiento
    public float frequency = 2f;    // Velocidad del movimiento

    public bool rotate = true;
  
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimiento de subir y bajar
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = tempPos;

    }
}
