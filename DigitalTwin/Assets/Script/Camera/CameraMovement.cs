using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 200f;
    private bool _isOnTopView = false;
    private bool _isTPressed = false;
    void Update()
    {
        //GROUNDED CAMERA BRANCH
        if (!_isOnTopView)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(-1f, 0f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(1f, 0f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 0f, 1f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 0f, -1f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            if (Input.GetKey(KeyCode.R))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 1f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                UnityEngine.Vector3 downMovement = new UnityEngine.Vector3(0f, -1f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(downMovement);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(UnityEngine.Vector3.up, -_rotationSpeed * Time.unscaledDeltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(UnityEngine.Vector3.up, _rotationSpeed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.T))
            {
                if (!_isTPressed)
                {
                    _isTPressed = true;

                    _moveSpeed *= 2f;

                    transform.Rotate(90f, 0f, 0f);
                    transform.position += UnityEngine.Vector3.up * 9.0f;

                    _isOnTopView = !_isOnTopView;

                }
            }
        }
        //FLYING CAMERA BRANCH
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(-1f, 0f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(1f, 0f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 1f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, -1f, 0f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            if (Input.GetKey(KeyCode.R))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 0f, -1f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                UnityEngine.Vector3 downMovement = new UnityEngine.Vector3(0f, 0f, 1f) * _moveSpeed * Time.unscaledDeltaTime;
                transform.Translate(downMovement);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(UnityEngine.Vector3.forward, _rotationSpeed * Time.unscaledDeltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(UnityEngine.Vector3.forward, -_rotationSpeed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.T))
            {
                if (!_isTPressed)
                {
                    _isTPressed = true;

                    _moveSpeed /= 2f;

                    transform.Rotate(-90f, 0f, 0f);
                    transform.position = new UnityEngine.Vector3(transform.position.x, 1.8f, transform.position.z);

                    _isOnTopView = !_isOnTopView;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            _isTPressed = false;
        }
    }
}
