using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityChecker : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _checkSuccessConvey;
    [SerializeField] private Convey _checkFailedConvey;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _testFailed;
    [SerializeField] private TextMeshPro _testSuccess;
    [SerializeField] private TextMeshPro _successRate;

    private int _countSuccess = 0;
    private int _countFails = 0;

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Quality Assurance";
        _testFailed.text = "Tests failed: " + _countFails;
        _testSuccess.text = "Tests suceeded: " + _countSuccess;
        _successRate.text = "Success rate: " + "100%";
    }

    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponentsInChildren<Base>().Length > 1)
            {
                if (Resource.GetComponentsInChildren<Base>()[0].GetKeyAttribute() + Resource.GetComponentsInChildren<Base>()[1].GetKeyAttribute() > 100)
                {
                    RepositionResourceSuccess(Resource);
                    _countSuccess++;
                    _testSuccess.text = "Tests suceeded: " + _countSuccess;
                }
                else
                {
                    RepositionResourceFail(Resource);
                    _countFails++;
                    _testFailed.text = "Tests failed: " + _countFails;
                }

                float percentage = (float)_countSuccess / (_countSuccess + _countFails) * 100;
                _successRate.text = "Success rate: " + percentage.ToString("F2") + "%";
            }
            else
            {
                //ALTERNATIVA CON APPROCCIO PIU' CONSERVATIVO IN CUI SI DISTRUGGE CIO' CHE NON RISPETTA LA CATENA
                //Debug.LogError("Assicurati che i moduli siano posizionati correttamente, un Quality Checker ha ispezionate un oggetto per il quale non era predisposto");
                //Destroy(Resource);
                RepositionResourceFail(Resource);
            }
        }
    }

    public void RepositionResourceSuccess(GameObject resource)
    {
        _checkSuccessConvey.GetComponent<Receiver>().ReceiveResource(resource);
    }
    public void RepositionResourceFail(GameObject resource)
    {
        _checkFailedConvey.GetComponent<Receiver>().ReceiveResource(resource);
    }
}
