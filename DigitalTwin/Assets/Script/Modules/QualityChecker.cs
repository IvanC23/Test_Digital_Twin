using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityChecker : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private Convey _checkSuccessConvey;
    [SerializeField] private Convey _checkFailedConvey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            if (other.gameObject.GetComponentsInChildren<Base>()[0].GetX() + other.gameObject.GetComponentsInChildren<Base>()[1].GetX() > 100)
            {
                RepositionResourceSuccess(other.gameObject);
            }
            else
            {
                RepositionResourceFail(other.gameObject);
            }
        }
    }

    public void RepositionResourceSuccess(GameObject resource)
    {
        resource.transform.position = _checkSuccessConvey.GetStartPosition();
        resource.transform.rotation = _checkSuccessConvey.transform.rotation;
    }
    public void RepositionResourceFail(GameObject resource)
    {
        resource.transform.position = _checkFailedConvey.GetStartPosition();
        resource.transform.rotation = _checkFailedConvey.transform.rotation;
    }
}
