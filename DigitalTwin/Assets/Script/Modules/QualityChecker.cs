using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityChecker : MonoBehaviour,Receiver
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _checkSuccessConvey;
    [SerializeField] private Convey _checkFailedConvey;

     void Receiver.ReceiveResource(GameObject Resource){
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponentsInChildren<Base>()[0].GetX() + Resource.GetComponentsInChildren<Base>()[1].GetX() > 100)
            {
                RepositionResourceSuccess(Resource);
            }
            else
            {
                RepositionResourceFail(Resource);
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
