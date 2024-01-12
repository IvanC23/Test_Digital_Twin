using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowSplitter : MonoBehaviour
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            RepositionResource(other.gameObject);
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
                resource.transform.position = _conveysWithWeight[i]._convey.GetStartPosition();
                resource.transform.rotation = _conveysWithWeight[i]._convey.transform.rotation;
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
