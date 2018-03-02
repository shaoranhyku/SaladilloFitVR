///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloVR
// StartTrainingScript.cs
///////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartTrainingScript : MonoBehaviour {

	/// <summary>
	/// Este es el método que se ejecuta en OnClick
	/// </summary>
	public void Click()
	{
		// Llama al método que guarda la información del entrenamiento a comenzar
		StartTraining();
	}

	/// <summary>
	/// Guarda la información del entrenamiento a comenzar.
	/// </summary>
	/// <remarks>
	/// Llama a una corrutina que se conecta con el webAPI para guardar
	/// la información.
	/// </remarks>
	private void StartTraining()
	{
		StartCoroutine(StartTrainingWebAPI());
	}

	private IEnumerator StartTrainingWebAPI()
	{
		if (GameManager.training > 0)
		{
			// Construimos la información que se envia a la webAPI
			WWWForm form = new WWWForm();
			form.AddField("dni", GameManager.dni);
			form.AddField("name", GameManager.training);
			
			// Crea la petición a la webAPI
			using (UnityWebRequest www = UnityWebRequest.Post(
				Uri.EscapeUriString(string.Format(GameManager.WEB_API_LOG_TRAINING, GameManager.ipAddress)), form))
			{
				// Envia la petición a la webAPI y espera la respuesta
				yield return www.SendWebRequest();

				// Acción a realizar si la petición se ha ejecutado sin error
				if (!www.isNetworkError)
				{
					// Carga la escena de entrenamiento
					SceneManager.LoadScene("Machines");
				}
			}
		}
	}
}
