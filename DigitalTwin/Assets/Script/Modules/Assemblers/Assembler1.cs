using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using TMPro;


public class Assembler1 : MonoBehaviour, Receiver, IAssembler
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite1Prefab;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _creationTime;
    [SerializeField] private TextMeshPro _unitsNeeded;
    [SerializeField] private TextMeshPro _unitsInside;
    [SerializeField] private TextMeshPro _unitsProduced;
    private Queue<GameObject> _baseCollected = new Queue<GameObject>();
    private int _baseNeeded = 2;
    private int _countUnitsProduced = 0;
    private int _countUnitsInside = 0;

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Assembler1";
        _creationTime.text = "Creation time: " + _timeToAssemble + "s";
        _unitsNeeded.text = "Bases needed: " + _baseNeeded.ToString();
        _unitsInside.text = "Bases inside: " + _countUnitsInside.ToString();
        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();

    }


    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Base>())
            {
                Resource.SetActive(false);
                _baseCollected.Enqueue(Resource);

                _countUnitsInside++;
                _unitsInside.text = "Bases inside: " + _countUnitsInside.ToString();

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else
            {
                //Debug.LogError("A resource not needed is passing through " + gameObject.name);
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

        _countUnitsInside -= 2;
        _unitsInside.text = "Bases inside: " + _countUnitsInside.ToString();

        _countUnitsProduced++;
        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();


        Destroy(base1);
        Destroy(base2);
    }

    public float GetAssembleTime()
    {
        return _timeToAssemble;
    }
    public void SetAssembleTime(float NewTime)
    {
        _timeToAssemble = NewTime;
        _creationTime.text = "Creation time: " + _timeToAssemble + "s";
    }
}
