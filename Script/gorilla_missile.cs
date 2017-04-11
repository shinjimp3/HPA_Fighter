using UnityEngine;
using System.Collections;

public class gorilla_missile : MonoBehaviour {
	public GameObject player_fighter;
	public GameObject missile;
	Transform missile_pod;
	bool launching = false;
	float timer = 0.0f;
	const float start_time = 15.0f;
	const float interval_time = 0.20f;
	int missile_count = 0;

	// Use this for initialization
	void Start () {
		missile_pod = transform.FindChild("Missile_pod").transform;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(Vector3.Distance(player_fighter.transform.position,this.transform.position) < 1000.0f && timer > start_time){
			launching = true;
		}

		if(launching){
			launch_missile();
		}
	}

	void launch_missile(){
		if(missile_count > 10){
			timer = 0.0f;
			missile_count = 0;
			launching = false;
			return;
		}

		if((timer - start_time) > interval_time){
			GameObject _missile = Instantiate(missile,
			                                  missile_pod.position + transform.right*(missile_count*10.0f - 50.0f),
			                                  transform.rotation) as GameObject;
			_missile.GetComponent<Missile>().target = player_fighter;
			_missile.GetComponent<Missile>().gorilla_missile = true;
			missile_count++;
			timer = start_time;
		}
	}
}
