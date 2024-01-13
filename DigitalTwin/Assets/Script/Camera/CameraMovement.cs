using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Parametri configurabili")]
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _rotationSpeed = 200f;
    private bool _isOnTopView = false;
    private bool _isTPressed = false;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //GROUNDED CAMERA BRANCH
        if (!_isOnTopView)
        {
            UnityEngine.Vector3 movement = new UnityEngine.Vector3(horizontal, 0f, vertical) * _moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            if (Input.GetKey(KeyCode.R))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 1f, 0f) * _moveSpeed * Time.deltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                UnityEngine.Vector3 downMovement = new UnityEngine.Vector3(0f, -1f, 0f) * _moveSpeed * Time.deltaTime;
                transform.Translate(downMovement);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(UnityEngine.Vector3.up, -_rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(UnityEngine.Vector3.up, _rotationSpeed * Time.deltaTime);
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
            UnityEngine.Vector3 movement = new UnityEngine.Vector3(horizontal, vertical, 0f) * _moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            if (Input.GetKey(KeyCode.R))
            {
                UnityEngine.Vector3 upMovement = new UnityEngine.Vector3(0f, 0f, -1f) * _moveSpeed * Time.deltaTime;
                transform.Translate(upMovement);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                UnityEngine.Vector3 downMovement = new UnityEngine.Vector3(0f, 0f, 1f) * _moveSpeed * Time.deltaTime;
                transform.Translate(downMovement);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(UnityEngine.Vector3.forward, _rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(UnityEngine.Vector3.forward, -_rotationSpeed * Time.deltaTime);
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
