using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public GameObject target;
	float speed = 100.0f;
	Vector3 direction;
	Vector3 target_direction;
	float timer = 0.0f;
	public bool launch = false;
	public Texture cursor;
	public bool gorilla_missile = false;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		Launch();
		Speed_change();
		//Camera_control();
	}

	void Launch(){

		direction = transform.forward;
		transform.position += transform.forward*speed*Time.deltaTime;
		if(target == null) return;
		target_direction = (target.transform.position - this.transform.position).normalized;
		if(Vector3.Angle(direction,target_direction) > 15.0f && !gorilla_missile){
			target = null;
			return;
		}
		transform.rotation = Quaternion.FromToRotation(direction,direction + target_direction*3.0f*Time.deltaTime) * transform.rotation;

		timer += Time.deltaTime;
		if(timer > 12.0f){
			if(transform.FindChild("Smoke_source") != null){
				GameObject smoke = transform.FindChild("Smoke_source").transform.gameObject;
				transform.FindChild("Smoke_source").transform.parent = null;
				Destroy(smoke,10.0f);
				Destroy(gameObject,0.1f);
			}
			
		}
	}

	void Speed_change(){
		if(target == null) return;
		if(target.transform.tag == "Player"){

			speed = 140.0f - target.GetComponent<HPA_controller>().inputs.cadence
				+ Vector3.Distance(target.transform.position,transform.position);
		}
	}
	

	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "Enemy"){
			if(transform.FindChild("Smoke_source") != null){
			GameObject smoke = transform.FindChild("Smoke_source").transform.gameObject;
			transform.FindChild("Smoke_source").transform.parent = null;
			Destroy(smoke,10.0f);
			Destroy(gameObject,0.1f);
			}
		}
	}

	void OnGUI(){
		if(target==null) return;
		if(target.transform.tag == "Enemy"){
			Enemy_controller enemy_controller = target.GetComponent<Enemy_controller>();
			if(enemy_controller != null){
				if(target.GetComponent<Enemy_controller>().visible){
					Vector3 point = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(target.transform.position);
					GUI.DrawTexture(new Rect(point.x-15,Screen.height - point.y-15,30,30),cursor);
				}
			}
		}		
	}
}
