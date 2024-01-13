using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Parametri necessari")]
    [SerializeField] private GameObject _detailPanel;
    [SerializeField] private GameObject _suggestions;

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
}
