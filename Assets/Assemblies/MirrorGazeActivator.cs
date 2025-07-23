using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGazeActivator : MonoBehaviour
{
    public GameObject textGroup;
    public float activationTime = 3f;
    private float timer;
    private bool isInside;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entró: " + other.name);
        isInside = true;
        timer = 0f;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            textGroup.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInside)
        {
            timer += Time.deltaTime;
            if (timer >= activationTime)
            {
                textGroup.SetActive(true);
            }
        }
    }
}