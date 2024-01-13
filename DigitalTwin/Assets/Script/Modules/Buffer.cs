using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private float _timeToBuff = 5.0f;

    private Queue<GameObject> _resourcesCollected = new Queue<GameObject>();
    private bool _isWaiting = false;

    void Receiver.ReceiveResource(GameObject Resource){
        if (Resource.CompareTag("Resource"))
        {
            _resourcesCollected.Enqueue(Resource);
            Resource.SetActive(false);
            if (!_isWaiting)
            {
                StartCoroutine(StoreAndSend());
            }
        }
    }

    private IEnumerator StoreAndSend()
    {
        _isWaiting = true;
        GameObject resource = _resourcesCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToBuff);

        resource.SetActive(true);

        _convey.GetComponent<Receiver>().ReceiveResource(resource);


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
