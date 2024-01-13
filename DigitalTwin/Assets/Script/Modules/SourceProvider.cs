using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SourceProvider : MonoBehaviour
{
    [Header("Parametri configurabili")]
    [SerializeField] private ResourceTypes.Resources _selectedResource = ResourceTypes.Resources.Base;
    [SerializeField] private float _timeToSpawn = 4.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private List<GameObject> _resourcesPrefabs;
    private char[] _charForBodyKey = { 'A', 'B', 'C' };
    private GameObject _selectedResourcePrefab;
    const string CARATTERI = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


    void Awake()
    {

        _selectedResourcePrefab = _resourcesPrefabs[(int)_selectedResource];

        InvokeRepeating("SpawnResourceAtInterval", 0f, _timeToSpawn);
    }

    private void SpawnResourceAtInterval()
    {

        if (_selectedResource == ResourceTypes.Resources.Base)
        {
            string uniqueID = GenerateRandomAlphanumericCode(6);
            float X = Random.Range(0, 101);
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
            instantiatedResource.GetComponent<Base>().SetCommonValues(ID: uniqueID, Color: randomColor);
            instantiatedResource.GetComponent<Base>().SetKeyAttribute(X: X);
            _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
        }
        else if (_selectedResource == ResourceTypes.Resources.Body)
        {
            string uniqueID = GenerateRandomAlphanumericCode(6);
            char Y = GenerateRandomChar();
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
            instantiatedResource.GetComponent<Body>().SetCommonValues(ID: uniqueID, Color: randomColor);
            instantiatedResource.GetComponent<Body>().SetKeyAttribute(Y: Y);
            _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
        }
        else if (_selectedResource == ResourceTypes.Resources.Detail)
        {
            string uniqueID = GenerateRandomAlphanumericCode(6);
            float Z = Random.Range(-30, 31);
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            GameObject instantiatedResource = Instantiate(_selectedResourcePrefab);
            instantiatedResource.GetComponent<Detail>().SetCommonValues(ID: uniqueID, Color: randomColor);
            instantiatedResource.GetComponent<Detail>().SetKeyAttribute(Z: Z);
            _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);
        }
    }
    private string GenerateRandomAlphanumericCode(int length)
    {
        return new string(Enumerable.Repeat(CARATTERI, length)
          .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }
    private char GenerateRandomChar()
    {
        return _charForBodyKey[Random.Range(0, 3)];
    }
}
