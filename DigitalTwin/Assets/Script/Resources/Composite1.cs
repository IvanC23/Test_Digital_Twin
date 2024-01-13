using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite1 : MonoBehaviour,HeightSender
{
    [SerializeField] Base _base1;
    [SerializeField] Base _base2;

    public void SetValuesBase1(string ID, float X, Color Color)
    {
        _base1.SetValues(ID,X,Color);
    }
    public void SetValuesBase2(string ID, float X, Color Color)
    {
        _base2.SetValues(ID,X,Color);
    }
    public float GetHeight(){
        return _base1.GetHeight();
    }
}
