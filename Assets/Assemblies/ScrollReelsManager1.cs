using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollReelsManager1 : MonoBehaviour
{
    public GameObject[] reelPlanes; // Sprites
    public GameObject[] associatedAssets; // Assets

    private int currentReelIndex = 0;
    private bool isScrolling = false;
    private Vector3 basePosition; // posición inicial fija

    void Start()
    {
        // Inicializa primer reel
        SetActiveReel(currentReelIndex);
        SetAssociatedAssets(currentReelIndex);
        basePosition = reelPlanes[currentReelIndex].transform.position;
    }

    void SetActiveReel(int index)
    {
        if (reelPlanes == null || index >= reelPlanes.Length) return;

        // Desactiva planes
        foreach (var plane in reelPlanes)
        {
            if (plane != null)
                plane.SetActive(false);
        }

        // Activa el plane actual
        if (reelPlanes[index] != null)
            reelPlanes[index].SetActive(true);
    }

    void SetAssociatedAssets(int index)
    {
        if (associatedAssets == null || index >= associatedAssets.Length) return;

        // Desactiva todos los assets 
        foreach (var assets in associatedAssets)
        {
            if (assets != null)
                assets.SetActive(false);
        }

        // Activa el asset asociado al reel actual
        if (associatedAssets[index] != null)
            associatedAssets[index].SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 
        {
            StartScroll();
        }
    }

    void StartScroll()
    {
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollAnimation());
        }
    }

    IEnumerator ScrollAnimation()
    {
        // 1. Mueve reel actual hacia arriba
        yield return StartCoroutine(MoveReelUp(reelPlanes[currentReelIndex]));

        // 2. Cambia al siguiente reel
        currentReelIndex = (currentReelIndex + 1) % reelPlanes.Length;
        SetActiveReel(currentReelIndex);
        SetAssociatedAssets(currentReelIndex);

        // 3. Mueve el nuevo reel desde abajo
        yield return StartCoroutine(MoveReelFromBottom(reelPlanes[currentReelIndex]));

        isScrolling = false;
    }

    IEnumerator MoveReelUp(GameObject plane)
    {
        float duration = 0.15f;
        float elapsed = 0;

        Vector3 startPos = plane.transform.position;
        Vector3 endPos = startPos + plane.transform.up * 20f;

        while (elapsed < duration)
        {
            plane.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Una vez termina el movimiento, lo desactivas
        plane.SetActive(false);
    }

    IEnumerator MoveReelFromBottom(GameObject plane)
    {
        float duration = 0.15f;
        float elapsed = 0;

        Vector3 startPos = basePosition - plane.transform.up * 2f;
        Vector3 endPos = basePosition;

        plane.transform.position = startPos;
        plane.SetActive(true);

        while (elapsed < duration)
        {
            plane.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}