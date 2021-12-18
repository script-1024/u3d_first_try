using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ThisPlayer;
    public Transform Model;
    public Transform Camera;
    public Transform Head;
    public Transform Body;
    public Transform LeftArm;
    public Transform RightArm;
    public Transform LeftLeg;
    public Transform RightLeg;

    public static float Speed = 0f;
    public static float Health = 20f;
    public static float Armor = 0f;
    public static Vector3 Pos;
    public static bool Pause = false;

    void Start()
    {
        ThisPlayer = GetComponentInParent<Transform>().parent.transform;
    }

    void Update()
    {
        Pos = Model.transform.position;
        PlayerController();
        CameraFollow();
    }

    void RotateRect()
    {
        
    }

    void PlayerController()
    {
        if (Input.GetKey("mouse 2")) {Speed = 5.6f;}
        else {Speed = 4.2f;}
        
        if (Input.GetKey("w"))
        {
            RotateRect();
            Model.transform.Translate(0, 0, Speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            RotateRect();
            Model.transform.Translate(0, 0, -Speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            RotateRect();
            Model.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            RotateRect();
            Model.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
        Camera.transform.position = new Vector3(Pos.x, Pos.y + 1.75f, Pos.z);
    }

    float _mouseX = 0f;
    float _mouseY = 0f;
    float _rotationOnX = 0f;
    float _rotationOnY = 0f;
    float _cameraSpeed = 100f;
    bool _curLock = false;

    void CameraFollow()
    {
        
        
        if (Input.GetKey("left alt"))
        {
            _curLock = false;
        }
        else if (Input.GetKeyUp("escape"))
        {
            Game.Pause = !Game.Pause;
            _curLock = false;
        }
        else if (!Input.GetKey("left alt") && !Game.Pause) { _curLock = true; }

        if (_curLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (_curLock)
        {
            _mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _cameraSpeed * 6;
            _mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _cameraSpeed * 6;

            _rotationOnX -= _mouseY;
            _rotationOnX = Mathf.Clamp(_rotationOnX, -90, 90);

            _rotationOnY += _mouseX;
            if (_rotationOnY < -45)
            {
                _rotationOnY = -45;
                Model.transform.Rotate(0, -2, 0);
                Camera.transform.Rotate(0, -2, 0);
                
            }
            else if (_rotationOnY > 45)
            {
                _rotationOnY = 45;
                Model.transform.Rotate(0, 2, 0);
                Camera.transform.Rotate(0, 2, 0);
                
            }
            
            Camera.transform.localRotation = Quaternion.Euler(_rotationOnX, _rotationOnY, 0f);
            
            Head.transform.localRotation = Quaternion.Euler(-_rotationOnX, _rotationOnY, 0f);
        }
    }
}
