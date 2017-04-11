using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {
	public GameObject splash;
	GameObject[] fighter_set;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		fighter_set = GameObject.FindGameObjectsWithTag("Player");
		for(int i=0; i<fighter_set.Length; i++){
			if(fighter_set[i].transform.position.y < 20.0f){
				GameObject _splash = Instantiate(splash,
				                                 new Vector3(fighter_set[i].transform.position.x,1.0f,fighter_set[i].transform.position.z),
					  					         transform.rotation) as GameObject;
				Destroy(_splash.gameObject,3.0f);
			}
		}
		fighter_set = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i=0; i<fighter_set.Length; i++){
			if(fighter_set[i].transform.position.y < 20.0f){
				GameObject _splash = Instantiate(splash,
				                                 new Vector3(fighter_set[i].transform.position.x,1.0f,fighter_set[i].transform.position.z),
				                                 transform.rotation) as GameObject;
				Destroy(_splash.gameObject,3.0f);
			}
		}
		fighter_set = GameObject.FindGameObjectsWithTag("missile");
		for(int i=0; i<fighter_set.Length; i++){
			if(fighter_set[i].transform.position.y < 20.0f){
				GameObject _splash = Instantiate(splash,
				                                 new Vector3(fighter_set[i].transform.position.x,1.0f,fighter_set[i].transform.position.z),
				                                 transform.rotation) as GameObject;
				Destroy(_splash.gameObject,3.0f);
			}
		}
	}
}
