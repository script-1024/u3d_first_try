using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMove : MonoBehaviour
{
	public float Acceleration = 2.0f;  //加速度 
	public float Speed = 0.0f;  //速度
	public float Friction = 0.02f;  //速度衰減
	float Rotate = 100.0f;  //旋轉速度

	public static bool resetSpeed = false;

    void Start() { }

	void Update()
	{
		if (resetSpeed)
        {
			Speed = 0.0f;
			resetSpeed = false;
		}
		PlayerControl();
	}
	
	void PlayerControl()
	{
		//移動（加速度公式：V = V0 + at）
		if (Input.GetKey("w"))
		{
			if (Speed < 300) { Speed += Acceleration * Time.deltaTime * 10; }
		}
		else if (Input.GetKey("s"))
		{
			if (Speed > -90) { Speed -= Acceleration * Time.deltaTime * 10; }
		}
		else if (!Input.anyKey)
		{
			if (Speed > 1) { Speed -= Friction; }
			if (Speed < 1 && Speed >-1) { Speed = 0.0f; }
			if (Speed < -1) { Speed += Friction; }
		}
		transform.Translate(0, 0, Time.deltaTime * Speed / 10);

		//旋轉
		if (Input.GetKey("a"))
		{
			transform.Rotate(0, -Time.deltaTime * Rotate, 0);
		}
		else if (Input.GetKey("d"))
		{
			transform.Rotate(0, Time.deltaTime * Rotate, 0);
		}
	}
}