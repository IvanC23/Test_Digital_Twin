using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Parametro configurabile")]
    [SerializeField] private float _actualScale = 1.0f;
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
            if (_actualScale >= 0.2f)
            {
                _actualScale -= 0.2f;
                if (!_inPause)
                {
                    Time.timeScale = _actualScale;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _actualScale += 0.2f;
            if (!_inPause)
            {
                Time.timeScale = _actualScale;
            }
        }
    }

    void managePause()
    {
        if (_inPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _actualScale;
        }
    }
}
