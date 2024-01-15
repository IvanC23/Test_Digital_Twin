using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SourceReceiver : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _resourcesTaken;
    private List<GameObject> _resourcesCollected = new List<GameObject>();

    //Ricevitore di risorse, si limita ad accumulare le risorse alla fine di un percorso

    //Counter delle risorse accumulate/arrivate.
    private int _countTaken = 0;

    // Riempimento UI modulo iniziale

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Source Receiver";
        _resourcesTaken.text = "Resources collected: " + _countTaken;
    }

    public void ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            _resourcesCollected.Add(Resource);

            _countTaken++;
            _resourcesTaken.text = "Resources collected: " + _countTaken;
            
            Destroy(Resource);
        }
    }

}
