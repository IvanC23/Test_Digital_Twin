using System.Collections;
using System.Collections.Generic;
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
    // Update is called once per frame
    void Awake()
    {
        _spawnPoint = _convey.GetStartPosition();
        _selectedPrefab = _resourcesPrefabs[(int)_selectedResource];
        _spawnPlusOffset = new Vector3(_spawnPoint.x, _spawnPoint.y + _selectedPrefab.transform.localScale.y / 2, _spawnPoint.z);
        InvokeRepeating("SpawnResourceAtInterval", 0f, _timeToSpawn);
    }

    private void SpawnResourceAtInterval()
    {
        GameObject spawnedObject = Instantiate(_selectedPrefab, _spawnPlusOffset, Quaternion.identity);

        if (_selectedResource == ResourceTypes.Resources.Base)
        {
            float randomScaleFactor = Random.Range(0f, 1f);
            float newScaleX = spawnedObject.transform.localScale.x + spawnedObject.transform.localScale.x * randomScaleFactor;
            spawnedObject.transform.localScale = new Vector3(newScaleX, spawnedObject.transform.localScale.y, spawnedObject.transform.localScale.z);
        }
    }
}
