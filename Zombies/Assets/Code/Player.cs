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

    public static float Velocity = 0f;
    public static float Health = 20f;
    public static float Armor = 0f;
    public static Vector3 Pos;
    public static bool Pause = false;

    void Start()
    {
        ThisPlayer = GetComponentInParent<Transform>().parent.transform;
    }

    float sight;
    void Update()
    {
        Pos = Model.transform.position;
        PlayerController();
        CameraFollow();
        sight = AngleConvert( Head.transform.localRotation.eulerAngles.y - Lower.transform.localRotation.eulerAngles.y );
    }
    
    void PlayerController()
    {
        if (Input.GetKey("mouse 2")) {Velocity = 5.6f;}
        else {Velocity = 4.2f;}
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey("a") && Input.GetKey("d") || Input.GetKey("left") && Input.GetKey("right")) { h=0; }
        if (Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("up") && Input.GetKey("down")) { v=0; }
        if (h!=0 || v!=0)
        {
            Vector3 direction = new Vector3(h, 0, v);
            float y = Head.transform.rotation.eulerAngles.y;
            direction = Quaternion.Euler(0, y, 0) * direction;
            Model.transform.Translate(-direction * Time.deltaTime * Velocity);
            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                Lower.transform.rotation = Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetKey("a")) Lower.transform.rotation = Quaternion.Euler(0,Head.transform.localRotation.eulerAngles.y+135,0);
            if (Input.GetKey("d")) Lower.transform.rotation = Quaternion.Euler(0,Head.transform.localRotation.eulerAngles.y-135,0);
            Action("walk");
        }

        if (!Input.anyKey)
        {
            Action("idle");
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
            Head.transform.localRotation = Quaternion.Euler(-rotationOnX, rotationOnY, 0);
            
            if (sight < -45)
            {
                Lower.transform.Rotate(0,-6,0);
            }
            else if (sight > 45)
            {
                Lower.transform.Rotate(0,6,0);
            }
        }
    }

    float AngleConvert(float _source)
    {
        //將角度限制在 -180 ~ 180 之間
        if (_source>180) _source -= 360;
        if (_source<-180) _source += 360;
        return _source;
    }

    float arm = 0.0f;
    float crrntArm;
    string armState = "";

    float leg = 0.0f;
    float crrntLeg;
    string legState = "";

    float rate = 0.0f;
    void Action(string _id)
    {
        crrntArm = AngleConvert(LeftArm.transform.rotation.eulerAngles.x);
        crrntLeg = AngleConvert(LeftLeg.transform.localRotation.eulerAngles.x);

        switch (_id)
        {
            case "idle":
                rate = 0.02f;
                arm = 5.0f;
                leg = 0.0f;
                break;
            
            case "walk":
                rate = 0.8f;
                arm = 45.0f;
                leg = 45.0f;
                break;

            default:
                rate = 0.0f;
                arm = 0.0f;
                leg = 0.0f;
                break;
        }

        if (armState == "")
        {
            armState = "upward";
            LeftArm.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
            RightArm.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        else if (crrntArm < arm && armState == "upward")
        {
            LeftArm.transform.Rotate(rate, 0, 0);
            RightArm.transform.Rotate(-rate, 0, 0);
        }
        else if (crrntArm <= -arm && armState == "down")
        {
            armState = "upward";
            LeftArm.transform.Rotate(rate, 0, 0);                
            RightArm.transform.Rotate(-rate, 0, 0);
        }
        else if (armState == "down" )
        {
            LeftArm.transform.Rotate(-rate, 0, 0);
            RightArm.transform.Rotate(rate, 0, 0);
        }
        else if (crrntArm >= arm && armState == "upward")
        {
            armState = "down";
            LeftArm.transform.Rotate(-rate, 0, 0);
            RightArm.transform.Rotate(rate, 0, 0);
        }

        if (legState == "")
        {
            legState = "upward";
            LeftLeg.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
            RightLeg.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        else if (crrntLeg < leg && legState == "upward")
        {
            LeftLeg.transform.Rotate(rate, 0, 0);
            RightLeg.transform.Rotate(-rate, 0, 0);
        }
        else if (crrntLeg <= -leg && legState == "down")
        {
            legState = "upward";
            LeftLeg.transform.Rotate(rate, 0, 0);                
            RightLeg.transform.Rotate(-rate, 0, 0);
        }
        else if (legState == "down" )
        {
            LeftLeg.transform.Rotate(-rate, 0, 0);
            RightLeg.transform.Rotate(rate, 0, 0);
        }
        else if (crrntLeg >= leg && legState == "upward")
        {
            legState = "down";
            LeftLeg.transform.Rotate(-rate, 0, 0);
            RightLeg.transform.Rotate(rate, 0, 0);
        }
    }
}
