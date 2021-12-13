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
/*
    float distToGround = 0.0f;
    Vector3 dwn = transform.TransformDirection(Vector3.down);
    bool OnGround()
    {
        return Physics.Raycast(transform.position, dwn, 0.1f);
    }

    void Update() { Debug.Log(OnGround()); }
*/
    void LateUpdate()
    {
        if ( (times > 2 && transform.position.y <= -5) || transform.position.y >= 30)
        {
            Debug.Log("實體 \"" + this.gameObject.name + "\" 位於無效位置，已經重置。坐標: " + target.position);
            target.position = new Vector3(0, 2, -8);
            target.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            CarMove.Speed = 0.0f;
            times = 0;
        }
        else if (times <= 2 && transform.position.y <= -5 )
        {
            Debug.Log("實體 \"" + this.gameObject.name + "\" 掉入虛空。坐標: " + target.position);
            target.position = new Vector3(0, 2, target.position.z);
            target.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            times += 1;
            sec = Time.time;
        }

        secLatest = Time.time;
        if ( (secLatest - sec) > 2.0f) { times = 0; }
    }
}
