using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ThisPlayer;
    public Transform Model;
    public Transform Lower;
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

    void PlayerController()
    {
        if (Input.GetKey("mouse 2")) {Speed = 5.6f;}
        else {Speed = 4.2f;}
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey("a") && Input.GetKey("d") || Input.GetKey("left") && Input.GetKey("right")) { h=0; }
        if (Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("up") && Input.GetKey("down")) { v=0; }
        if (h!=0 || v!=0)
        {
            Vector3 direction = new Vector3(h, 0, v);
            float y = Head.transform.rotation.eulerAngles.y;
            direction = Quaternion.Euler(0, y, 0) * direction;
            Model.transform.Translate(-direction * Time.deltaTime * Speed);
        }
    }

    float mouseX = 0f;
    float mouseY = 0f;
    float rotationOnX = 0f;
    float rotationOnY = 0f;
    float cameraSpeed = 100f;
    bool curLock = false;

    void CameraFollow()
    {
        
        
        if (Input.GetKey("left alt"))
        {
            curLock = false;
        }
        else if (Input.GetKeyUp("escape"))
        {
            Game.Pause = !Game.Pause;
            curLock = false;
        }
        else if (!Input.GetKey("left alt") && !Game.Pause) { curLock = true; }

        if (curLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (curLock)
        {
            mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * cameraSpeed * 6;
            mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSpeed * 6;

            rotationOnX -= mouseY;
            rotationOnX = Mathf.Clamp(rotationOnX, -90, 90);

            rotationOnY += mouseX;
            
            Head.transform.localRotation = Quaternion.Euler(-rotationOnX, rotationOnY, 0f);
            Lower.transform.rotation = Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0);
        }
    }
}
