///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloFitVR
// ModeJoystick.cs
///////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour {

	// La velocidad máxima de desplazamiento
	public float maxSpeed = 0.5f;

	// Fuerza de empuje
	public float pushForce = 10f;

	// Referencia al rigidbody que queremos mover;
	public Rigidbody rigidbody;
	
	// Use this for initialization
	private void Awake()
	{
		//Recuperamos la referencia a componente RigidBody
		rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		// Recuperamos el valor de los ejes horizontal y vertical
		// Son valores normalizados que van de 0 a 1
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// Calculamos el vector de movimiento con la dirección a la que mira la cámara
		Vector3 movementDirection = (h * Camera.main.transform.right + v * Camera.main.transform.forward).normalized;

		// Comprobamos la magnitud de desplazamiento y aplicamos el empuje si la velocidad
		// máxima no se ha alcanzado
		if (rigidbody.velocity.magnitude < maxSpeed)
		{
			// Aplicamos el empuje en la dirección calculada con la fuerza indicada
			rigidbody.AddForce(movementDirection * pushForce);
		}
	}
}
