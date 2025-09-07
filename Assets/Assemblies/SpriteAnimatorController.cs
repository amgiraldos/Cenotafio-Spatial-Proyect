using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class SpriteAnimatorController : MonoBehaviour
{
    private Animator animator;
    private IReadOnlyAvatar avatar;  // <-- tipo correcto
    public float speedThreshold = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();

        var localActor = SpatialBridge.actorService.localActor;
        if (localActor != null)
        {
            avatar = localActor.avatar; // No hay cast
        }

        if (avatar == null)
        {
            Debug.LogWarning("No se encontró el avatar local.");
        }
    }

    void Update()
    {
        if (avatar == null)
            return;

        float speed = avatar.velocity.magnitude;

        animator.speed = (speed < speedThreshold) ? 1f : 0f;
    }
}
