using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform model;
    public Transform camera;
    public Transform head;
    public Transform body;
    public Transform leftArm;
    public Transform rightArm;
    public Transform leftLeg;
    public Transform rightLeg;

    public static float Speed = 0.0f;
    public static float Health = 20.0f;
    public static float Armor = 0.0f;

    void Start() { }

    void Update()
    {
        PlayerController();
        CameraFollow();
    }

    void PlayerController()
    {
        if (Input.GetKey("mouse 2")) {Speed = 5.612f;}
        else {Speed = 4.317f;}
        
        if (Input.GetKey("w"))
        {
            model.transform.Translate(0, 0, Speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            model.transform.Translate(0, 0, -Speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            model.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            model.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
    }

    void CameraFollow()
    {

    }
}
