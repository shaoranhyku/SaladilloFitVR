using System;

public static class GameManager
{
    // Clave para la dirección IP
    public const string IP_ADDRESS = "IP_ADDRESS";

    // Variable para almacena la dirección IP de la web API
    public static string ipAddress;

    // Variable que almacena el DNI del cliente
    public static string dni;
    
    // Variable que almacena el nombre del cliente
    public static string nombre;

    // Constante con la URL de la web API para comprobar la conectividad

    public const string WEB_API_CHECK_CONNECTIVITY =
        "http://{0}/SaladilloFitVR/api/SaladilloFitVR/CheckConnectivity";

    // Constante con la URL de la web API para obtener el nombre de un cliente

    public const string WEB_API_GET_CLIENT_NAME =
        "http://{0}/Saladillo Fit VR/api/SaladilloFitVR/GetClientName?dni={1}";

    // Constante con la URL de la web API para obtener el entrenamiento

    public const string WEB_API_GET_TRAINING =
        "http://{0}/SaladilloFitVR/api/SaladilloFitVR/GetTraining?training={1}";

    // Constante con la URL de la web API para guardar un cliente
    public const string WEB_API_LOG_TRAINING = "http://{0}/SaladilloFitVR/api/SaladilloFitVR/LogTraining";
}