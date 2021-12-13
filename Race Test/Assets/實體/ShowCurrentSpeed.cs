using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentSpeed : MonoBehaviour
{
    void Start() { }

    void Update()
    {
        GetComponent<Text>().text = "當前速度："+ Math.Round(CarMove.Speed, 1);
    }
}
