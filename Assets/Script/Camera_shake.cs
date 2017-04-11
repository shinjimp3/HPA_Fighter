using UnityEngine;
using System.Collections;

public class Camera_shake : MonoBehaviour {
	public GameObject player_fighter;
	HPA_controller hpa_controller;
	Missile_alert missile_alert;
	Vector3 first_position;
	Vector3 move_position = Vector3.zero;
	float shake_interval;
	float time = 0.0f;
	Rigidbody rigidbody;
	float speed = 0.0f;

	// Use this for initialization
	void Start () {
		hpa_controller = player_fighter.GetComponent<HPA_controller>();
		rigidbody = player_fighter.GetComponent<Rigidbody>();
		missile_alert = player_fighter.transform.FindChild("Missile_alert").GetComponent<Missile_alert>();//GetComponentInChildren<Missile_alert>();
		first_position = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		speed = rigidbody.velocity.magnitude;
		//print (missile_alert.min_distance);
		if(missile_alert.min_distance < 50.0f){
			move_position = (move_position + Time.deltaTime *missile_alert.min_distance*(0.95f*Vector3.up + 0.17f*Vector3.forward))/(1.0f + Time.deltaTime);
		}else{
			move_position = (move_position + Time.deltaTime * Vector3.zero)/(1.0f + Time.deltaTime);
		}
		if(speed > 70.0f){
			Shake();
		}else{
			transform.localPosition = first_position + move_position;
			time = 0.0f;
		}
	}

	void Shake(){
		time += Time.deltaTime;
		float power = (speed - 70.0f)*0.001f;
		Vector3 shake = new Vector3(Mathf.Sin(2.0f*Mathf.PI*10.0f*time),Mathf.Sin(2.0f*Mathf.PI*11.0f*time),Mathf.Sin(2.0f*Mathf.PI*9.0f*time));
		shake *= power;
		transform.localPosition = first_position + move_position + shake;
	}
}
