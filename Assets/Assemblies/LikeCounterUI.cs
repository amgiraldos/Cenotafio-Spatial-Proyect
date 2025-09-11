using UnityEngine;
using TMPro;

public class LikeCounterUI : MonoBehaviour
{
    public TMP_Text likeText; // Arrastra aquí el objeto TMP_Text
    private int likeCount = 0;

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

    public void AddLike()
    {
        likeCount++;
        UpdateLikeText();
    }

    public void EnableClickLike()
    {
        enableClick = true;
        enableF = false;
    }

    public void EnableFLike()
    {
        enableClick = false;
        enableF = true;
    }

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