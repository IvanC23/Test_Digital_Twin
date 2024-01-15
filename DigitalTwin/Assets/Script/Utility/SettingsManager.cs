using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Sezione providers")]
    [SerializeField] private TMP_Dropdown _providerDropDown;
    [SerializeField] private TMP_InputField _providerTimeToSpawn;
    [SerializeField] private TMP_Dropdown _resourceDropdown;
    [SerializeField] private List<GameObject> _providerList = new List<GameObject>();

    [Header("Sezione flow splitters")]
    [SerializeField] private TMP_Dropdown _flowSplittersDropDown;
    [SerializeField] private TMP_InputField _weight1;
    [SerializeField] private TMP_InputField _weight2;
    [SerializeField] private List<GameObject> _flowSplittersList = new List<GameObject>();

    [Header("Sezione conveys")]
    [SerializeField] private TMP_Dropdown _conveysDropDown;
    [SerializeField] private TMP_InputField _speed;
    [SerializeField] private List<GameObject> _conveysList = new List<GameObject>();

    [Header("Sezione buffers")]
    [SerializeField] private TMP_Dropdown _buffersDropDown;
    [SerializeField] private TMP_InputField _timeToBuff;
    [SerializeField] private List<GameObject> _buffersList = new List<GameObject>();

    [Header("Sezione assemblers")]
    [SerializeField] private TMP_Dropdown _assemblersDropDown;
    [SerializeField] private TMP_InputField _timeToAssemble;
    [SerializeField] private List<GameObject> _assemblersList = new List<GameObject>();

    //Gestore dei vari componenti del pannello di configurazione, all'interno del quale vi sono componenti
    //dropdown per selezionare una trai vari moduli di una determinata categoria e vari input field per
    //modificarne i parametri durante l'esperienza. Vi é un dropdown aggiuntivo per selezionare la tipologia 
    //di risorsa da produrre tramite source provider.

    private void Start()
    {
        PopulateDropdowns();
    }

    //Popolamento iniziale del menù di configurazione tramite riempimento delle DropdownList e dei campi di testo con le informazioni già presenti
    //Per questioni di leggibilità, sopra ogni sezione vi é indicato a quelo modulo faccia riferimento.

    void PopulateDropdowns()
    {

        //PROVIDERS SECTION

        _providerDropDown.ClearOptions();

        List<string> objectNames = new List<string>();
        foreach (GameObject obj in _providerList)
        {
            objectNames.Add(obj.name);
        }
        _providerDropDown.AddOptions(objectNames);

        _providerTimeToSpawn.text = _providerList[0].GetComponent<SourceProvider>().GetTimeToSpawn().ToString();

        _resourceDropdown.ClearOptions();
        List<string> resourceNames = new List<string>(System.Enum.GetNames(typeof(ResourceTypes.Resources)));
        _resourceDropdown.AddOptions(resourceNames);
        _resourceDropdown.value = _providerList[0].GetComponent<SourceProvider>().GetResourceSelected();

        //FLOW SPLITTERS SECTION

        _flowSplittersDropDown.ClearOptions();
        objectNames = new List<string>();
        foreach (GameObject obj in _flowSplittersList)
        {
            objectNames.Add(obj.name);
        }
        _flowSplittersDropDown.AddOptions(objectNames);

        _weight1.text = _flowSplittersList[0].GetComponent<FlowSplitter>().GetWeight(0).ToString();
        _weight2.text = _flowSplittersList[0].GetComponent<FlowSplitter>().GetWeight(1).ToString();

        //CONVEYS SECTION

        _conveysDropDown.ClearOptions();

        objectNames = new List<string>();
        foreach (GameObject obj in _conveysList)
        {
            objectNames.Add(obj.name);
        }
        _conveysDropDown.AddOptions(objectNames);

        _speed.text = _conveysList[0].GetComponent<Convey>().GetSpeed().ToString();

        //BUFFERS SECTION

        _buffersDropDown.ClearOptions();

        objectNames = new List<string>();
        foreach (GameObject obj in _buffersList)
        {
            objectNames.Add(obj.name);
        }
        _buffersDropDown.AddOptions(objectNames);

        _timeToBuff.text = _buffersList[0].GetComponent<Buffer>().GetTimeToBuff().ToString();

        //ASSEMBLERS SECTION

        _assemblersDropDown.ClearOptions();

        objectNames = new List<string>();
        foreach (GameObject obj in _assemblersList)
        {
            objectNames.Add(obj.name);
        }
        _assemblersDropDown.AddOptions(objectNames);

        _timeToAssemble.text = _assemblersList[0].GetComponent<IAssembler>().GetAssembleTime().ToString();

    }

    
    
    //Gestione input, qui ci sono le funzioni chiamate dai vari componenti del menù quando vi si interagisce
    //Dopo aver selezionato un componente dalla dropdownlist, viene chiamata una funzione Select in cui si ripopolano
    //i campi con le informazioni del componente selezionato. Dopo aver cambiato uno qualsiasi dei campi, viene invece
    //chiamata una funzione change per risettarli all'interno del componente. 

    //Nel parsing del valore ottenuto tramite interfaccia, ci assicuriamo che sia un valore maggiore di 0 e che non contenga
    //caratteri.

    //PROVIDERS SECTION

    public void SelectProvider()
    {
        int selectedIndex = _providerDropDown.value;
        GameObject selectedObject = _providerList[selectedIndex];

        _providerTimeToSpawn.text = selectedObject.GetComponent<SourceProvider>().GetTimeToSpawn().ToString();
        _resourceDropdown.value = selectedObject.GetComponent<SourceProvider>().GetResourceSelected();
    }

    public void ChangeTimeToSpawnProvider()
    {
        // Aggiorna il parametro 1 dell'oggetto selezionato quando l'input cambia
        int selectedIndex = _providerDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _providerList.Count)
        {
            float parsedValue;
            if (float.TryParse(_providerTimeToSpawn.text, out parsedValue))
            {
                if (parsedValue > 0)
                {
                    _providerList[selectedIndex].GetComponent<SourceProvider>().SetNewTimeToSpawn(parsedValue);
                }
            }
            else
            {
                if (_providerTimeToSpawn.text.Length == 0)
                {
                    _providerTimeToSpawn.text = "";

                }
                else
                {
                    _providerTimeToSpawn.text = _providerTimeToSpawn.text.Substring(0, _providerTimeToSpawn.text.Length - 1);
                }
            }
        }
    }

    public void ChangeResourceDoneProvider()
    {
        int selectedIndex = _providerDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _providerList.Count)
        {
            int selectedResource = _resourceDropdown.value;
            if (selectedResource >= 0 && selectedResource < 3)
            {
                _providerList[selectedIndex].GetComponent<SourceProvider>().SetNewResourceToProduce(selectedResource);
            }
        }
    }

    //FLOW SPLITTERS SECTION
    public void SelectFlowSplitter()
    {
        int selectedIndex = _flowSplittersDropDown.value;
        GameObject selectedObject = _flowSplittersList[selectedIndex];

        _weight1.text = selectedObject.GetComponent<FlowSplitter>().GetWeight(0).ToString();
        _weight2.text = selectedObject.GetComponent<FlowSplitter>().GetWeight(1).ToString();
    }

    public void OnChangeWeight1()
    {
        // Aggiorna il parametro 1 dell'oggetto selezionato quando l'input cambia
        int selectedIndex = _flowSplittersDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _flowSplittersList.Count)
        {
            float parsedValue;
            if (float.TryParse(_weight1.text, out parsedValue))
            {
                if (parsedValue > 0 && parsedValue <= 1.0f)
                {
                    _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().SetWeight1(parsedValue);

                    _weight2.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(1).ToString();
                }
            }
            else
            {
                if (_weight1.text.Length == 0)
                {
                    _weight1.text = "";

                }
                else
                {
                    _weight1.text = _weight1.text.Substring(0, _weight1.text.Length - 1);
                }
            }
        }
    }

    public void OnChangeWeight2()
    {
        int selectedIndex = _flowSplittersDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _flowSplittersList.Count)
        {
            float parsedValue;
            if (float.TryParse(_weight2.text, out parsedValue))
            {
                if (parsedValue > 0 && parsedValue <= 1.0f)
                {
                    _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().SetWeight2(parsedValue);

                    _weight2.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(1).ToString();
                }
            }
            else
            {
                if (_weight2.text.Length == 0)
                {
                    _weight2.text = "";

                }
                else
                {
                    _weight2.text = _weight2.text.Substring(0, _weight2.text.Length - 1);
                }
            }
        }
    }

    //CONVEY SECTION

    public void SelectConvey()
    {
        int selectedIndex = _conveysDropDown.value;
        GameObject selectedObject = _conveysList[selectedIndex];

        _speed.text = selectedObject.GetComponent<Convey>().GetSpeed().ToString();
    }

    public void ChangeSpeed()
    {
        // Aggiorna il parametro 1 dell'oggetto selezionato quando l'input cambia
        int selectedIndex = _conveysDropDown.value;
        
        if (selectedIndex >= 0 && selectedIndex < _conveysList.Count)
        {
            float parsedValue;
            if (float.TryParse(_speed.text, out parsedValue))
            {
                if (parsedValue > 0)
                {
                    _conveysList[selectedIndex].GetComponent<Convey>().SetSpeed(parsedValue);
                }
            }
            else
            {
                if (_speed.text.Length == 0)
                {
                    _speed.text = "";

                }
                else
                {
                    _speed.text = _speed.text.Substring(0, _speed.text.Length - 1);
                }
            }
        }
    }

    //BUFFERS SECTION

    public void SelectBuffer()
    {
        int selectedIndex = _buffersDropDown.value;
        GameObject selectedObject = _buffersList[selectedIndex];

        _timeToBuff.text = selectedObject.GetComponent<Buffer>().GetTimeToBuff().ToString();
    }

    public void ChangeBuffering()
    {
        // Aggiorna il parametro 1 dell'oggetto selezionato quando l'input cambia
        int selectedIndex = _buffersDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _buffersList.Count)
        {
            float parsedValue;
            if (float.TryParse(_timeToBuff.text, out parsedValue))
            {
                if (parsedValue > 0)
                {
                    _buffersList[selectedIndex].GetComponent<Buffer>().SetTimeToBuff(parsedValue);
                }
            }
            else
            {
                if (_timeToBuff.text.Length == 0)
                {
                    _timeToBuff.text = "";

                }
                else
                {
                    _timeToBuff.text = _timeToBuff.text.Substring(0, _timeToBuff.text.Length - 1);
                }
            }
        }
    }

    //ASSEMBLERS SECTION

    public void SelectAssembler()
    {
        int selectedIndex = _assemblersDropDown.value;
        GameObject selectedObject = _assemblersList[selectedIndex];

        _timeToAssemble.text = selectedObject.GetComponent<IAssembler>().GetAssembleTime().ToString();
    }

    public void ChangeAssembleTime()
    {
        // Aggiorna il parametro 1 dell'oggetto selezionato quando l'input cambia
        int selectedIndex = _assemblersDropDown.value;
        if (selectedIndex >= 0 && selectedIndex < _assemblersList.Count)
        {
            float parsedValue;
            if (float.TryParse(_timeToAssemble.text, out parsedValue))
            {
                if (parsedValue > 0)
                {
                    _assemblersList[selectedIndex].GetComponent<IAssembler>().SetAssembleTime(parsedValue);
                }
            }
            else
            {
                if (_timeToAssemble.text.Length == 0)
                {
                    _timeToAssemble.text = "";

                }
                else
                {
                    _timeToAssemble.text = _timeToAssemble.text.Substring(0, _timeToAssemble.text.Length - 1);
                }
            }
        }
    }

}
