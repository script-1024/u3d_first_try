using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallOut : MonoBehaviour
{
    int times = 0;
    float sec = 0.0f;
    float secLatest = 0.0f;
    public Transform target;

    void Start() { }

    void Update()
    {
        isFallOut();
    }

    void isFallOut()
    {
        string str = ""+target;
        str = str.Replace(" (UnityEngine.Transform)","");

        if ( (times > 2 && transform.position.y <= -5) || target.position.x > 1e+9 || target.position.y > 30 || target.position.z > 1e+9)
        {
            Debug.Log("實體 \"" + str + "\" 位於無效位置，已經重置。坐標: " + target.position);
            target.position = new Vector3(0.0f, 2.0f, -8.0f);
            CarMove.resetSpeed = true;
            times = 0;
        }
        else if (times <= 2 && transform.position.y <= -5 )
        {
            Debug.Log("實體 \"" + str + "\" 掉入虛空。坐標: " + target.position);
            target.position = new Vector3(0.0f, 2.0f, target.position.z);
            times += 1;
            sec = Time.time;
        }

        if (target.position.y >= 0.21)
        {
            target.Rotate(0 - target.eulerAngles.x, 0, 0);
        }

        secLatest = Time.time;
        if ( (secLatest - sec) > 2.0f) { times = 0; }
    }
}
