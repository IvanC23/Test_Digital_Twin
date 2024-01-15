using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{
    [Header("Parametro configurabile")]
    [SerializeField] private float _actualTimeScale = 1.0f;

    [Header("Parametro configurabile")]
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] private TMP_Text _scaleTimeText;

    public static event Action SimulationStarted;

    private bool _inPause = false;
    private bool _started = false;

    //All'inizio dell'esperienza, la simulazione sarà ferma

    void Awake()
    {
        Time.timeScale = 0f;
    }

    //Gestione del tempo e dell'esperienza (uscita dal gioco/restart esperienza)
    //Lo script é collegato ai Text esterni che danno informazioni sulla scala temporale

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
                if (_actualTimeScale > 0.21f)
                {
                    _actualTimeScale -= 0.2f;
                    if (!_inPause)
                    {
                        Time.timeScale = _actualTimeScale;
                        _scaleTimeText.text = "x" + Math.Floor(_actualTimeScale * 10) / 10;
                    }
                }
                else
                {
                    _actualTimeScale = 0.0f;
                    if (!_inPause)
                    {
                        Time.timeScale = _actualTimeScale;
                        _scaleTimeText.text = "x" + Math.Floor(_actualTimeScale * 10) / 10;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                _actualTimeScale += 0.2f;
                if (!_inPause)
                {
                    Time.timeScale = _actualTimeScale;
                    _scaleTimeText.text = "x" + Math.Floor(_actualTimeScale * 10) / 10;
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

    //Gestione pausa

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
            Time.timeScale = _actualTimeScale;
            _scaleTimeText.text = "x" + Math.Floor(_actualTimeScale * 10) / 10;
            _pauseText.enabled = false;
            _scaleTimeText.enabled = true;
        }
    }

    //Startup iniziale, viene invocato l'evento SimulationStarted, utile per moduli che devono attivarsi 
    //solo in quel momento, come i Source Providers.

    void StartScene()
    {
        _started = true;
        Time.timeScale = 1.0f;

        SimulationStarted?.Invoke();
    }

    //Restart della scena

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

    //Uscita dalla scena

    void QuitScene()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
