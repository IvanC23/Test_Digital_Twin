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

    private void Start()
    {
        PopulateDropdowns();
    }

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

    }

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

                    _weight1.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(0).ToString();
                    _weight2.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(1).ToString();
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

                    _weight1.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(0).ToString();
                    _weight2.text = _flowSplittersList[selectedIndex].GetComponent<FlowSplitter>().GetWeight(1).ToString();
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

    public void OnSpeedChange()
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
                    _speed.text = _conveysList[selectedIndex].GetComponent<Convey>().GetSpeed().ToString();
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

    public void OnBufferingChange()
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
                    _timeToBuff.text = _buffersList[selectedIndex].GetComponent<Buffer>().GetTimeToBuff().ToString();
                }
            }
        }
    }

}
