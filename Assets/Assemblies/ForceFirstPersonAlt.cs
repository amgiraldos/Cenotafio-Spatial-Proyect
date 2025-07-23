using UnityEngine;
using SpatialSys.UnitySDK;

public class ForceFirstPersonAlt : MonoBehaviour
{
    private void Start()
    {
        SpatialBridge.cameraService.forceFirstPerson = true;
    }

    private void OnDestroy()
    {
        SpatialBridge.cameraService.forceFirstPerson = false;
    }
}
