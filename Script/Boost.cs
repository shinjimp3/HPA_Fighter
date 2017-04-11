using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {
	HPA_controller hpa_controller;
	float keep_cadence_time = 0.0f;
	float boost_time = 2.0f;
	public bool boosting = false;
	public AudioClip boost_clip;
	public GameObject ring;
	public Texture bar_meter;
	public Texture bar_meter_back;
	AudioSource audio_source;

	// Use this for initialization
	void Start () {
		hpa_controller = GetComponent<HPA_controller>();
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hpa_controller.inputs.cadence >= 90.0f){
			keep_cadence_time += Time.deltaTime;
		}else{
			keep_cadence_time = Mathf.Max(0.0f,keep_cadence_time - Time.deltaTime*2.0f);
		}

		if(keep_cadence_time >= 5.0f && !boosting){
			audio_source.PlayOneShot(boost_clip);
			GetComponent<Rigidbody>().AddForce(transform.up*-100.0f,ForceMode.Impulse);
			hpa_controller.parameter1 = 0.04f;
			keep_cadence_time = 0.0f;
			boosting = true;
			boost_time = 2.0f;
			GameObject _ring = Instantiate(ring,transform.FindChild("p47 prop01").transform.position+transform.up*-10.0f,Quaternion.AngleAxis(90.0f,transform.right)*transform.rotation) as GameObject;
			Destroy(_ring,2.0f);
		}
		if(boosting){
			Boosting();
		}
	}

	void Boosting(){
		GetComponent<Rigidbody>().AddForce(transform.up*-1000.0f*Time.deltaTime);
		boost_time -= Time.deltaTime;
		if(boost_time < 0.0f){
			boosting = false;
			hpa_controller.parameter1 = 0.12f;
		}
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(20,450,256,8),bar_meter_back);
		if(boosting){
			GUI.DrawTexture(new Rect(20,450,boost_time*128,8),bar_meter);
		}else{
			GUI.DrawTexture(new Rect(20,450,keep_cadence_time*51,8),bar_meter);
		}
	}
}
