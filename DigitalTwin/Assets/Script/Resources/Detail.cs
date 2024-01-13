using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : AbstractResource
{
    [Header("Attributo chiave, solo per visualizzazione")]
    [SerializeField] private float _z;
    public void SetKeyAttribute(float Z){
        _z = Z;
    }
    public float GetKeyAttribute(){
        return _z;
    }
}
