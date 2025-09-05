using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetAnimation : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;
    public float framesPerSecond = 10f;

    private Material material;
    private int currentFrame = 0;
    private float nextFrameTime = 0f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        if (Time.time >= nextFrameTime)
        {
            currentFrame = (currentFrame + 1) % (rows * columns);

            float x = (currentFrame % columns) / (float)columns;
            float y = (rows - 1 - Mathf.Floor(currentFrame / columns)) / (float)rows;

            material.mainTextureOffset = new Vector2(x, y);
            nextFrameTime = Time.time + (1f / framesPerSecond);
        }
    }
}
