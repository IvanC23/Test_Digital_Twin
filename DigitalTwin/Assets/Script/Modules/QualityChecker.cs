using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityChecker : MonoBehaviour, Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _checkSuccessConvey;
    [SerializeField] private Convey _checkFailedConvey;

    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponentInChildren<Base>() != null)
            {
                if (Resource.GetComponentsInChildren<Base>()[0].GetKeyAttribute() + Resource.GetComponentsInChildren<Base>()[1].GetKeyAttribute() > 100)
                {
                    RepositionResourceSuccess(Resource);
                }
                else
                {
                    RepositionResourceFail(Resource);
                }
            }
            else
            {
                Debug.LogError("Assicurati che i moduli siano posizionati correttamente, un Quality Checker ha ispezionate un oggetto per il quale non era predisposto");
                Destroy(Resource);
            }
        }
    }

    public void RepositionResourceSuccess(GameObject resource)
    {
        _checkSuccessConvey.GetComponent<Receiver>().ReceiveResource(resource);
    }
    public void RepositionResourceFail(GameObject resource)
    {
        _checkFailedConvey.GetComponent<Receiver>().ReceiveResource(resource);
    }
}
