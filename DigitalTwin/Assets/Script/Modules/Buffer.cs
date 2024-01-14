using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buffer : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private float _timeToBuff = 5.0f;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _timeBuff;
    [SerializeField] private TextMeshPro _unitsInside;
    [SerializeField] private TextMeshPro _unitsSent;

    private int _countInside = 0;
    private int _countSent = 0;

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Buffer";
        _timeBuff.text = "Buffering time: " + _timeToBuff;
        _unitsInside.text = "Units inside: " + _countInside;
        _unitsSent.text = "Units released: " + _countSent;
    }

    private Queue<GameObject> _resourcesCollected = new Queue<GameObject>();
    private bool _isWaiting = false;

    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            _resourcesCollected.Enqueue(Resource);

            _countInside++;
            _unitsInside.text = "Units inside: " + _countInside;

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

        _countInside--;
        _countSent++;
        _unitsInside.text = "Units inside: " + _countInside;
        _unitsSent.text = "Units released: " + _countSent;


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
