using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssembler
{
    //Interfaccia necessaria per astrarre gli assembler e prenderne i parametri nel pannello delle impostazioni
    float GetAssembleTime();
    void SetAssembleTime(float NewTime);
}
