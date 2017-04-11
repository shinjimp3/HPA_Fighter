using UnityEngine;
using System.Collections;

public class Biwako : MonoBehaviour {
	Renderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
		void Update() {
			
		}

	void Wave(){
		Vector2 offset = renderer.sharedMaterial.mainTextureOffset;
		offset.x = Mathf.Sin(Time.time * 1.8f) * 0.01f + Mathf.Sin(Time.time * 0.5f) * 0.03f;
		offset.y = Mathf.Sin(Time.time * 2.0f) * 0.01f + Mathf.Sin(Time.time * 0.4f) * 0.04f;
		renderer.sharedMaterial.mainTextureOffset = offset;
		renderer.sharedMaterial.SetTextureOffset("_BumpMap", offset);
	}

	void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player"){
			if(Vector3.Angle(Vector3.up,collision.transform.forward) > 80.0f){
				collision.transform.Rotate(collision.transform.right*85.0f,Space.World);
				collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			}
		}
	}
}
