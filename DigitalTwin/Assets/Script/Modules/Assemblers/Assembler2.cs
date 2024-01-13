using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler2 : MonoBehaviour, Receiver
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite2Prefab;


    private Queue<GameObject> _composite1Collected = new Queue<GameObject>();
    private int _composite1Needed = 1;
    private Queue<GameObject> _bodyCollected = new Queue<GameObject>();
    private int _bodyNeeded = 1;



    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Composite1>() != null)
            {
                Resource.SetActive(false);

                _composite1Collected.Enqueue(Resource);

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else if (Resource.GetComponent<Body>() != null)
            {
                Resource.SetActive(false);

                _bodyCollected.Enqueue(Resource);

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
        if (_composite1Collected.Count >= _composite1Needed && _bodyCollected.Count >= _bodyNeeded)
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
        GameObject composite1 = _composite1Collected.Dequeue();
        GameObject body = _bodyCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToAssemble);

        GameObject instantiatedResource = Instantiate(_composite2Prefab);

        Composite2 compositeComponent = instantiatedResource.GetComponent<Composite2>();

        compositeComponent.SetComponents(body.GetComponent<Body>(), composite1.GetComponent<Composite1>());

        _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

        Destroy(composite1);
        Destroy(body);
    }
}
