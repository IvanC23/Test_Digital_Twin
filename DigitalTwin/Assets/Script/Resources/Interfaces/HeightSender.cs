using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HeightSender 
{
    // Interfaccia necessaria per poter prendere l'altezza della risorsa trasportata a prescindere 
    // dalla tipologia, potendo calcolare così l'offset per posizionarla sul trasportatore
    float GetHeight();
}
