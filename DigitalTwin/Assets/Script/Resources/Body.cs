using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : AbstractResource
{
    [Header("Attributo chiave, solo per visualizzazione")]
    [SerializeField] private char _y;
    public void SetKeyAttribute(char Y){
        _y = Y;
    }
    public char GetKeyAttribute(){
        return _y;
    }
}