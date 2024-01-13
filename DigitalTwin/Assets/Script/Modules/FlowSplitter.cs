using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowSplitter : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private List<ConveyWithWeight> _conveysWithWeight;

    private List<GameObject> _resourcesCollected = new List<GameObject>();

    void Awake()
    {
        float totalWeight = 0;

        for (int i = 0; i < _conveysWithWeight.Count; i++)
        {
            totalWeight += _conveysWithWeight[i]._weight;
        }

        if (totalWeight != 1.0f)
        {
            Debug.LogError("All'interno di un flow splitter, il peso totale assegnato ai vari canali é " + totalWeight + " mentre dovrebbe essere 1.0");
        }
    }

    void Receiver.ReceiveResource(GameObject Resource){
        if (Resource.CompareTag("Resource"))
        {
            RepositionResource(Resource);
        }
    }

    

    public void RepositionResource(GameObject resource)
    {
        //Calcolo della probabilità che esca su un determinato canale tramite media ponderata e poi lo sposta sul canale

        float randomValue = Random.value;
        float cumulativeWeight = 0;

        for (int i = 0; i < _conveysWithWeight.Count; i++)
        {
            cumulativeWeight += _conveysWithWeight[i]._weight;

            if (randomValue <= cumulativeWeight)
            {
                _conveysWithWeight[i]._convey.GetComponent<Receiver>().ReceiveResource(resource);
                break;
            }
        }

    }
}

[System.Serializable]
public struct ConveyWithWeight
{
    public Convey _convey;
    public float _weight;
}
