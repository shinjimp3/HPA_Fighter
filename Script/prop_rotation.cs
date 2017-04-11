using UnityEngine;
using System.Collections;

public class prop_rotation : MonoBehaviour {
	HPA_controller hpa_controller;
	Boost boost;
	AudioSource audio_source;
	float cadence = 0.0f;

	// Use this for initialization
	void Start () {
		hpa_controller = gameObject.GetComponentInParent<HPA_controller>();
		boost = gameObject.GetComponentInParent<Boost>();
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		cadence = hpa_controller.inputs.cadence;
		if(boost != null){
			if(boost.boosting){
				cadence += 150.0f;
			}
		}
		transform.Rotate(Vector3.up*cadence*6.0f*Time.deltaTime);
		audio_source.pitch = (audio_source.pitch + (0.6f + 0.4f*(cadence/90.0f))*Time.deltaTime)/(1.0f+Time.deltaTime);
	}
}
