using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CamFollow : MonoBehaviour
{
	public Transform target;  //要追蹤的遊戲物件
	public float dist = 3f;  //與攝像機之間的距離
	public float height = 2f;  //設定攝像機的高度
	public float dampTrace = 20.0f;  //實現平滑追蹤的變數
	bool lockMode = true;

	public Transform cam;
	void Start()
	{
		cam = GetComponent<Transform>();
	}
	
	void Update()
	{
		if (Input.GetKeyUp("r"))
		{
			lockMode = !lockMode;
			Debug.Log("Lock Mode Changed. ("+lockMode+")");
		}

		if (lockMode) { CamLock(); }
		else { CamUnlock(); }
	}

	void CamLock()
	{
		cam.position = Vector3.Lerp(cam.position, target.position - (target.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
		cam.LookAt(target.position);
	}

	float MouseX, MouseY;
	void CamUnlock()
	{
		MouseX = Input.GetAxis("Mouse X");
		MouseY = Input.GetAxis("Mouse Y");
		//cam.position = Vector3.Lerp(cam.position, target.position - (target.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
		//cam.LookAt(target.position);
		Debug.Log("X:"+MouseX+"/Y:"+MouseY);
	}
}