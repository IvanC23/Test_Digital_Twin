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

    private Vector3 _spawnPoint;
    private Vector3 _spawnPlusOffset;
    private GameObject _selectedPrefab;
    const string CARATTERI = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    void Awake()
    {
        _spawnPoint = _convey.GetStartPosition();
        _selectedPrefab = _resourcesPrefabs[(int)_selectedResource];
        _spawnPlusOffset = new Vector3(_spawnPoint.x, _spawnPoint.y + _selectedPrefab.transform.localScale.y / 2, _spawnPoint.z);
        InvokeRepeating("SpawnResourceAtInterval", 0f, _timeToSpawn);
    }

    private void SpawnResourceAtInterval()
    {

        if (_selectedResource == ResourceTypes.Resources.Base)
        {
            string uniqueID = GenerateRandomAlphanumericCode(6);
            float X = Random.Range(0, 100);
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            GameObject baseGameObject = Instantiate(_selectedPrefab, _spawnPlusOffset, Quaternion.identity);
            baseGameObject.GetComponent<Base>().SetValues(ID: uniqueID, X: X, Color: randomColor);
        }
    }
    private string GenerateRandomAlphanumericCode(int length)
    {
        return new string(Enumerable.Repeat(CARATTERI, length)
          .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }
}
