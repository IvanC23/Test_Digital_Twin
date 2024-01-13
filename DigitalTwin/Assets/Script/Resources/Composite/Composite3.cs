using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite3 : MonoBehaviour, HeightSender
{
    [SerializeField] Detail _detail;
    [SerializeField] Composite2 _composite2;

    public void SetValuesDetail(string ID, float Z, Color Color)
    {
        _detail.SetCommonValues(ID, Color);
        _detail.SetKeyAttribute(Z);
    }
    public Detail GetDetail()
    {
        return _detail;
    }
    public Composite2 GetComposite2()
    {
        return _composite2;
    }
    public float GetHeight()
    {
        return _detail.GetHeight() + _composite2.GetHeight();
    }

    public void SetComponents(Detail detail, Composite2 composite2)
    {
        SetValuesDetail(detail.GetID(), detail.GetKeyAttribute(), detail.GetColor());
        _composite2.SetComponents(composite2.GetBody(),composite2.GetComposite1());
    }
}
