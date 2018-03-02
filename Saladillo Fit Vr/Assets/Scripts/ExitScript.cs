using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

	/// <summary>
	/// Este es el método que se ejecuta en OnClick
	/// </summary>
	public void Click()
	{
		// Reinicia la variable que almacena el entrenamiento
		GameManager.training = 0;
		
		// Carga la escena principal
		SceneManager.LoadScene("Main");
	}
}
