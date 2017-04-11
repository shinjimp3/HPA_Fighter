using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {
	GameObject[] enemy_set;
	Rect radar_rect = new Rect(10,10,200,200);
	public Texture player_radar;
	public Texture enemy_radar_up;
	public Texture enemy_radar_down;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnGUI(){
		enemy_set = GameObject.FindGameObjectsWithTag("Enemy");
		Vector3 player_axis_f = new Vector3(transform.up.x*-1,0.0f,transform.up.z*-1).normalized;
		Vector3 player_axis_r = new Vector3(transform.right.x,0.0f,transform.right.z).normalized;
		GUI.DrawTexture(new Rect(radar_rect.x+radar_rect.width/2-10,radar_rect.y+radar_rect.height/2+10,20,20),player_radar);
		for(int i=0; i<enemy_set.Length; i++){
			Vector3 rerational_position3 = enemy_set[i].transform.position - transform.position;
			rerational_position3.y = 0.0f;
			Vector2 rerational_position2 = new Vector2(Vector3.Dot(rerational_position3,player_axis_r),Vector3.Dot(rerational_position3,player_axis_f));
			if(rerational_position2.x<1000.0f&&rerational_position2.y<1000.0f){
				//float angle = Mathf.Rad2Deg*Mathf.Atan2(Vector3.Dot(enemy_set[i].transform.up*-1,player_axis_f),Vector3.Dot(enemy_set[i].transform.up*-1,player_axis_r));

				rerational_position2 *= radar_rect.width/(1000.0f*2);
				Rect rect = new Rect(radar_rect.x+Mathf.FloorToInt(radar_rect.width/2.0f+rerational_position2.x-5),
				                     radar_rect.y+Mathf.FloorToInt(radar_rect.width/2.0f-rerational_position2.y+5),10,10);
				//GUIUtility.RotateAroundPivot(angle,new Vector2(rect.x+5,rect.y-5));
				if(enemy_set[i].transform.position.y > transform.position.y){
					GUI.DrawTexture(rect,enemy_radar_up);
				}else{
					GUI.DrawTexture(rect,enemy_radar_down);
				}
			}
		}
	}
}
