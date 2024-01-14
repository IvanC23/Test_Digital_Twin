using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Assembler3 : MonoBehaviour, Receiver
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite3Prefab;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _creationTime;
    [SerializeField] private TextMeshPro _unit1Needed;
    [SerializeField] private TextMeshPro _unit1Inside;
    [SerializeField] private TextMeshPro _unit2Needed;
    [SerializeField] private TextMeshPro _unit2Inside;
    [SerializeField] private TextMeshPro _unitsProduced;

    private Queue<GameObject> _composite2Collected = new Queue<GameObject>();
    private int _composite2Needed = 1;
    private Queue<GameObject> _detailCollected = new Queue<GameObject>();
    private int _detailNeeded = 1;
    private int _countUnit1Inside = 0;
    private int _countUnit2Inside = 0;
    private int _countUnitsProduced = 0;

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Assembler3";
        _creationTime.text = "Creation time: " + _timeToAssemble + "s";
        _unit1Needed.text = "Details needed: " + _detailNeeded.ToString();
        _unit1Inside.text = "Details inside: " + _countUnit1Inside.ToString();
        _unit2Needed.text = "Composite needed: " + _composite2Needed.ToString();
        _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();
        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();

    }


    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Composite2>() != null)
            {
                Resource.SetActive(false);

                _composite2Collected.Enqueue(Resource);

                _countUnit2Inside++;
                _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else if (Resource.GetComponent<Detail>() != null)
            {
                Resource.SetActive(false);

                _detailCollected.Enqueue(Resource);

                _countUnit1Inside++;
                _unit1Inside.text = "Detail inside: " + _countUnit1Inside.ToString();

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

        _countUnit1Inside--;
        _countUnit2Inside--;
        _unit1Inside.text = "Detail inside: " + _countUnit1Inside.ToString();
        _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();

        _countUnitsProduced++;
        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();




        _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

        Destroy(composite2);
        Destroy(detail);
    }





}
