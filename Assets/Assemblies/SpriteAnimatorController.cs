using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class SpriteAnimatorController : MonoBehaviour
{
    private Animator animator;
    private IReadOnlyAvatar avatar;

    [Header("Movimiento")]
    public float speedThreshold = 0.1f;

    [Header("Audio")]
    [Tooltip("Audio del reel asociado a este sprite")]
    public AudioSource reelAudio;            // Asigna en el Inspector
    public bool useFade = true;
    public float fadeTime = 0.15f;           // 150 ms

    // Estado para evitar llamadas repetidas
    private bool isIdle;                     // true = avatar quieto (anim + audio ON)
    private Coroutine fadeRoutine;

    void Start()
    {
        animator = GetComponent<Animator>();

        var localActor = SpatialBridge.actorService.localActor;
        if (localActor != null)
            avatar = localActor.avatar;

        if (avatar == null)
            Debug.LogWarning("No se encontró el avatar local.");

        // Si no asignaste el audio en el Inspector, intenta encontrarlo en hijos
        if (reelAudio == null)
            reelAudio = GetComponentInChildren<AudioSource>();

        // Estado inicial
        SetIdleState(true, immediate:true);
    }

    void Update()
    {
        if (avatar == null || animator == null)
            return;

        float speed = avatar.velocity.magnitude;
        bool shouldBeIdle = speed < speedThreshold; // quieto = queremos animación y audio

        if (shouldBeIdle != isIdle)
            SetIdleState(shouldBeIdle, immediate:false);
    }

    private void SetIdleState(bool idle, bool immediate)
    {
        isIdle = idle;

        // Control del Animator: idle => reproduce; en movimiento => detén
        animator.speed = idle ? 1f : 0f;

        // Control del Audio
        if (reelAudio != null)
        {
            if (useFade && !immediate)
            {
                if (fadeRoutine != null) StopCoroutine(fadeRoutine);
                fadeRoutine = StartCoroutine(FadeAudio(idle));
            }
            else
            {
                if (idle)
                {
                    if (!reelAudio.isPlaying) reelAudio.UnPause(); // mantiene posición
                    reelAudio.volume = 1f;
                }
                else
                {
                    reelAudio.Pause(); // no reinicia al reanudar
                }
            }
        }
    }

    private IEnumerator FadeAudio(bool toIdle)
    {
        // toIdle=true => subir volumen y UnPause; false => bajar y Pause al final
        float startVol = reelAudio.volume;
        float endVol   = toIdle ? 1f : 0f;

        if (toIdle && !reelAudio.isPlaying) reelAudio.UnPause();

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / fadeTime);
            reelAudio.volume = Mathf.Lerp(startVol, endVol, k);
            yield return null;
        }

        reelAudio.volume = endVol;

        if (!toIdle)
            reelAudio.Pause();
    }
}
