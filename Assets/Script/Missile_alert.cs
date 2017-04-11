using UnityEngine;
using System.Collections;

public class Missile_alert : MonoBehaviour {
	GameObject[] missile_set;
	public float min_distance;
	public Texture caution;
	AudioSource audio_source;
	public AudioClip alert_clip;
	float alert_timer = 0.0f;
	bool sound = true;

	// Use this for initialization
	void Start () {
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		min_distance = Mathf.Infinity;
		missile_set = GameObject.FindGameObjectsWithTag("missile");
		for(int i=0; i<missile_set.Length; i++){
			if(missile_set[i].GetComponent<Missile>().target!=null){
				if(missile_set[i].GetComponent<Missile>().target.tag == "Player"){
					min_distance = Mathf.Min(Vector3.Distance(missile_set[i].transform.position,transform.position),min_distance);
				}
			}
		}
		Alert_sound();
	}

	void Alert_sound(){
		if(min_distance > 500.0f){
			alert_timer = 0.0f;
			audio_source.Stop();
			return;
		}

		alert_timer += Time.deltaTime;
		if(alert_timer > 0.1f + min_distance*0.005f){
			if(audio_source.isPlaying){
				audio_source.Stop();
				alert_timer = 0.0f;
			}else{
				audio_source.clip = alert_clip;
				audio_source.Play();
				alert_timer = 0.0f;
			}
		}
	}

	void OnGUI(){
		if(min_distance > 500.0f) return;
		GUI.DrawTexture(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) - 70,80,80),caution);
		GUIStyle style = new GUIStyle();
		style.fontSize = 50;
		style.alignment = TextAnchor.MiddleCenter;
		GUIStyleState state = new GUIStyleState();
		state.textColor = new Color(255,0,0);
		style.normal = state;
		GUI.Label(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) +100,100,100),
		          "Missile Alert",style);
		style.fontSize = 30;
		GUI.Label(new Rect(Mathf.FloorToInt(Screen.width/2) - 50,Mathf.FloorToInt(Screen.height/2) +150,100,100),
		          Mathf.FloorToInt(min_distance) + " m",style);
	}
}
