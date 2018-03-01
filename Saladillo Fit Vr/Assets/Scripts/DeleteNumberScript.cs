///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloFitVR
// DeleteNumberScript.cs
///////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteNumberScript : MonoBehaviour {

	// Objeto con la dirección IP controducida por el usuario
	public Text ipAddress;

	/// <summary>
	/// Método que se ejecuta cuando se pulsa un boton.
	/// </summary>
	/// <remarks>
	/// Borra el último número introducido.
	/// </remarks>
	public void Click()
	{
		if (ipAddress.GetComponent<Text>().text.Length > 0)
		{
			// Se obtiene la dirección IP introducida por el usuario
			ipAddress.GetComponent<Text>().text = ipAddress.GetComponent<Text>().text.Substring(0, ipAddress.GetComponent<Text>().text.Length-1);
		}
	}
}
