using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Assembler1 : MonoBehaviour
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite1Prefab;
    private Queue<GameObject> _baseCollected = new Queue<GameObject>();
    private int _baseNeeded = 2;
    private UnityEngine.Vector3 _spawnPoint;
    void Awake()
    {
        _spawnPoint = _convey.GetStartPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            if (other.gameObject.GetComponent<Base>() != null && other.gameObject.GetComponent<Composite1>() == null)
            {
                other.gameObject.SetActive(false);
                _baseCollected.Enqueue(other.gameObject);

                if (_baseCollected.Count >= _baseNeeded)
                {
                    StartCoroutine(StoreAndSend());
                }
            }
        }
    }

    private IEnumerator StoreAndSend()
    {

        GameObject _base1 = _baseCollected.Dequeue();
        GameObject _base2 = _baseCollected.Dequeue();

        yield return new WaitForSecondsRealtime(_timeToAssemble);

        GameObject baseGameObject = Instantiate(_composite1Prefab, _spawnPoint, _convey.transform.rotation);

        Composite1 compositeComponent = baseGameObject.GetComponent<Composite1>();

        var newPosition = new UnityEngine.Vector3(_spawnPoint.x, compositeComponent.GetHeight() / 2 + _spawnPoint.y, _spawnPoint.z);
        baseGameObject.transform.position = newPosition;
        compositeComponent.SetValuesBase1(_base1.GetComponent<Base>().GetID(), _base1.GetComponent<Base>().GetX(), _base1.GetComponent<Base>().GetColor());
        compositeComponent.SetValuesBase2(_base2.GetComponent<Base>().GetID(), _base2.GetComponent<Base>().GetX(), _base2.GetComponent<Base>().GetColor());


        Destroy(_base1);
        Destroy(_base2);

    }





}
