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

  
    void Receiver.ReceiveResource(GameObject Resource){
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Base>() != null && Resource.GetComponent<Composite1>() == null)
            {
                Resource.SetActive(false);
                _baseCollected.Enqueue(Resource);

                if (_baseCollected.Count >= _baseNeeded)
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
        }
    }

    private IEnumerator AssembleAndSend()
    {

        Base _base1 = _baseCollected.Dequeue().GetComponent<Base>();
        Base _base2 = _baseCollected.Dequeue().GetComponent<Base>();

        yield return new WaitForSecondsRealtime(_timeToAssemble);

        GameObject instantiatedResource = Instantiate(_composite1Prefab);

        Composite1 compositeComponent = instantiatedResource.GetComponent<Composite1>();

        compositeComponent.SetValuesBase1(_base1.GetID(), _base1.GetX(), _base1.GetColor());
        compositeComponent.SetValuesBase2(_base2.GetID(), _base2.GetX(), _base2.GetColor());

        _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

        Destroy(_base1.gameObject);
        Destroy(_base2.gameObject);

    }





}
