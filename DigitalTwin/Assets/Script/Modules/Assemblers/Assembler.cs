using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Assembler : MonoBehaviour, Receiver
{
    [Header("Parametri configurabili")]
    [SerializeField] private EnumAssembler.Assemblers _selectedAssembler = EnumAssembler.Assemblers.Assembler1;
    [SerializeField] private float _timeToAssemble = 2.0f;
    [Header("Parametri necessari")]
    [SerializeField] private Convey _convey;
    [SerializeField] private List<GameObject> _compositePrefabs;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _moduleType;
    [SerializeField] private TextMeshPro _creationTime;
    [SerializeField] private TextMeshPro _unit1Needed;
    [SerializeField] private TextMeshPro _unit1Inside;
    [SerializeField] private TextMeshPro _unit2Needed;
    [SerializeField] private TextMeshPro _unit2Inside;
    [SerializeField] private TextMeshPro _unitsProduced;

    private Queue<GameObject> _basesCollected = new Queue<GameObject>();
    private int _basesNeeded = 2;
    private Queue<GameObject> _composite1Collected = new Queue<GameObject>();
    private int _composite1Needed = 1;
    private Queue<GameObject> _bodyCollected = new Queue<GameObject>();
    private int _bodyNeeded = 1;
    private Queue<GameObject> _composite2Collected = new Queue<GameObject>();
    private int _composite2Needed = 1;
    private Queue<GameObject> _detailCollected = new Queue<GameObject>();
    private int _detailNeeded = 1;


    private int _countUnit1Inside = 0;
    private int _countUnit2Inside = 0;
    private int _countUnitsProduced = 0;

    void Awake()
    {
        switch (_selectedAssembler)
        {
            case EnumAssembler.Assemblers.Assembler1:
                {
                    _nameText.text = "Name: " + gameObject.name;
                    _moduleType.text = "Type: Assembler1";
                    _creationTime.text = "Creation time: " + _timeToAssemble + "s";
                    _unit1Needed.text = "Base needed: " + _basesNeeded.ToString();
                    _unit1Inside.text = "Base inside: " + _countUnit1Inside.ToString();
                    _unit2Needed.text = "";
                    _unit2Inside.text = "";
                    _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();
                    break;
                }
            case EnumAssembler.Assemblers.Assembler2:
                {
                    _nameText.text = "Name: " + gameObject.name;
                    _moduleType.text = "Type: Assembler2";
                    _creationTime.text = "Creation time: " + _timeToAssemble + "s";
                    _unit1Needed.text = "Bodies needed: " + _bodyNeeded.ToString();
                    _unit1Inside.text = "Bodies inside: " + _countUnit1Inside.ToString();
                    _unit2Needed.text = "Composite needed: " + _composite1Needed.ToString();
                    _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();
                    _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();
                    break;
                }
            case EnumAssembler.Assemblers.Assembler3:
                {
                    _nameText.text = "Name: " + gameObject.name;
                    _moduleType.text = "Type: Assembler3";
                    _creationTime.text = "Creation time: " + _timeToAssemble + "s";
                    _unit1Needed.text = "Details needed: " + _detailNeeded.ToString();
                    _unit1Inside.text = "Details inside: " + _countUnit1Inside.ToString();
                    _unit2Needed.text = "Composite needed: " + _composite2Needed.ToString();
                    _unit2Inside.text = "Composite inside: " + _countUnit2Inside.ToString();
                    _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();
                    break;
                }
            default:
                break;
        }

    }

    void Receiver.ReceiveResource(GameObject Resource)
    {
        if (Resource.CompareTag("Resource"))
        {
            switch (_selectedAssembler)
            {
                case EnumAssembler.Assemblers.Assembler1:
                    {
                        if (Resource.GetComponent<Base>())
                        {
                            Resource.SetActive(false);
                            _basesCollected.Enqueue(Resource);

                            _countUnit1Inside++;
                            _unit1Inside.text = "Bases inside: " + _countUnit1Inside.ToString();

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
                        break;
                    }
                case EnumAssembler.Assemblers.Assembler2:
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
                        break;
                    }
                case EnumAssembler.Assemblers.Assembler3:
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
                        break;
                    }
                default:
                    break;
            }
        }

    }

    private IEnumerator AssembleAndSend()
    {
        switch (_selectedAssembler)
        {
            case EnumAssembler.Assemblers.Assembler1:
                {
                    GameObject base1 = _basesCollected.Dequeue();
                    GameObject base2 = _basesCollected.Dequeue();

                    yield return new WaitForSecondsRealtime(_timeToAssemble);
                    

                    GameObject instantiatedResource = Instantiate(_compositePrefabs[(int)EnumAssembler.Assemblers.Assembler1]);

                    Composite1 compositeComponent = instantiatedResource.GetComponent<Composite1>();

                    compositeComponent.SetComponents(base1.GetComponent<Base>(), base2.GetComponent<Base>());

                    _convey.GetComponent<Receiver>().ReceiveResource(instantiatedResource);

                    _countUnit1Inside -= 2;
                    _unit1Inside.text = "Bases inside: " + _countUnit1Inside.ToString();

                    _countUnitsProduced++;
                    _unitsProduced.text = "Units created: " + _countUnitsProduced.ToString();

                    Destroy(base1);
                    Destroy(base2);
                    
                    break;
                }
            case EnumAssembler.Assemblers.Assembler2:
                {
                    GameObject composite1 = _composite1Collected.Dequeue();
                    GameObject body = _bodyCollected.Dequeue();

                    yield return new WaitForSecondsRealtime(_timeToAssemble);

                    GameObject instantiatedResource = Instantiate(_compositePrefabs[(int)EnumAssembler.Assemblers.Assembler2]);

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

                    break;
                }
            case EnumAssembler.Assemblers.Assembler3:
                {
                    GameObject composite2 = _composite2Collected.Dequeue();
                    GameObject detail = _detailCollected.Dequeue();

                    yield return new WaitForSecondsRealtime(_timeToAssemble);

                    GameObject instantiatedResource = Instantiate(_compositePrefabs[(int)EnumAssembler.Assemblers.Assembler3]);

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

                    break;
                }
            default:
                break;
        }
    }

    bool CheckToAssemble()
    {
        switch (_selectedAssembler)
        {
            case EnumAssembler.Assemblers.Assembler1:
                {
                    if (_basesCollected.Count >= _basesNeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case EnumAssembler.Assemblers.Assembler2:
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
            case EnumAssembler.Assemblers.Assembler3:
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
            default:
                return false;
        }
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
