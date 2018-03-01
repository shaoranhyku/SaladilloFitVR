///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloFitVR
// MovementDisc.cs
///////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisc : MonoBehaviour
{
    // La velocidad máxima de desplazamiento
    public float maxSpeed = 0.5f;

    // Fuerza de empuje
    public float pushForce = 10f;

    // Indica si el usuario está mirando directamente
    [HideInInspector] public bool isHover;

    // Referencia al rigidbody que queremos mover;
    public Rigidbody rigidbody;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHover)
        {
            if (rigidbody.velocity.magnitude < maxSpeed)
            {
                rigidbody.AddForce((GvrPointerInputModule.Pointer.CurrentRaycastResult.worldPosition -
                                    transform.position).normalized * pushForce);
            }
        }
    }

    /// <summary>
    /// Acciones a realizar para el evento de mirar el disco.
    /// </summary>
    public void StartLookingAtDisc()
    {
        isHover = true;
    }
    
    /// <summary>
    /// Acciones a realizar para el evento de dejar de mirar el disco.
    /// </summary>
    public void StopLookingAtDisc()
    {
        isHover = false;
    }
}