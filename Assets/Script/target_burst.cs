using UnityEngine;
using System.Collections;

public class target_burst : MonoBehaviour {
	public GameObject explosion;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "bullet"){
			Destroy(gameObject,0.1f);
			Instantiate(explosion,transform.position,transform.rotation);
		}
	}
}
