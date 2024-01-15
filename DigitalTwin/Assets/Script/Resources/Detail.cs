using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : AbstractResource
{
    [Header("Attributo chiave, solo per visualizzazione")]
    [SerializeField] private float _z;

    //Estensione della classe astratta per gestire l'attributo chiave del detail

    public void SetKeyAttribute(float Z)
    {
        _z = Z;
    }
    public float GetKeyAttribute()
    {
        return _z;
    }
}
