///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloVR
// TrainingButtonScript.cs
///////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrainingButtonScript : MonoBehaviour
{
    // Indica el número del entrenamiento
    public int trainingNumber;

    // GameObject que representa el panel de detalles
    public GameObject detail;

    // Prefab de un GameObject que repesenta un ejercicio
    public GameObject trainingItem;

    /// <summary>
    /// Método que se ejecuta cuando se pulsa el botón de entrenamiento.
    /// </summary>
    /// <remarks>
    /// Activa el panel de detalles y llamará a un método encargado de crear los ejercicios en dicho panel.
    /// </remarks>
    public void Click()
    {
        // Activa el panel de detalles
        detail.SetActive(true);

        // Intenta obtener los ejercicios del entrenamiento
        CheckExercises();
    }

    /// <summary>
    /// Obtiene los ejercicios de un entrenamiento.
    /// </summary>
    /// <remarks>
    /// Llamará a la corrutina GetExercisesWebApi para obtenerlos.
    /// </remarks>
    private void CheckExercises()
    {
        StartCoroutine(CheckExercisesWebAPI());
    }

    private IEnumerator CheckExercisesWebAPI()
    {
        // Prepara la petición a la web API
        using (UnityWebRequest www = UnityWebRequest.Get(
            Uri.EscapeUriString(string.Format(GameManager.WEB_API_GET_TRAINING, GameManager.ipAddress, trainingNumber.ToString()))))
        {
            // Hace la petición a la web API
            yield return www.SendWebRequest();

            // Acción a realizar si la petición se ha
            // ejecutado sin error
            if (!www.isNetworkError)
            {
                // Se recupera el entrenamiento
                Training training = JsonUtility.FromJson<Training>(www.downloadHandler.text);
                
                // TODO: CREAR LOS ITEMS Y COLOCARLOS
            }
        }
    }

    #region Entidades

    [Serializable]
    public class Training
    {
        public string exercise1;
        public string exercise2;
        public string exercise3;
    }

    #endregion
}