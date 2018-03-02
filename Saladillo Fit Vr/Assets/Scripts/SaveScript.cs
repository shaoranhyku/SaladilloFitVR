///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloFitVR
// SaveScript.cs
///////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    // Objeto que indica si se ha establecido la conexión
    public GameObject connected;

    // Objeto que indica si no se ha establecido la conexión
    public GameObject disconnected;

    // Objeto que representa el panel de cliente
    public GameObject clientPanel;
    
    // Objeto que representa el panel de entrenamientos
    public GameObject trainingPanel;

    // Objeto con la dirección IP controducida por el usuario
    public Text ipAddress;

    /// <summary>
    /// Método que se ejecuta cuando se pulsa el botón Save.
    /// </summary>
    /// <remarks>
    /// Obtiene la dirección IP introducida por el usuario y
    /// la guarda en las preferencias de la aplicación.
    /// </remarks>
    public void Click()
    {
        // Se obtiene la dirección IP introducida por el usuario
        GameManager.ipAddress = ipAddress.GetComponent<Text>().text;

        // Aquí se guarda la direcció IP
        PlayerPrefs.SetString(GameManager.IP_ADDRESS, GameManager.ipAddress);
        // Se almacena el valor en la configuración de la aplicación
        PlayerPrefs.Save();
        
        // Se comprueba la conecctividad con la web api
        CheckConnectivity();
    }

    /// <summary>
    /// Comprueba si existe conexión con la web API
    /// </summary>
    /// <remarks>
    /// Llamará a la corrutina CheckConnectivityWebAPI
    /// para comprobar la conexión
    /// </remarks>
    private void CheckConnectivity()
    {
        StartCoroutine(CheckConnectivityWebAPI());
    }

    private IEnumerator CheckConnectivityWebAPI()
    {
        // Prepara la petición a la web API
        using (UnityWebRequest www = UnityWebRequest.Get(
            Uri.EscapeUriString(string.Format(GameManager.WEB_API_CHECK_CONNECTIVITY, GameManager.ipAddress))))
        {
            // Hace la petición a la web API
            yield return www.SendWebRequest();
			
            // Comprueba el valor devuelto por el método. Si la respuesta
            // es correcta, activa el objeto que indica que se ha establecido conexión,
            // y desactiva el objeto que indica que no hay conexión
            connected.SetActive(www.responseCode == 200);
            disconnected.SetActive(!connected.activeSelf);
            clientPanel.SetActive(www.responseCode == 200);
            if (www.responseCode != 200)
            {
                trainingPanel.SetActive(false);
            }
        }
    }
}