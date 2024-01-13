using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Parametro configurabile")]
    [SerializeField] private float _actualScale = 1.0f;

    [Header("Parametro configurabile")]
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] private TMP_Text _scaleTimeText;


    private bool _inPause = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _inPause = !_inPause;

            managePause();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_actualScale > 0.21f)
            {
                _actualScale -= 0.2f;
                if (!_inPause)
                {
                    Time.timeScale = _actualScale;
                    _scaleTimeText.text = "x" + Math.Floor(_actualScale * 10) / 10;
                }
            }else{
                _actualScale = 0.0f;
                if (!_inPause)
                {
                    Time.timeScale = _actualScale;
                    _scaleTimeText.text = "x" + Math.Floor(_actualScale * 10) / 10;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _actualScale += 0.2f;
            if (!_inPause)
            {
                Time.timeScale = _actualScale;
                _scaleTimeText.text = "x" + Math.Floor(_actualScale * 10) / 10;
            }
        }
    }

    void managePause()
    {
        if (_inPause)
        {
            Time.timeScale = 0f;
            _pauseText.enabled = true;
            _scaleTimeText.enabled = false;
        }
        else
        {
            Time.timeScale = _actualScale;
            _scaleTimeText.text = "x" + Math.Floor(_actualScale * 10) / 10;
            _pauseText.enabled = false;
            _scaleTimeText.enabled = true;
        }
    }
}
