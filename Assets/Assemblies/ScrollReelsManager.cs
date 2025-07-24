using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ScrollReelsManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] reelClips; // Assign videos in Inspector
    public GameObject[] associatedAssets; // Assign 3D objects for each reel

    private int currentReelIndex = 0;
    private bool isScrolling = false;
    private Vector3 basePosition; // posición inicial fija

    void Start()
    {
        // Initialize first reel
        PlayReel(currentReelIndex);
        SetAssociatedAssets(currentReelIndex);
        basePosition = videoPlayer.transform.position;

    }

    void PlayReel(int index)
    {
        if (reelClips == null || index >= reelClips.Length) return;

        videoPlayer.clip = reelClips[index];
        videoPlayer.Play();
    }

    void SetAssociatedAssets(int index)
    {
        if (associatedAssets == null || index >= associatedAssets.Length) return;

        // Disable all assets first
        foreach (var assets in associatedAssets)
        {
            if (assets != null)
                assets.SetActive(false);
        }

        // Enable current assets
        if (associatedAssets[index] != null)
            associatedAssets[index].SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 
        {
            StartScroll();
        }

        // Handle scroll animation in coroutine instead
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
        // 1. Animate current reel moving up
        yield return StartCoroutine(MoveReelUp());

        // 2. Switch to next reel
        currentReelIndex = (currentReelIndex + 1) % reelClips.Length;
        PlayReel(currentReelIndex);
        SetAssociatedAssets(currentReelIndex);

        // 3. Animate new reel coming from bottom
        yield return StartCoroutine(MoveReelFromBottom());

        isScrolling = false;
    }

    IEnumerator MoveReelUp()
    {
        float duration = 0.15f;
        float elapsed = 0;

        Vector3 startPos = basePosition;
        Vector3 endPos = basePosition + Vector3.up * 20f;

        videoPlayer.transform.position = startPos;

        while (elapsed < duration)
        {
            videoPlayer.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator MoveReelFromBottom()
    {
        float duration = 0.15f;
        float elapsed = 0;

        Vector3 startPos = basePosition + Vector3.down * 2f;
        Vector3 endPos = basePosition;

        videoPlayer.transform.position = startPos;

        while (elapsed < duration)
        {
            videoPlayer.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}