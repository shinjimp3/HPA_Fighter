using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public Texture2D title;
	public static string port_name = "Port Name";

	void Update(){

	}

	void OnGUI(){

		//title
		GUI.DrawTexture(new Rect(0.0f,0.0f,Screen.width,Screen.height),title);
		GUIStyle style = new GUIStyle();
		style.fontSize = 120;
		style.alignment = TextAnchor.MiddleCenter;
		GUIStyleState state = new GUIStyleState();
		state.textColor = new Color(1.0f,0.5f,0);
		style.normal = state;
		GUI.Label(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) -100,100,100),
		          "HPA Fighter",style);

		//start button
		if(GUI.Button(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) +100,100,50),
		              "Start")){
			Application.LoadLevel(1);
		}



	}
}
