using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CuboColor
{
    public Renderer renderer;
}

[System.Serializable]
public class CuboColorContainer
{
    public List<CuboColor> cubitos;
    public bool wasUsed;
}

public class AnimController : MonoBehaviour
{
    public List<CuboColorContainer> container;
    public Color color;

    [Header("Audio Controls")]
    public AudioSource audioSource1; // Looping/playing controlled in Inspector (si lo usas)
    public AudioClip audioClip2;     // <<-- Asigna aquí tu audio desde el Inspector

    [Header("GameObject to Deactivate")]
    public GameObject objectToDeactivate;

    [Header("Volume Increment Settings")]
    public float volumeIncrement = 0.1f; // 0.1 per key press

    private AudioSource audioSource2;    // El AudioSource que crearemos en runtime
    private bool eventTriggered = false;
    private List<GameObject> lastChangedCubes = new List<GameObject>();

    void Start()
    {
        // Crea el AudioSource en runtime y lo configura
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = audioClip2;
        audioSource2.volume = 0f;
        audioSource2.playOnAwake = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Tecla F presionada.");
            CambiarColor();
            HandleAudioVolume();
        }
    }

    public void CambiarColor()
    {
        foreach (var cuboColorContainer in container)
        {
            if (!cuboColorContainer.wasUsed)
            {
                foreach (var cuboColor in cuboColorContainer.cubitos)
                {
                    cuboColor.renderer.material.color = color;
                }

                cuboColorContainer.wasUsed = true;
                break;
            }
        }
    }

    private void MoveCubesDown(float distance)
    {
        foreach (var go in lastChangedCubes)
        {
            if (go != null)
            {
                Vector3 pos = go.transform.position;
                go.transform.position = new Vector3(pos.x, pos.y - distance, pos.z);
            }
        }
    }
    private void HandleAudioVolume()
    {
        Debug.Log("Entrando a HandleAudioVolume");

        if (eventTriggered)
        {
            Debug.Log("Evento ya disparado, saliendo.");
            return;
        }
        if (audioSource2 == null)
        {
            Debug.Log("audioSource2 es null, saliendo.");
            return;
        }

        Debug.Log("audioSource2.volume antes: " + audioSource2.volume);

        if (!audioSource2.isPlaying && audioSource2.volume == 0f)
        {
            audioSource2.Play();
            Debug.Log("Llamando a Play() en audioSource2");
        }

        audioSource2.volume = Mathf.Clamp01(audioSource2.volume + volumeIncrement);
        Debug.Log("audioSource2.volume después: " + audioSource2.volume);

        if (audioSource2.volume >= 1.0f)
        {
            eventTriggered = true;
            Debug.Log("Volumen llegó a 1.0, desactivando GO");
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
            
            MoveCubesDown(10f); 
        }
    }
}