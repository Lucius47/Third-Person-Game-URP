using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenColorChanger : MonoBehaviour
{


    public void Operate()
	{
        Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<Renderer>().material.color = randomColor;
    }
    

    
}
