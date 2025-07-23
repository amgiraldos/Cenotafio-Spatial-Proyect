using UnityEngine;
using SpatialSys.UnitySDK;
public class MessagePopup : MonoBehaviour
{
    void Start()
    {
        SpatialBridge.coreGUIService.DisplayToastMessage("Hello from Unity!");
    }
}