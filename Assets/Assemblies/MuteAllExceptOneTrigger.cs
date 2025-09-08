using UnityEngine;

public class MuteAllExceptOneTrigger : MonoBehaviour
{
    [Header("Arrastra aquí el AudioSource que NO quieres mutear")]
    public AudioSource audioExcluido;

    [Header("Arrastra aquí el script ScrollReelsManager1")]
    public MonoBehaviour scrollReelsManager1; // referencia al componente

    public void MuteAllExceptOneTriggerr()
    {
        // Mutea todos los audios menos el excluido
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            source.mute = (source != audioExcluido);
        }
        // Desactiva el script ScrollReelsManager1
        if (scrollReelsManager1 != null)
            scrollReelsManager1.enabled = false;
    }

    // Opcional: para restaurar
    public void UnmuteAllAndEnableScript()
    {
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            source.mute = false;
        }
        if (scrollReelsManager1 != null)
            scrollReelsManager1.enabled = true;
    }
}