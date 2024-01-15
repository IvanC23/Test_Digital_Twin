using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private GameObject _detailPanel;
    [SerializeField] private GameObject _suggestions;
    [SerializeField] private GameObject _welcomePanel;
    [SerializeField] private GameObject _settingsPanel;
    private bool _detailWasActive;
    private bool _welcomeWasActive;




    void Awake()
    {
        TimeManager.SimulationStarted += OnSimulationStarted;
        _detailWasActive = _detailPanel.activeSelf;
        _welcomeWasActive = _welcomePanel.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _detailPanel.SetActive(!_detailPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _suggestions.SetActive(!_suggestions.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (_settingsPanel.activeSelf)
            {
                _detailPanel.SetActive(_detailWasActive);
                _welcomePanel.SetActive(_welcomeWasActive);
            }
            else
            {
                _detailWasActive = _detailPanel.activeSelf;
                _welcomeWasActive = _welcomePanel.activeSelf;

                _detailPanel.SetActive(false);
                _welcomePanel.SetActive(false);
            }
            _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        }
    }

    void OnDestroy()
    {
        TimeManager.SimulationStarted -= OnSimulationStarted;
    }

    private void OnSimulationStarted()
    {
        _welcomePanel.SetActive(false);
    }

}
