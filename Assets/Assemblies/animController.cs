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
    public AudioSource audioSource1; // Looping/playing controlled in Inspector
    public AudioSource audioSource2; // Will increment volume

    [Header("GameObject to Deactivate")]
    public GameObject objectToDeactivate;

    [Header("Volume Increment Settings")]
    public float volumeIncrement = 0.1f; // 0.1 per key press

    private bool eventTriggered = false;

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarColor();
            HandleAudioVolume();
        }
    }
#endif

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

    private void HandleAudioVolume()
    {
        if (eventTriggered || audioSource2 == null) return;

        // Play if not already playing and volume is at zero
        if (!audioSource2.isPlaying && audioSource2.volume == 0f)
        {
            audioSource2.Play();
        }

        audioSource2.volume = Mathf.Clamp01(audioSource2.volume + volumeIncrement);

        if (audioSource2.volume >= 1.0f)
        {
            eventTriggered = true;
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
        }
    }
}