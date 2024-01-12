using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    //private ResourceTypes.Resources type = ResourceTypes.Resources.Base;
    [Header("Parametri necessari")]
    [SerializeField] Material _myMaterial;
    private Material _myMaterialInstance;

    private string _myID;
    private float _X;
    private Color _myColor;

    public void SetValues(string ID, float X, Color Color)
    {
        _myID = ID;
        _X = X;
        _myColor = Color;

        _myMaterialInstance = new Material(_myMaterial)
        {
            color = Color
        };

        GetComponent<Renderer>().material = _myMaterialInstance;
    }

}
