using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite2 : MonoBehaviour,HeightSender
{
    [SerializeField] Body _body;
    [SerializeField] Composite1 _composite1;

    // I composite sono degli oggetti creati per gestire le varie composizioni di risorse possibili
    // ogni composite gestisce il prefab corrispondente. 

    public void SetValuesBody(string ID, char Y, Color Color)
    {
        _body.SetCommonValues(ID, Color);
        _body.SetKeyAttribute(Y);
    }
    public Body GetBody()
    {
        return _body;
    }
    public Composite1 GetComposite1()
    {
        return _composite1;
    }
    public float GetHeight()
    {
        return _composite1.GetHeight();
    }

    public void SetComponents(Body body, Composite1 composite1)
    {
        SetValuesBody(body.GetID(), body.GetKeyAttribute(), body.GetColor());
        _composite1.SetComponents(composite1.GetBase1(),composite1.GetBase2());
    }
}
