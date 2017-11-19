using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zivot : MonoBehaviour {

    public static int brZivota = 3;
    private Text txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        postavi();
	}


    public void BrojiloZivota(int zivot)
    {
        brZivota -= zivot;
        txt.text = "Life: " +brZivota.ToString();
    }

    public static void postavi()
    {
        brZivota = 3;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
