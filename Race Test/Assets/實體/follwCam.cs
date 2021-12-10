using UnityEngine;
using System.Collections;

public class follwCam : MonoBehaviour
{
	public Transform targetTr;  //要追蹤的遊戲物件
	public float dist = 3f;  //與攝像機之間的距離
	public float height = 2f;  //設定攝像機的高度
	public float dampTrace = 20.0f;  //實現平滑追蹤的變數

	public Transform tr;
	// Use this for initialization
	void Start()
	{
		tr = GetComponent<Transform>();
	}

	// Update is called once per frame
	void LateUpdate()
	{
		tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
		tr.LookAt(targetTr.position);
	}
}