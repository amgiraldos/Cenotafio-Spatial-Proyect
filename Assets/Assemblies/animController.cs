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

public class animController : MonoBehaviour
{
    public List<CuboColorContainer> container;
    public Color color;

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarColor();
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
}