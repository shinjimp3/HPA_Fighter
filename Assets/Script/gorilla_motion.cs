using UnityEngine;
using System.Collections;

public class gorilla_motion : MonoBehaviour {
	HPA_controller hpa_controller;
	public GameObject right_arm;
	public GameObject left_arm;
	public GameObject right_leg;
	public GameObject left_leg;
	Quaternion initial_right_arm;
	Quaternion initial_left_arm;
	Quaternion initial_right_leg;
	Quaternion initial_left_leg;

	// Use this for initialization
	void Start () {
		hpa_controller = GetComponentInParent<HPA_controller>();
//		right_arm = transform.FindChild("Bip01 R Clavicle").gameObject;
//		left_arm = transform.FindChild("Bip01 L Clavicle").gameObject;
//		right_leg = transform.FindChild("Bip01 R Thigh").gameObject;
//		left_leg = transform.FindChild("Bip01 L Thigh").gameObject;
		initial_right_arm = right_arm.transform.localRotation;
		initial_left_arm = left_arm.transform.localRotation;
		initial_right_leg = right_leg.transform.localRotation;
		initial_left_leg = left_leg.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		float elevator = hpa_controller.inputs.elevator;
		float rudder = hpa_controller.inputs.rudder;
		right_arm.transform.localRotation = Quaternion.AngleAxis((-elevator + rudder)*25.0f,Vector3.forward)*initial_right_arm;
		left_arm.transform.localRotation = Quaternion.AngleAxis((-elevator - rudder)*25.0f,Vector3.forward)*initial_left_arm;
		right_leg.transform.localRotation = Quaternion.AngleAxis((elevator + rudder)*25.0f,Vector3.forward)*initial_right_leg;
		left_leg.transform.localRotation = Quaternion.AngleAxis((elevator - rudder)*25.0f,Vector3.forward)*initial_left_leg;
	}
}
