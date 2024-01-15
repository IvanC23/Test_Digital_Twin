using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Receiver
{
    // Interfaccia necessaria per astrarre il concetto di modulo ricevitore, e poter 
    // passare le risorse in maniera indipendente da un modulo all'altro alla fine del transito
    void ReceiveResource(GameObject Resource);
}
