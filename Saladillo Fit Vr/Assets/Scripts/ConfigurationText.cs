///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloVR
// ConfigurationText.cs
///////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConfigurationText : MonoBehaviour
{
    // Objeto que indica si se ha establecido la conexión
    public GameObject connected;

    // Objeto que indica si no se ha establecido la conexión
    public GameObject disconnected;

    // Objeto que representa el panel de cliente
    public GameObject clientPanel;

    // Use this for initialization
    void Start()
    {
        // Se recupera el valor de dirección IP almacenado
        // en la configuración de la aplicación
        GameManager.ipAddress = PlayerPrefs.GetString(GameManager.IP_ADDRESS);

        // Se muestra la dirección IP
        GetComponent<Text>().text = GameManager.ipAddress;

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
        }
    }
}