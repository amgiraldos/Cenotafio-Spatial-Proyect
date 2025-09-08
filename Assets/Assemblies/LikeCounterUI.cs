using UnityEngine;
using UnityEngine.UI;

public class LikeCounterUI : MonoBehaviour
{
    public Text likeText; // Arrastra aquí el objeto LikeText
    private int likeCount = 0;

    // Controla los modos de suma
    private bool enableClick = false;
    private bool enableF = false;

    void Update()
    {
        if (enableClick && Input.GetMouseButtonDown(0))
        {
            AddLike();
        }

        if (enableF && Input.GetKeyDown(KeyCode.F))
        {
            AddLike();
        }
    }

    // Método para triggers 1 y 2
    public void AddLike()
    {
        likeCount++;
        UpdateLikeText();
    }

    // Trigger 3: habilita suma por click, desactiva suma por F
    public void EnableClickLike()
    {
        enableClick = true;
        enableF = false;
    }

    // Trigger 4: deshabilita suma por click, habilita suma por F
    public void EnableFLike()
    {
        enableClick = false;
        enableF = true;
    }

    // Trigger 5: resetea contador y desactiva ambos modos
    public void ResetLikes()
    {
        likeCount = 0;
        enableClick = false;
        enableF = false;
        UpdateLikeText();
    }

    private void UpdateLikeText()
    {
        likeText.text = likeCount.ToString();
    }
}