using UnityEngine;
using System.Collections;

public class Data_holder : MonoBehaviour {
	public static string port_name = "Port Name";
	public static bool testplay = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//port name
		Rect rect1 = new Rect(10, 10, 100, 20);
		port_name = GUI.TextField(rect1, port_name, 20);

//		//test play
//		if(GUI.Button(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) +170,100,50),
//		              "Test")){
//			testplay = true;
//			Application.LoadLevel(1);
//		}
	}
}
