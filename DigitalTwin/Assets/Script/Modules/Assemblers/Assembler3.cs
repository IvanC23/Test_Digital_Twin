using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler3 : MonoBehaviour, Receiver
{
     [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite3Prefab;

    private Queue<GameObject> _composite2Collected = new Queue<GameObject>();
    private int _composite2Needed = 1;
    private Queue<GameObject> _detailCollected = new Queue<GameObject>();
    private int _detailNeeded = 1;


    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Composite2>() != null)
            {
                Resource.SetActive(false);

                _composite2Collected.Enqueue(Resource);

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else if (Resource.GetComponent<Detail>() != null)
            {
                Resource.SetActive(false);

                _detailCollected.Enqueue(Resource);

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
        if (_composite2Collected.Count >= _composite2Needed && _detailCollected.Count >= _detailNeeded)
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
        GameObject composite2 = _composite2Collected.Dequeue();
        GameObject detail = _detailCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToAssemble);

        GameObject instantiatedResource = Instantiate(_composite3Prefab);

        Composite3 compositeComponent = instantiatedResource.GetComponent<Composite3>();

        compositeComponent.SetComponents(detail.GetComponent<Detail>(), composite2.GetComponent<Composite2>());

        _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

        Destroy(composite2);
        Destroy(detail);
    }





}
