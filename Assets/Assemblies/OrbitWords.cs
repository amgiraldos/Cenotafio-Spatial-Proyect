using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class OrbitWords : MonoBehaviour
{
    [Header("Órbita")]
    public Transform wordsParent;
    public float heightOffset = 1.4f;
    public float radius = 1.8f;
    public float angularSpeed = 45f;
    public float bobAmplitude = 0.05f;
    public float bobFrequency = 2f;
    public bool billboardToPlayerY = true;

    [Header("Aparición")]
    public float appearDelay = 0.3f;
    public float appearDuration = 0.5f;
   

    private List<Transform> _items = new List<Transform>();
    private float _baseAngle;
    private bool _hasAppeared = false;

    void Awake()
    {
        RefreshItems();
        this.enabled = false;
    }

    void Update()
    {
        Vector3 camPos = SpatialBridge.cameraService.position;
        camPos.y += heightOffset;
        transform.position = camPos;

        _baseAngle += angularSpeed * Time.deltaTime;

        int count = _items.Count;
        if (count == 0) return;
        float step = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform item = _items[i];
            if (item == null) continue;

            float angleDeg = _baseAngle + i * step;
            float angleRad = angleDeg * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleRad) * radius;
            float z = Mathf.Sin(angleRad) * radius;
            float bob = bobAmplitude > 0 ? Mathf.Sin((Time.time * bobFrequency) + i * 0.7f) * bobAmplitude : 0;

            item.localPosition = new Vector3(x, bob, z);

            if (billboardToPlayerY)
            {
                Vector3 lookTarget = SpatialBridge.cameraService.position;
                lookTarget.y = transform.position.y;
                item.LookAt(lookTarget);
                Vector3 e = item.localEulerAngles;
                item.localEulerAngles = new Vector3(0f, e.y, 0f);
            }
        }
    }

    [ContextMenu("Refresh Items")]
    public void RefreshItems()
    {
        _items.Clear();
        var parent = wordsParent ?? transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            var c = parent.GetChild(i);
            if (c != null) _items.Add(c);
        }
    }

    public void StartOrbit()
    {
        this.enabled = true;
        if (!_hasAppeared)
        {
            _hasAppeared = true;
            StartCoroutine(ShowItemsSequentially());
        }
    }

    private IEnumerator ShowItemsSequentially()
    {
        foreach (var item in _items)
        {
            var rend = item.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                Material mat = new Material(rend.material);
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0f);
                rend.material = mat;
                StartCoroutine(FadeInMaterialAlpha(mat, appearDuration));
             
            }
            yield return new WaitForSeconds(appearDelay);
        }
    }

    private IEnumerator FadeInMaterialAlpha(Material mat, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            Color c = mat.color;
            c.a = alpha;
            mat.color = c;
            time += Time.deltaTime;
            yield return null;
        }
        Color final = mat.color;
        final.a = 1f;
        mat.color = final;
    }
}
