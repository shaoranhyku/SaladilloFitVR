///////////////////////////////
// Francisco Javier Florín Cárdenas
// Curso 2017/18
// SaladilloFitVR
// ButtonGazeClick.cs
///////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtomGazeClick : MonoBehaviour
{
    // Tiempo que tardará en activarse el temporizador
    public float activationTime = 3f;

    // Indica si el puntero está sobre el objeto
    private bool isHover;

    // Indica si la acción se ha realizado
    private bool executed;

    // Update is called once per frame
    void Update()
    {
        // Si el usuario está mirando el objeto y el temporizador ha terminado de contar, o si
        // el usuario está mirando el objeto y pulsa el botón Fire1 del mando y la acción aún
        // no se ha ejecuado, relizaremos la acción correspondiente
        if (isHover && Input.GetButtonDown("Fire1") && !executed)
        {
            // Se indica que ya se ha realizado la acción
            executed = true;
            // Desactivamos el contador de tiempo del cursor para evitar que se quede bloqueado
            CustomPointerTimer.CPT.StopCounting();
        }

        if (isHover && CustomPointerTimer.CPT.ended && !executed)
        {
            // Se indica que ya se ha realizado la acción
            executed = true;
            // Desactivamos el contador de tiempo del cursor para evitar que se quede bloqueado
            CustomPointerTimer.CPT.StopCounting();
            // Se invoca al click del botón
            GetComponent<Button>().onClick.Invoke();
        }
    }
    
    /// <summary>
    /// Método que llamaremos desde el EventTrigger PointerEnter.
    /// </summary>
    public void StartHover()
    {
        // Indicamos que el objeto está siendo mirado directamente
        isHover = true;
        // Marcamos como no realizada la acción
        executed = false;
        // Iniciamos el contador del puntero, indicando el tiempo de activación
        CustomPointerTimer.CPT.StartCounting(activationTime);
    }

    /// <summary>
    /// Este método lo llamaremos desde el EventTrigger PointerExist
    /// </summary>
    public void EndHover()
    {
        // Indicado que el objeto ya no está siendo mirado
        isHover = false;
        // Reiniciamos el contador del puntero
        CustomPointerTimer.CPT.StopCounting();
    }
}