using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Assembler1 : MonoBehaviour, Receiver
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite1Prefab;
    private Queue<GameObject> _baseCollected = new Queue<GameObject>();
    private int _baseNeeded = 2;


    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Base>())
            {
                Resource.SetActive(false);
                _baseCollected.Enqueue(Resource);

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else
            {
                Debug.LogError("A resource not needed is passing through " + gameObject.name);
                _convey.GetComponent<Receiver>().ReceiveResource(Resource);
            }
        }
    }

    bool CheckToAssemble()
    {
        if (_baseCollected.Count >= _baseNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator AssembleAndSend()
    {
        GameObject base1 = _baseCollected.Dequeue();
        GameObject base2 = _baseCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToAssemble);

        GameObject instantiatedResource = Instantiate(_composite1Prefab);

        Composite1 compositeComponent = instantiatedResource.GetComponent<Composite1>();

        compositeComponent.SetComponents(base1.GetComponent<Base>(), base2.GetComponent<Base>());

        _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

        Destroy(base1);
        Destroy(base2);
    }
}
