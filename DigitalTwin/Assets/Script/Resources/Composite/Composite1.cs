using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite1 : MonoBehaviour, HeightSender
{
    [SerializeField] Base _base1;
    [SerializeField] Base _base2;

    public void SetValuesBase1(string ID, float X, Color Color)
    {
        _base1.SetCommonValues(ID, Color);
        _base1.SetKeyAttribute(X);
    }
    public void SetValuesBase2(string ID, float X, Color Color)
    {
        _base2.SetCommonValues(ID, Color);
        _base2.SetKeyAttribute(X);
    }
    public Base GetBase1()
    {
        return _base1;
    }
    public Base GetBase2()
    {
        return _base2;
    }
    public float GetHeight()
    {
        return _base1.GetHeight();
    }

    public void SetComponents(Base base1, Base base2)
    {
        SetValuesBase1(base1.GetID(), base1.GetKeyAttribute(), base1.GetColor());
        SetValuesBase2(base2.GetID(), base2.GetKeyAttribute(), base2.GetColor());
    }
}
