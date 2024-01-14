using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{
    [Header("Parametro configurabile")]
    [SerializeField] private float _actualScale = 1.0f;

    [Header("Parametro configurabile")]
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] private TMP_Text _scaleTimeText;

    public static event Action SimulationStarted;

    private bool _inPause = false;
    private bool _started = false;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (_started)
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
                }
                else
                {
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

            if (Input.GetKeyDown(KeyCode.J))
            {
                RestartScene();
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.B))
            {
                StartScene();
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            QuitScene();
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

    void StartScene()
    {
        _started = true;
        Time.timeScale = 1.0f;

        SimulationStarted?.Invoke();
    }

    void RestartScene()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
        }
    }

    void QuitScene()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
