using UnityEngine;
using System.Collections;

public class Enemy_controller : MonoBehaviour {

	GameObject fire;
	HPA_controller hpa_controller;
	public Transform target;
	Vector3 direction;
	Vector3 player_direction;
	Vector3 target_direction;
	public Texture cursor;
	public bool visible = false;
	float left_timer = 0.0f;
	float missile_timer = 0.0f;



	// Use this for initialization
	void Start () {
		hpa_controller = gameObject.GetComponent<HPA_controller>();
		hpa_controller.inputs.cadence = 90.0f;
		transform.GetComponent<Rigidbody>().velocity = transform.up*-90.0f;
		target = GameObject.Find("Player_fighter").transform;
	}

	// Update is called once per frame
	void Update () {
		control();
	}

	void control(){
		direction = this.transform.up*-1;
		target_direction = (target.position - this.transform.position).normalized;
		if(Vector3.Angle(direction,target_direction) < 5.0f){
			if(Vector3.Distance(target.position,this.transform.position)<500.0f){
				missile_timer += Time.deltaTime;
				if(missile_timer < 1.5f){
					//
				}else if(missile_timer < 3.0f){
					hpa_controller.Shoot();
				}else{
					hpa_controller.missile_target = target.gameObject;
					hpa_controller.Shoot_missile();
					missile_timer = 0.0f;
				}
			}
		}else if(Vector3.Angle(direction,target_direction) < 45.0f){
			transform.rotation = Quaternion.FromToRotation(direction,direction + target_direction*0.8f*Time.deltaTime) * transform.rotation;
		}else{
			left_timer += Time.deltaTime;
			if(left_timer < 30.0f){
				transform.rotation = Quaternion.AngleAxis(10.0f*Time.deltaTime,Vector3.up) * transform.rotation;
			}else{
				transform.rotation = Quaternion.AngleAxis(-10.0f*Time.deltaTime,Vector3.up) * transform.rotation;
				if(left_timer > 60.0f){
					left_timer = 0.0f;
				}
			}
		}


	//	if(Mathf.Abs(target.position.y - transform.position.y) < 3.0f){
	//		hpa_controller.inputs.elevator = 0.05f * (transform.position.y - target.transform.position.y);
	//	}else{
			hpa_controller.inputs.elevator = 0.0f;
		//}
		if(Vector3.Angle(transform.up*-1,Vector3.down)<10.0f){
			hpa_controller.inputs.elevator = -1.0f;
		}
		
		if(transform.forward.y < -0.5f){
			hpa_controller.inputs.rudder = 1.0f;
		}else{
			hpa_controller.inputs.rudder = 0.0f;
		}

		player_direction = target.transform.up*-1;
		if(Vector3.Angle(player_direction,target_direction*-1)< 5.0f){
			left_timer += Time.deltaTime;
			if(left_timer < 10.0f){
				hpa_controller.inputs.rudder = 1.0f;
			}else{
				hpa_controller.inputs.rudder = -1.0f;
				if(left_timer > 20.0f){
					left_timer = 0.0f;
				}
			}
		}
		
		if(transform.position.y < 20.0f){
			hpa_controller.inputs.cadence = 200.0f;
		}else{
			hpa_controller.inputs.cadence = 85.0f;
		}
	}
	
	void OnWillRenderObject(){
		if(Camera.current.name != "SceneCamera"  && Camera.current.name != "Preview Camera" && Camera.current.name != "Back_camera"){
			visible = true;
		}
		if(Camera.current.name != "Main Camera"){
			visible = false;
		}
	}

	void OnGUI(){
		if(visible){
			Vector3 point = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position);
			GUI.DrawTexture(new Rect(point.x-15,Screen.height - point.y-15,30,30),cursor);
		}
	}
}
