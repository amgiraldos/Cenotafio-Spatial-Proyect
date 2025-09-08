using UnityEngine;
using TMPro;

public class DatosRecolectadosCounterTMP : MonoBehaviour
{
    public TextMeshProUGUI datosText;
    public int maxDatos = 2000;
    public float duracionAnimacion = 15f;


    private int valorActual = 0;
    private Coroutine animacionCorutina;

    void Start()
    {
        valorActual = 0;
        ActualizarTexto();
    }

    public void ContarHastaMax()
    {
        // Si ya hay una animación corriendo, la detenemos
        if (animacionCorutina != null)
            StopCoroutine(animacionCorutina);
        
        animacionCorutina = StartCoroutine(AnimarContador(valorActual, maxDatos, duracionAnimacion));
    }

    private System.Collections.IEnumerator AnimarContador(int desde, int hasta, float duracion)
    {
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            valorActual = Mathf.RoundToInt(Mathf.Lerp(desde, hasta, t));
            ActualizarTexto();
            tiempo += Time.deltaTime;
            yield return null;
        }
        valorActual = hasta;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        datosText.text = "_ datos recolectados : " + valorActual + " .";
    }
}