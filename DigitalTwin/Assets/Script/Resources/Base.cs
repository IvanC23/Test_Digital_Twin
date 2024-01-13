using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour,HeightSender
{
    [Header("Parametri necessari")]
    [SerializeField] Material _myMaterial;
    private Material _myMaterialInstance;


    [Header("Parametri da non modificare, solo per visualizzare da Editor")]
    [SerializeField] private string _myID;
    [SerializeField] private float _x;
    [SerializeField] private Color _myColor;

    public void SetValues(string ID, float X, Color Color)
    {
        _myID = ID;
        _x = X;
        _myColor = Color;

        _myMaterialInstance = new Material(_myMaterial)
        {
            color = Color
        };

        GetComponent<Renderer>().material = _myMaterialInstance;
    }
    public float GetX()
    {
        return _x;
    }
    public string GetID()
    {
        return _myID;
    }
    public Color GetColor()
    {
        return _myColor;
    }
    public float GetHeight(){
        return transform.localScale.y;
    }


}
