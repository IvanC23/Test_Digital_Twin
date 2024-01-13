using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceReceiver : MonoBehaviour, Receiver
{
    private List<GameObject> _resourcesCollected = new List<GameObject>();

    public void ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            _resourcesCollected.Add(Resource);
            Destroy(Resource);
        }
    }

}
