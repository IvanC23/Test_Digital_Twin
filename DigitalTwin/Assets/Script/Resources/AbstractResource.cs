using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractResource : MonoBehaviour,HeightSender
{
    [Header("Parametri necessari")]
    [SerializeField] Material _myMaterial;
    private Material _myMaterialInstance;


    [Header("Parametri da non modificare, solo per visualizzare da Editor")]
    [SerializeField] private string _myID;
    [SerializeField] private Color _myColor;

    //Classe astratta comune tra tutto le risorse, prevede i metodi di inizializzazione dei vari parametri della risorsa

    public void SetCommonValues(string ID, Color Color)
    {
        _myID = ID;
        _myColor = Color;

        _myMaterialInstance = new Material(_myMaterial)
        {
            color = Color
        };

        GetComponent<Renderer>().material = _myMaterialInstance;
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
