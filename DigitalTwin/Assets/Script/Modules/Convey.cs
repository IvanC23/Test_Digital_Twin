using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _nextComponentPosition;

    [Header("Parametri configurabili")]
    [SerializeField] private float _speedMeterPerSecond = 1f;


    
    private List<GameObject> _objectsToMove = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            _objectsToMove.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            _objectsToMove.Remove(other.gameObject);
        }
    }

    void Update()
    {
        MoveObjects();
    }

    private void MoveObjects()
    {
        for (int i = _objectsToMove.Count - 1; i >= 0; i--)
        {
            if (_objectsToMove[i] != null && _objectsToMove[i].activeSelf)
            {
                GameObject currentObject = _objectsToMove[i];
                MoveObject(currentObject);
            }
            else
            {
                _objectsToMove.Remove(_objectsToMove[i]);
            }
        }
    }

    private void MoveObject(GameObject obj)
    {
        float step = _speedMeterPerSecond * Time.deltaTime;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, _endPosition.position, step);
        if(obj.transform.position == _endPosition.position){
            _objectsToMove.Remove(obj);
            obj.transform.position = _nextComponentPosition.position;
        }
    }

    public Vector3 GetStartPosition()
    {
        return _startPosition.position;
    }

}
