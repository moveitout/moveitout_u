using UnityEngine;
using System;
using System.Collections;


public class gameController : MonoBehaviour {

	int pointsNum;
	public static bool isCollision;
	GameObject[] figures = new GameObject[20];
	GameObject[] pins = new GameObject[20];
	GameObject[] stars = new GameObject[20];
	int pinNum,figNum,starNum;
	string nowLevel;
	GameObject [] ids = new GameObject[100];
	const float speed = 2F;

	// Use this for initialization
	void Start () {
		isCollision = false;
		nowLevel = "1"; //just for now

		//opening the map file
		string fileName = nowLevel + ".txt";
		System.IO.StreamReader map = 
			new System.IO.StreamReader(fileName);
		string cnd;
		cnd = map.ReadLine ();
		string [] splits = cnd.Split (new Char[] {' '});
		figNum = Convert.ToInt32 (splits [0]);
		pinNum = Convert.ToInt32 (splits [1]);
		starNum = Convert.ToInt32 (splits [2]);
		for(int i = 0; i < figNum;i++){
			cnd = map.ReadLine();
			pointsNum = Convert.ToInt32(cnd);
			for(int j = 0; j < pointsNum; j++){
				cnd = map.ReadLine();
				splits = cnd.Split(new Char[] {' '});
				int x,y;
				x = Convert.ToInt32 (splits [0]);
				y = Convert.ToInt32 (splits [1]);
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.AddComponent<FixedJoint>();
				cube.AddComponent<BoxCollider>();
				cube.rigidbody.mass = 10;
				cube.rigidbody.freezeRotation = true;
				cube.transform.position = new Vector3 (x, y, 0);
				if(j > 0){
					cube.GetComponent<FixedJoint>().connectedBody = ids[j - 1].rigidbody;
				}
				ids[j] = cube;
				figures[j] = cube;
			}

				ids[0].GetComponent<FixedJoint>().connectedBody = ids[1].rigidbody;
		}
		for(int i = 0; i < pinNum; i++){

			cnd = map.ReadLine();
			splits = cnd.Split(new Char[] {' '});
			int x,y;
			x = Convert.ToInt32 (splits [0]);
			y = Convert.ToInt32 (splits [1]);
			GameObject pin = GameObject.CreatePrimitive(PrimitiveType.Cube);
			pin.AddComponent<Rigidbody>();
			pin.AddComponent<MyCollision>();
			pin.AddComponent<BoxCollider>();
			pin.rigidbody.mass = 1F;
			pin.rigidbody.isKinematic = true;
			pin.rigidbody.useGravity = false;
			pin.rigidbody.collider.isTrigger = true;
			pin.transform.position = new Vector3 (x, y, 0);
			pins[i] = pin;
		}
		/*for(int i = 0 ; i < starNum;i++){
			cnd = map.ReadLine();
			splits = cnd.Split(new Char[] {' '});
			int x,y;
			x = Convert.ToInt32 (splits [0]);
			y = Convert.ToInt32 (splits [1]);
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3 (x, y, 0);
			stars[i] = sphere;
		}*/
		Physics.gravity = Vector3.zero;
	}

	void changeDirection(int x){
		if (x == 1) {
			for (int i = 0; i < pointsNum; i++) {
				figures[i].rigidbody.velocity = new Vector3(0, -speed, 0);
			}
		}
		if (x == 2) {
			for (int i = 0; i < pointsNum; i++) {
				figures[i].rigidbody.velocity = new Vector3(0, speed, 0);
			}
		}
		if (x == 3) {
			for (int i = 0; i < pointsNum; i++) {
				figures[i].rigidbody.velocity = new Vector3(speed, 0, 0);
			}
		}
		if (x == 4) {
			for (int i = 0; i < pointsNum; i++) {
				figures[i].rigidbody.velocity = new Vector3(-speed, 0, 0);
			}
		}


	}

	void Update () {
		if (isCollision) {
			Debug.Log("update");
			for (int i = 0; i < pointsNum; i++) {
				figures[i].rigidbody.velocity = Vector3.zero;
			}
			isCollision = false;
		}
		if (figures [0].rigidbody.velocity != Vector3.zero)
						return;

		if (Input.GetKeyDown (KeyCode.DownArrow)){
			changeDirection (1);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)){
			changeDirection (2);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			changeDirection (4);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)){
			changeDirection (3);
		}
	}
	}
