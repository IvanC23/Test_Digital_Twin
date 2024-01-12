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

        GameObject baseGameObject = Instantiate(_composite1Prefab, _spawnPoint, _convey.transform.rotation);

        Composite1 compositeComponent = baseGameObject.GetComponent<Composite1>();

        var newPosition = new UnityEngine.Vector3(_spawnPoint.x, compositeComponent.GetHeight() / 2 + _spawnPoint.y, _spawnPoint.z);
        baseGameObject.transform.position = newPosition;
        compositeComponent.SetValuesBase1(_base1.GetID(), _base1.GetX(), _base1.GetColor());
        compositeComponent.SetValuesBase2(_base2.GetID(), _base2.GetX(), _base2.GetColor());

        Destroy(_base1.gameObject);
        Destroy(_base2.gameObject);

    }





}