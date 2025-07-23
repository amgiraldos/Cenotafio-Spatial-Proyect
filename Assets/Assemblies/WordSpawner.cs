using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;
    private Transform avatar;
    public float spawnDistance = 3f;
    public float wordHeight = 0.9f;
    public float fadeDuration = 1f;

    private List<string> wordBank = new List<string>
    {
        "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo",
                "Información general",
        "Nació en",
        "nombre completo"
        // ... resto de palabras
    };

    private int currentWordIndex = 0;
    private Vector3 lastSpawnPosition;
    public float spawnInterval = 3f;

    IEnumerator Start()
    {
        // Esperar a que Spatial cargue el avatar
        yield return new WaitForSeconds(1f);

        // Buscar el avatar específico de Spatial
        GameObject spatialAvatar = GameObject.Find("[Spatial SDK] Editor Local Avatar");

        if (spatialAvatar != null)
        {
            avatar = spatialAvatar.transform;
            Debug.Log("Avatar encontrado: " + avatar.name);
        }
        else
        {
            Debug.LogError("No se encontró el avatar de Spatial");
            yield break;
        }

        // Verificación de componentes
        if (wordPrefab == null)
        {
            Debug.LogError("Prefab de palabra no asignado");
            yield break;
        }

        if (wordPrefab.GetComponent<TextMeshPro>() == null)
        {
            Debug.LogError("El prefab no tiene componente TextMeshPro");
        }
    }

    void Update()
    {
        if (avatar == null) return;

        // Usar posición forward del avatar 
        if (Vector3.Distance(avatar.position, lastSpawnPosition) > spawnInterval &&
            currentWordIndex < wordBank.Count)
        {
            SpawnWord();
            lastSpawnPosition = avatar.position;
        }
    }

    
    void SpawnWord()
    {
        // Spawn frente al avatar (considerando su rotación)
        Vector3 spawnDirection = avatar.forward;
        spawnDirection.y = 0; // Mantener horizontal
        spawnDirection.Normalize();

        Vector3 spawnPos = avatar.position +
                         spawnDirection * spawnDistance +
                         new Vector3(0, wordHeight, 20) +
                         Random.insideUnitSphere * 0.5f;

        GameObject wordObj = Instantiate(wordPrefab, spawnPos, Quaternion.identity);
        TextMeshPro wordText = wordObj.GetComponent<TextMeshPro>();
        wordText.text = wordBank[currentWordIndex];

        // Orientar hacia el avatar
        wordObj.transform.LookAt(avatar);
        wordObj.transform.rotation *= Quaternion.Euler(0, 180, 0); // Voltear para que se lea correctamente

        StartCoroutine(FadeWord(wordText));
        currentWordIndex++;
    }

    IEnumerator FadeWord(TextMeshPro text)
    {
        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(text.gameObject);
    }

    void OnDrawGizmos()
    {
        if (avatar != null)
        {
            Gizmos.color = Color.green;
            Vector3 spawnAreaCenter = avatar.position + avatar.forward * spawnDistance;
            Gizmos.DrawWireSphere(spawnAreaCenter, 0.5f);
        }
    }
}