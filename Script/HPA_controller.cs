using UnityEngine;
using System.Collections;

public class HPA_controller : MonoBehaviour {

	public struct pilot_inputs{
		public float rudder;
		public float elevator;
		public float cadence;
		public pilot_inputs(float z){
			rudder = z;
			elevator = z;
			cadence = z;
		}
	} ;
	public pilot_inputs inputs = new pilot_inputs(0.0f);
	Rigidbody rigidbody;
	public GameObject bullet;
	public GameObject _case;
	public GameObject fire;
	public GameObject missile;
	public GameObject missile_target;
	float reload_timer = 0.0f;
	public bool dead = false;
	public Texture cursor;
	int life = 1;
	public AudioClip shoot_clip;
	AudioSource audio_source;
	public float parameter1 = 0.12f;


	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
		rigidbody.AddForce(transform.up*-1.0f,ForceMode.Impulse);
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Fly_sim();
	}

	void Fly_sim(){
		float dt = Time.deltaTime;
		float speed = Vector3.Dot(rigidbody.velocity,transform.up*-1);
		float vertical_speed = Vector3.Dot(rigidbody.velocity,transform.forward);

		if(dead){
			inputs.cadence = 0.0f;
			inputs.rudder = 0.5f;
		}
		rigidbody.AddForce(transform.up*-1*inputs.cadence*16.0f*dt);
		rigidbody.AddTorque(transform.forward*inputs.rudder*-60.0f*dt);
		rigidbody.AddTorque(transform.right*inputs.elevator*60.0f*dt);

		rigidbody.AddForce(transform.forward*speed*speed*parameter1*dt);//揚力.
		rigidbody.AddTorque(transform.up*transform.right.y*50.0f*dt);//ロール安定.
		rigidbody.AddTorque(transform.up*rigidbody.angularVelocity.y*120.0f*dt);//翼端速度の差でロール.
		rigidbody.AddTorque(transform.right*transform.up.y*-30.0f*dt);//ピッチ安定.
		rigidbody.AddForce(transform.forward*vertical_speed*-20.0f*dt);//翼の上下方向の抵抗.
	}

	public void Shoot(){
		reload_timer += Time.deltaTime;
		if(reload_timer > 0.1f){
			audio_source.PlayOneShot(shoot_clip);
			GameObject _bullet1 = Instantiate(bullet,transform.position + transform.up*-4.0f + transform.right*3.0f,transform.rotation) as GameObject;
			GameObject _bullet2 = Instantiate(bullet,transform.position + transform.up*-4.0f + transform.right*-3.0f,transform.rotation) as GameObject;
			//Instantiate(_case,transform.position + transform.up*-3.0f + transform.right*3.0f,transform.rotation);
			//Instantiate(_case,transform.position + transform.up*-3.0f + transform.right*-3.0f,transform.rotation);
			_bullet1.GetComponent<Rigidbody>().AddForce(_bullet1.transform.up*-30.0f,ForceMode.Impulse);
			_bullet2.GetComponent<Rigidbody>().AddForce(_bullet2.transform.up*-30.0f,ForceMode.Impulse);
			reload_timer = 0.0f;
		}
	}

	public void Lockon_missile(){
		GameObject[] enemy_set;
		float minimam_distance = Mathf.Infinity;
		enemy_set = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i=0; i<enemy_set.Length; i++){
			if(Vector3.Angle(enemy_set[i].transform.position-transform.position,transform.up*-1) < 10.0f){
				if(Vector3.Distance(transform.position,enemy_set[i].transform.position) < 800.0f){
					minimam_distance = Vector3.Distance(transform.position,enemy_set[i].transform.position);
					missile_target = enemy_set[i];
				}
			}
		}
	}

	public void Shoot_missile(){

		if(missile_target!= null){
			GameObject _missile = Instantiate(missile,transform.position + transform.up*-9.0f + transform.right*3.0f + transform.forward*-4.0f,
			                                  Quaternion.AngleAxis(90.0f,transform.right)*transform.rotation) as GameObject;
			_missile.GetComponent<Missile>().target = missile_target;
		}
		missile_target = null;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "bullet" || collision.transform.tag == "missile"){
			life--;
			if(life <= 0){
				if(!dead){
					GameObject _fire;
					_fire = Instantiate(fire,transform.position,transform.rotation) as GameObject;
					_fire.transform.parent = this.transform;
					GameObject.Find("Game_controller").GetComponent<Game_controller>().enemy_shot_down++;
					Destroy(gameObject,30.0f);
				}
				dead = true;
			}
		}
	}

	void OnGUI(){
		if(missile_target!=null){
			if(missile_target.transform.tag == "Enemy"){
				Enemy_controller enemy_controller = missile_target.GetComponent<Enemy_controller>();
				if(enemy_controller != null){
					if(enemy_controller.visible){
						Vector3 point = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(missile_target.transform.position);
						GUI.DrawTexture(new Rect(point.x-15,Screen.height - point.y-15,30,30),cursor);
					}
				}
			}
		}


		
	}
}
	