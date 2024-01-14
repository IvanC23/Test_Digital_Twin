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

    }

    //PROVIDERS SECTION

    public void SelectProvider()
    {
        int selectedIndex = _providerDropDown.value;
        GameObject selectedObject = _providerList[selectedIndex];

        //Aggiorna gli input con i parametri dell'oggetto selezionato
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
                Debug.Log("Parsed value: " + parsedValue.ToString());

                if (parsedValue > 0)
                {
                    _providerList[selectedIndex].GetComponent<SourceProvider>().SetNewTimeToSpawn(parsedValue);
                }
            }
        }
    }

    public void ChangeResourceDoneProvider()
    {
        // Aggiorna il parametro 2 dell'oggetto selezionato quando l'input cambia
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
}
