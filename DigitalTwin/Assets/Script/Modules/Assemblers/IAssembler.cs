using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssembler
{
    //Interfaccia necessaria per astrarre gli assembler per prendere i parametri nel pannello delle impostazioni
    float GetAssembleTime();
    void SetAssembleTime(float NewTime);
}
