using UnityEngine;
using System;
using System.Collections;


public class gameController : MonoBehaviour {

	ArrayList figures = new ArrayList();
	ArrayList pins = new ArrayList();
	ArrayList stars = new ArrayList();
	string nowLevel;
	GameObject [] ids = new GameObject[100];


	// Use this for initialization
	void Start () {
		nowLevel = "1"; //just for now

		//opening the map file
		string fileName = nowLevel + ".txt";
		System.IO.StreamReader map = 
			new System.IO.StreamReader(fileName);
		string cnd;
		cnd = map.ReadLine ();
		string [] splits = cnd.Split (new Char[] {' '});
		int figNum = Convert.ToInt32 (splits [0]);
		int pinNum = Convert.ToInt32 (splits [1]);
		int starNum = Convert.ToInt32 (splits [2]);
		for(int i = 0; i < figNum;i++){
			cnd = map.ReadLine();
			int pointsNum = Convert.ToInt32(cnd);
			for(int j = 0;j < pointsNum; j++){
				cnd = map.ReadLine();
				splits = cnd.Split(new Char[] {' '});
				int x,y;
				x = Convert.ToInt32 (splits [0]);
				y = Convert.ToInt32 (splits [1]);
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				ids[j] = cube;
				cube.AddComponent<FixedJoint>();
				cube.AddComponent<Rigidbody>();
				cube.AddComponent<BoxCollider>();
				cube.rigidbody.mass = 5;
				cube.transform.position = new Vector3 (x, y,0);
				if(j > 0){
					cube.GetComponent<FixedJoint>().connectedBody = ids[j-1].rigidbody;
				}
				figures.Add(cube);
			}

				ids[0].GetComponent<FixedJoint>().connectedBody = ids[1].rigidbody;
		}
		for(int i = 0 ; i < pinNum;i++){
			cnd = map.ReadLine();
			splits = cnd.Split(new Char[] {' '});
			int x,y;
			x = Convert.ToInt32 (splits [0]);
			y = Convert.ToInt32 (splits [1]);
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//cube.AddComponent<FixedJoint>();
			cube.AddComponent<Rigidbody>();
			cube.AddComponent<BoxCollider>();
			cube.rigidbody.mass = 5;
			cube.rigidbody.isKinematic = true;
			cube.rigidbody.useGravity = false;
			//cube.GetComponent<FixedJoint>().anchor = new Vector3(x,y,0);
			cube.rigidbody.useGravity = false;
			cube.transform.position = new Vector3 (x, y,0);
			pins.Add(cube);
		}
		for(int i = 0 ; i < starNum;i++){
			cnd = map.ReadLine();
			splits = cnd.Split(new Char[] {' '});
			int x,y;
			x = Convert.ToInt32 (splits [0]);
			y = Convert.ToInt32 (splits [1]);
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3 (x, y,0);
			stars.Add(sphere);
		}
		//finished
		//Nikita has small penis
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
