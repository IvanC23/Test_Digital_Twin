using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private GameObject _detailPanel;
    [SerializeField] private GameObject _suggestions;
    [SerializeField] private GameObject _welcomePanel;

    void Awake()
    {
        TimeManager.SimulationStarted += OnSimulationStarted;
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
