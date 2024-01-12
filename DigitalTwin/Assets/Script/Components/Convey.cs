using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    [Header("Parametri configurabili")]
    [SerializeField] private float _speedMeterPerSecond = 1f;

    private List<GameObject> _objectsToMove = new List<GameObject>();


    public Vector3 GetStartPosition()
    {
        return _startPosition.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            Debug.Log("Collider Found!");
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
            GameObject currentObject = _objectsToMove[i];
            MoveObject(currentObject);
        }
    }

    private void MoveObject(GameObject obj)
    {
        float step = _speedMeterPerSecond * Time.deltaTime;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, _endPosition.position, step);
    }

}
