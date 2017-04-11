using UnityEngine;
using System.Collections;

public class Enemy_serch : MonoBehaviour {
	public Texture cursor;
	Camera camera;
	GameObject[] enemy_set;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
//		enemy_set = GameObject.FindGameObjectsWithTag("Enemy");
//		for(int i=0;i<enemy_set.Length;i++){
//			if(enemy_set[i].GetComponent<Enemy_controller>().visible){
//				Vector3 point = camera.WorldToScreenPoint(enemy_set[i].transform.position);
//				GUI.DrawTexture(new Rect(point.x-15,Screen.height - point.y-15,30,30),cursor);
//			}
//
//		}
	}
}
