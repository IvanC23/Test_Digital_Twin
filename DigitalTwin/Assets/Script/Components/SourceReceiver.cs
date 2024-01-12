using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceReceiver : MonoBehaviour
{
    private List<GameObject> _resourcesCollected = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            _resourcesCollected.Add(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
