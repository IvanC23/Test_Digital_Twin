using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Assembler2 : MonoBehaviour, Receiver, IAssembler
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _timeToAssemble = 2.0f;

    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private GameObject _composite2Prefab;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _creationTime;
    [SerializeField] private TextMeshPro _unit1Needed;
    [SerializeField] private TextMeshPro _unit1Inside;
    [SerializeField] private TextMeshPro _unit2Needed;
    [SerializeField] private TextMeshPro _unit2Inside;
    [SerializeField] private TextMeshPro _unitsProduced;


    private Queue<GameObject> _composite1Collected = new Queue<GameObject>();
    private int _composite1Needed = 1;
    private Queue<GameObject> _bodyCollected = new Queue<GameObject>();
    private int _bodyNeeded = 1;
    private int _countUnit1Inside = 0;
    private int _countUnit2Inside = 0;
    private int _countUnitsProduced = 0;

    void Awake()
    {
        _nameText.text = "Name: " + gameObject.name;
        _moduleType.text = "Type: Assembler2";
        _creationTime.text = "Creation time: " + _timeToAssemble + "s";
        _unit1Needed.text = "Bodies needed: " + _bodyNeeded.ToString();
        _unit1Inside.text = "Bodies inside: " + _countUnit1Inside.ToString();
        _unit2Needed.text = "Composite needed: " + _composite1Needed.ToString();
        _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();
        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();

    }




    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            if (Resource.GetComponent<Composite1>() != null)
            {
                Resource.SetActive(false);

                _composite1Collected.Enqueue(Resource);

                _countUnit2Inside++;
                _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();

                if (CheckToAssemble())
                {
                    StartCoroutine(AssembleAndSend());
                }
            }
            else if (Resource.GetComponent<Body>() != null)
            {
                Resource.SetActive(false);

                _bodyCollected.Enqueue(Resource);
                _countUnit1Inside++;
                _unit1Inside.text = "Bodies inside: " + _countUnit1Inside.ToString();

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

        
        _countUnit1Inside--;
        _unit1Inside.text = "Bodies inside: " + _countUnit1Inside.ToString();
        _countUnit2Inside--;
        _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();

        _countUnitsProduced++;

        _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();

        Destroy(composite1);
        Destroy(body);
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
