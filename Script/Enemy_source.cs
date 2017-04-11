using UnityEngine;
using System.Collections;

public class Enemy_source : MonoBehaviour {
	public GameObject enemy;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectsWithTag("Enemy").Length < 20){
			timer += Time.deltaTime;
			if(timer > 30.0f ){
				Instantiate(enemy,transform.position+transform.up * 400.0f,transform.rotation);
				timer = 0.0f;
			}
		}
	}
}
