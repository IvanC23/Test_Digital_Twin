using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private float _timeToBuff = 5.0f;

    private Queue<GameObject> _resourcesCollected = new Queue<GameObject>();
    private List<GameObject> _resourcesSent = new List<GameObject>();
    private bool _isWaiting = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource") && !_resourcesSent.Contains(other.gameObject))
        {
            _resourcesCollected.Enqueue(other.gameObject);
            other.gameObject.SetActive(false);
            if (!_isWaiting)
            {
                StartCoroutine(StoreAndSend());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Resource") && _resourcesSent.Contains(other.gameObject))
        {
            _resourcesSent.Remove(other.gameObject);
        }
    }

    private IEnumerator StoreAndSend()
    {
        _isWaiting = true;
        GameObject resource = _resourcesCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToBuff);

        _resourcesSent.Add(resource);

        resource.SetActive(true);

        resource.transform.position = _convey.GetStartPosition();
        resource.transform.rotation = _convey.transform.rotation;

        if (_resourcesCollected.Count > 0)
        {
            StartCoroutine(StoreAndSend());
        }
        else
        {
            _isWaiting = false;
        }
    }
}
