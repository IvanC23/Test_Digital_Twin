using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : AbstractResource
{
    [Header("Attributo chiave, solo per visualizzazione")]
    [SerializeField] private float _x;

    //Estensione della classe astratta per gestire l'attributo chiave della base
    public void SetKeyAttribute(float X){
        _x = X;
    }
    public float GetKeyAttribute(){
        return _x;
    }
    
}
