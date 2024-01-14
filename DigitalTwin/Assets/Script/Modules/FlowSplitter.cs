using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlowSplitter : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private List<ConveyWithWeight> _conveysWithWeight;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _weightConvey1;
    [SerializeField] private TextMeshPro _unitPassedConvey1;
    [SerializeField] private TextMeshPro _actualPercentageConvey1;
    [SerializeField] private TextMeshPro _weightConvey2;
    [SerializeField] private TextMeshPro _unitPassedConvey2;
    [SerializeField] private TextMeshPro _actualPercentageConvey2;

    private int _countUnitPassed1 = 0;
    private int _countUnitPassed2 = 0;


    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Flow Splitter";
        _weightConvey1.text = "Weight flow 1: " + _conveysWithWeight[0]._weight;
        _unitPassedConvey1.text = "Units passed through 1: " + _countUnitPassed1;
        _actualPercentageConvey1.text = "Percentage passed 1: " + 0 + "%";
        _weightConvey2.text = "Weight flow 2: " + _conveysWithWeight[1]._weight;
        _unitPassedConvey2.text = "Units passed through 2: " + _countUnitPassed2;
        _actualPercentageConvey2.text = "Percentage passed 2: " + 0 + "%";

        float totalWeight = 0;

        for (int i = 0; i < _conveysWithWeight.Count; i++)
        {
            totalWeight += _conveysWithWeight[i]._weight;
        }

        if (totalWeight != 1.0f)
        {
            Debug.LogError("All'interno di un flow splitter, il peso totale assegnato ai vari canali é " + totalWeight + " mentre dovrebbe essere 1.0");
        }
    }

    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            RepositionResource(Resource);
        }
    }



    public void RepositionResource(GameObject resource)
    {
        //Calcolo della probabilità che esca su un determinato canale tramite media ponderata e poi lo sposta sul canale

        float randomValue = Random.value;
        float cumulativeWeight = 0;

        for (int i = 0; i < _conveysWithWeight.Count; i++)
        {
            cumulativeWeight += _conveysWithWeight[i]._weight;

            if (randomValue <= cumulativeWeight)
            {
                _conveysWithWeight[i]._convey.GetComponent<Receiver>().ReceiveResource(resource);

                if (i == 0)
                {
                    _countUnitPassed1++;
                    _unitPassedConvey1.text = "Units passed through 1: " + _countUnitPassed1;
                }
                else
                {
                    _countUnitPassed2++;
                    _unitPassedConvey2.text = "Units passed through 2: " + _countUnitPassed2;
                }
                
                float percentage1 = (float)_countUnitPassed1 / (_countUnitPassed1 + _countUnitPassed2) * 100;
                float percentage2 = (float)_countUnitPassed2 / (_countUnitPassed1 + _countUnitPassed2) * 100;

                _actualPercentageConvey1.text = "Percentage passed 1: " + percentage1.ToString("F2") + "%";
                _actualPercentageConvey2.text = "Percentage passed 2: " + percentage2.ToString("F2") + "%";

                break;
            }
        }

    }
}

[System.Serializable]
public struct ConveyWithWeight
{
    public Convey _convey;
    public float _weight;
}
