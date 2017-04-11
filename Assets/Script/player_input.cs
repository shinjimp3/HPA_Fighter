using UnityEngine;
using System.Collections;

public class player_input : MonoBehaviour {
	HPA_controller hpa_controller;
	SerialHandler serial_handler;
	float interval = 1000.0f;
	//commit test in Windows 2017/04/11 23:57
	bool weaponIsMissile = false;
	GameObject missile_target;
	public Texture cursor;
	bool button1 = false;	bool button1_buf = false;
	bool button2 = false;	bool button2_buf = false;

	// Use this for initialization
	void Start () {
		hpa_controller = gameObject.GetComponent<HPA_controller>();
		serial_handler = gameObject.GetComponent<SerialHandler>();

		serial_handler.onDataRecieved += onDataReceived;
	}

	// Update is called once per frame
	void Update () {
		//controll
		hpa_controller.inputs.elevator = (Input.mousePosition.y - Screen.height/2.0f)*2.0f/Screen.height;
		hpa_controller.inputs.rudder = (Input.mousePosition.x - Screen.width/2.0f)*2.0f/Screen.width;

		//
		interval += Time.deltaTime;
		if(interval >= 2.0f){
			hpa_controller.inputs.cadence = 60.0f/interval;
		}
		if(Input.GetKeyDown(KeyCode.A)){
			hpa_controller.inputs.cadence = 20.0f / interval;//60.0f/interval;
			interval = 0.0001f;
		}

		if(weaponIsMissile){
			if(Input.GetMouseButton(0)){
				hpa_controller.Lockon_missile();
			}else if(Input.GetMouseButtonUp(0)){
				hpa_controller.Shoot_missile();
			}
		}else{
			if(Input.GetMouseButton(0)){
				hpa_controller.Shoot();
			}
		}

		if(Input.GetMouseButtonDown(1)){
			weaponIsMissile = !weaponIsMissile;
		}

	}


	void onDataReceived(string message)
	{
		try {
			hpa_controller.inputs.cadence = float.Parse(message);
			interval = 0.0001f;
			Debug.Log(message);
		} catch (System.Exception e) {
			Debug.LogWarning(e.Message);
		}
	}



	void OnGUI(){
		GUIStyle style = new GUIStyle();
		GUIStyleState state = new GUIStyleState();
		style.fontSize = 50;
		state.textColor = Color.black;
		style.fontStyle = FontStyle.Bold;
		style.normal = state;
		GUI.Label(new Rect(20, 400, 200, 50), "Cadence: " + Mathf.FloorToInt(hpa_controller.inputs.cadence)+ " rpm",style);
		style.fontSize = 30;
		GUI.Label(new Rect(20, 470, 200, 50), "Velocity: " + Mathf.FloorToInt(GetComponent<Rigidbody>().velocity.magnitude) + " m/s",style);
		GUI.Label(new Rect(20, 500, 200, 50), "Height: " + Mathf.FloorToInt(transform.position.y) + " m",style);
		string weapon = (weaponIsMissile) ? "Missile" : "Autocannon";
		GUI.Label(new Rect(Screen.width-300, 450, 200, 50), "Weapon"+"\n"+weapon,style);


	}
}
