using UnityEngine;
using System.Collections;

public class Game_controller : MonoBehaviour {
	HPA_controller hpa_controller;
	public GameObject player;
	float game_over_timer = 0.0f;
	public int enemy_shot_down = 0;

	// Use this for initialization
	void Start () {
		hpa_controller = player.GetComponent<HPA_controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hpa_controller.dead){
			game_over_timer += Time.deltaTime;
			if(game_over_timer > 25.0f){
				Application.LoadLevel(0);
			}
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel(0);
		}
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle();
		GUIStyleState state = new GUIStyleState();
		style.fontSize = 20;
		state.textColor = Color.black;
		style.fontStyle = FontStyle.Normal;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal = state;
		int timer = Mathf.FloorToInt(Time.timeSinceLevelLoad);
		string time_count = Mathf.FloorToInt(timer/3600).ToString("d2")+":"+(timer%3600/60).ToString("d2")+":"+(timer%60).ToString("d2");
		GUI.Label(new Rect(Screen.width/2-200,10, 100, 20),time_count,style);
		GUI.Label(new Rect(Screen.width/2+50,10, 100, 20),"Enemies Shot Down: "+enemy_shot_down.ToString("d3"),style);

		if(!hpa_controller.dead) return;

		style.fontSize = 50;
		state.textColor = Color.red;
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal = state;
		GUI.Label(new Rect(Screen.width/2-200,Screen.height/2-50, 400, 100),"GAME OVER",style);

	}
}
