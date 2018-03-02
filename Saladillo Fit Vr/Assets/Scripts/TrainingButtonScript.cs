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
using UnityEngine.UI;

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
                ExercisesList exercisesList = JsonUtility.FromJson<ExercisesList>(www.downloadHandler.text);
                
                // Destruye todos os hijos
                foreach (Transform child in detail.transform) {
                    Destroy(child.gameObject);
                }
                
                // Crea los objetos y los coloca
                
                // Se crea el objeto para el titulo
                GameObject title = Instantiate(trainingItem);
                // Se asigna el texto que debe mostrar
                title.GetComponentInChildren<Text>().text = String.Format("Ejercicio {0}", trainingNumber.ToString());
                // Se establece su padre que este en la escena
                title.transform.SetParent(detail.transform);
                // Se posiciona en la escena
                title.GetComponent<RectTransform>().localPosition = new Vector3(0, -0.5f, 0);
                
                for (int i = 0; i < exercisesList.training.Length; i++)
                {
                    // Se crea el objeto para un ejercicio
                    GameObject newTrainingItem = Instantiate(trainingItem);
                    // Se asigna el texto que debe mostrar
                    newTrainingItem.GetComponentInChildren<Text>().text = exercisesList.training[i].machine;
                    // Se establece su padre que este en la escena
                    newTrainingItem.transform.SetParent(detail.transform);
                    // Se posiciona en la escena
                    newTrainingItem.GetComponent<RectTransform>().localPosition = new Vector3(0, -0.5f * (i+2), 0);
                }

                GameManager.training = trainingNumber;
            }
        }
    }

    #region Entidades

    [Serializable]
    public class ExercisesList
    {
        public Exercise[] training;
    }
    
    [Serializable]
    public class Exercise
    {
        public int training;
        public string machine;
    }

    #endregion
}