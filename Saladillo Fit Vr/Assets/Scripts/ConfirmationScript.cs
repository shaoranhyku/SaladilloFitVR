///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloVR
// ConfirmationScript.cs
///////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConfirmationScript : MonoBehaviour
{
    // Objeto que representa el panel de entrenamientos
    public GameObject trainingPanel;

    // Objeto con del dni del cliente
    public Text dni;

    // Objeto con el mensaje de bienvenida
    public Text welcomeMessage;

    /// <summary>
    /// Método que se ejecuta cuando se pulsa el botón Confirmation.
    /// </summary>
    /// <remarks>
    /// Obtiene el DNI introducida por el usuario y
    /// la guarda en las preferencias de la aplicación.
    /// </remarks>
    public void Click()
    {
        // Se obtiene la dirección IP introducida por el usuario
        GameManager.dni = dni.GetComponent<Text>().text;

        // Intenta obtener el cliente
        GetCliente();
    }

    /// <summary>
    /// Obtiene el cliente.
    /// </summary>
    /// <remarks>
    /// Llamará a la corrutina GetClienteWebAPI para obtener el cliente,.
    /// Si lo recibe, lo mostrará en el Text que recibe el script, y activará
    /// el panel de entrenamientos.
    /// </remarks>
    private void GetCliente()
    {
        StartCoroutine(GetClienteWebAPI());
    }

    private IEnumerator GetClienteWebAPI()
    {
        // Prepara la petición a la web API
        using (UnityWebRequest www = UnityWebRequest.Get(
            Uri.EscapeUriString(string.Format(GameManager.WEB_API_GET_CLIENT_NAME, GameManager.ipAddress,
                GameManager.dni))))
        {
            // Hace la petición a la web API
            yield return www.SendWebRequest();

            // Acción a realizar si la petición se ha
            // ejecutado sin error
            if (!www.isNetworkError)
            {
                // Se recupera el cliente
                Client client = JsonUtility.FromJson<Client>(www.downloadHandler.text);

                // Se comprueba que se ha devuelto un usuario
                if (String.IsNullOrEmpty(client.nombre))
                {
                    // Se guarda el nombre del cliente
                    GameManager.nombre = client.nombre;

                    // Se cambia el texto del dni por el nombre del cliente
                    dni.GetComponent<Text>().text = GameManager.nombre;

                    // Activo el panel de entrenamientos
                    trainingPanel.SetActive(true);

                    // Cambio el mensaje de bienvenida
                    welcomeMessage.GetComponent<Text>().text = String.Format("Hola {0}", GameManager.nombre);
                }
                else
                {
                    // Desactivo el panel de entrenamientos
                    trainingPanel.SetActive(false);

                    // Cambio el mensaje de bienvenida
                    welcomeMessage.GetComponent<Text>().text = "Bienvenid@ a Saladillo Fit VR";
                }
            }
        }
    }

    #region Entidades

    [Serializable]
    public class Client
    {
        public string dni;
        public string nombre;
    }

    #endregion
}