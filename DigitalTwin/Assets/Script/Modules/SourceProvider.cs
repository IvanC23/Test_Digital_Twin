using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SourceProvider : MonoBehaviour
{
    [Header("Parametri configurabili")]
    [SerializeField] private ResourceTypes.Resources _selectedResource = ResourceTypes.Resources.Base;
    [SerializeField] private float _timeToSpawn = 4.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private List<GameObject> _resourcesPrefabs;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _creationType;
    [SerializeField] private TextMeshPro _creationTime;
    [SerializeField] private TextMeshPro _unitsProduced;

    private char[] _charForBodyKey = { 'A', 'B', 'C' };
    private GameObject _selectedResourcePrefab;
    const string CARATTERI = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private int _countUnits = 0;
    private bool _isStarted = false;


    void Awake()
    {
        TimeManager.SimulationStarted += OnSimulationStarted;

        _selectedResourcePrefab = _resourcesPrefabs[(int)_selectedResource];

        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: SourceProvider";
        _creationType.text = "Creating: " + _selectedResource;
        _creationTime.text = "Creation time: " + _timeToSpawn + "s";
        _unitsProduced.text = "Units created: " + _countUnits.ToString();

    }

    void OnDestroy()
    {
        TimeManager.SimulationStarted -= OnSimulationStarted;
    }

    private void OnSimulationStarted()
    {
        _isStarted = true;
        InvokeRepeating("SpawnResourceAtInterval", 0f, _timeToSpawn);
    }



    private void SpawnResourceAtInterval()
    {
        //Funzione chiamata ogni _timeToSpawn per generare una risorsa
        //I vari branch sono gestiti in maniera analoga

        switch (_selectedResource)
        {
            case ResourceTypes.Resources.Base:
                {
                    //Generazione parametri della risorsa

                    string uniqueID = GenerateRandomAlphanumericCode(6);
                    float X = Random.Range(0, 101);
                    Color randomColor = new Color(Random.value, Random.value, Random.value);

                    //Istanziamento della risorsa

                    GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
                    instantiatedResource.GetComponent<Base>().SetCommonValues(ID: uniqueID, Color: randomColor);
                    instantiatedResource.GetComponent<Base>().SetKeyAttribute(X: X);

                    //Invio al canale di uscita

                    _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
                    break;
                }
            case ResourceTypes.Resources.Body:
                {
                    string uniqueID = GenerateRandomAlphanumericCode(6);
                    char Y = GenerateRandomChar();
                    Color randomColor = new Color(Random.value, Random.value, Random.value);

                    GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
                    instantiatedResource.GetComponent<Body>().SetCommonValues(ID: uniqueID, Color: randomColor);
                    instantiatedResource.GetComponent<Body>().SetKeyAttribute(Y: Y);
                    _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
                    break;
                }
            case ResourceTypes.Resources.Detail:
                {
                    string uniqueID = GenerateRandomAlphanumericCode(6);
                    float Z = Random.Range(-30, 31);
                    Color randomColor = new Color(Random.value, Random.value, Random.value);

                    GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
                    instantiatedResource.GetComponent<Detail>().SetCommonValues(ID: uniqueID, Color: randomColor);
                    instantiatedResource.GetComponent<Detail>().SetKeyAttribute(Z: Z);
                    _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
                    break;
                }
            default:
                break;
        }

        // Aumento counter delle unità generate per aggiornare la UI

        _countUnits++;
        _unitsProduced.text = "Units created: " + _countUnits.ToString();
    }

    //Generatore della stringa alfanumerica necessaria a ogni risorsa
    private string GenerateRandomAlphanumericCode(int length)
    {
        return new string(Enumerable.Repeat(CARATTERI, length)
          .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }

    //Generatore del char utile per la risorsa Body
    private char GenerateRandomChar()
    {
        return _charForBodyKey[Random.Range(0, 3)];
    }

    //INTERAZIONI UI
    //Metodi getter e setter del TimeToSpawn necessari per le interazioni tramite configurazione

    public float GetTimeToSpawn()
    {
        return _timeToSpawn;
    }
    public void SetNewTimeToSpawn(float NewValue)
    {
        _timeToSpawn = NewValue;
        _creationTime.text = "Creation time: " + _timeToSpawn + "s";
        if (_isStarted)
        {
            CancelInvoke("SpawnResourceAtInterval");
            InvokeRepeating("SpawnResourceAtInterval", _timeToSpawn, _timeToSpawn);
        }
    }
    public int GetResourceSelected()
    {
        return (int)_selectedResource;
    }
    public void SetNewResourceToProduce(int index)
    {
        if (index == 0)
        {
            _selectedResource = ResourceTypes.Resources.Base;
        }
        else if (index == 1)
        {
            _selectedResource = ResourceTypes.Resources.Body;
        }
        else if (index == 2)
        {
            _selectedResource = ResourceTypes.Resources.Detail;
        }
        _selectedResourcePrefab = _resourcesPrefabs[(int)_selectedResource];

        _creationType.text = "Creating: " + _selectedResource;

    }
}
