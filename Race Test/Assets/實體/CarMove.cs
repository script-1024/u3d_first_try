using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CarMove : MonoBehaviour
{
	public static float Speed = 0.0f;  //速度
	public float Acceleration = 2.0f;  //加速度 
	public float Friction = 0.02f;  //速度衰減

    void Start() { }
	
	void Update()
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
		else if (!Input.GetKey("w") && !Input.GetKey("s"))
		{
			if (Speed > 1) { Speed -= Friction; }
			if (Speed < 1 && Speed >-1) { Speed = 0.0f; }
			if (Speed < -1) { Speed += Friction; }
		}
		transform.Translate(0, 0, (float)Math.Round((Time.deltaTime*Speed/10), 2));

		//旋轉
		if (Input.GetKey("a"))
		{
			transform.Rotate(0, -Time.deltaTime * (Speed/2), 0);
		}
		else if (Input.GetKey("d"))
		{
			transform.Rotate(0, Time.deltaTime * (Speed/2), 0);
		}
	}
}