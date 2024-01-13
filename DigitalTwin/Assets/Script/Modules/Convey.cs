using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _nextComponentPosition;

    [Header("Parametri configurabili")]
    [SerializeField] private float _speedMeterPerSecond = 1f;



    private List<GameObject> _resourceToMove = new List<GameObject>();


    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            Vector3 start = _startPosition.position;
            Vector3 positionPlusOffset = new Vector3(start.x, start.y + Resource.GetComponent<HeightSender>().GetHeight()/2, start.z);

            Resource.transform.position = positionPlusOffset;
            Resource.transform.rotation = transform.rotation;
            _resourceToMove.Add(Resource);
        }
    }

    void Update()
    {
        MoveObjects();
    }

    private void MoveObjects()
    {
        for (int i = _resourceToMove.Count - 1; i >= 0; i--)
        {
            MoveObject(_resourceToMove[i]);
        }
    }

    private void MoveObject(GameObject obj)
    {
        float step = _speedMeterPerSecond * Time.deltaTime;
        Vector3 targetPosition = _endPosition.position;
        targetPosition.y = obj.transform.position.y;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, step);
        if (obj.transform.position == targetPosition)
        {
            _resourceToMove.Remove(obj);
            obj.transform.position = _nextComponentPosition.position;
            _nextComponentPosition.gameObject.GetComponent<Receiver>().ReceiveResource(obj);
        }
    }

}
