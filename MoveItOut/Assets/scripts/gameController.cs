using UnityEngine;
using System;
using System.Collections;


public class gameController : MonoBehaviour {

	int pointsNum;
	GameObject[] figures = new GameObject[20];
	GameObject[] pins = new GameObject[20];
	GameObject[] stars = new GameObject[20];
	int pinNum,figNum,starNum;
	string nowLevel;
	GameObject [] ids = new GameObject[100];
	int indexNowFly = 0;
	int indexNowWait = 0;


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
		figNum = Convert.ToInt32 (splits [0]);
		pinNum = Convert.ToInt32 (splits [1]);
		starNum = Convert.ToInt32 (splits [2]);
		for(int i = 0; i < figNum;i++){
			cnd = map.ReadLine();
			pointsNum = Convert.ToInt32(cnd);
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
				figures[j] = cube;
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
			pins[i] = cube;
		}
		for(int i = 0 ; i < starNum;i++){
			cnd = map.ReadLine();
			splits = cnd.Split(new Char[] {' '});
			int x,y;
			x = Convert.ToInt32 (splits [0]);
			y = Convert.ToInt32 (splits [1]);
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3 (x, y,0);
			stars[i] = sphere;
		}

		countMass(1);
		gdir = 1;
		//finished
		//Nikita has small penis
		
	}

	// Update is called once per frame



	int gdir = 1; // 1 - down 2 - up 3 - right - left
	int nowg = 1;

	void changeGravity(int x){
		if(x == 1)
			Physics.gravity = new Vector3(0, -1.0F, 0);
		if(x == 2)
			Physics.gravity = new Vector3(0, 1.0F, 0);
		if(x == 3)
			Physics.gravity = new Vector3(1.0F, 0, 0);
		if(x == 4)
			Physics.gravity = new Vector3(-1.0F, 0, 0);
	}

	void countMass(int x){
		float min_y = 100000;
		float min_x = 100000;
		float max_y = -100000;
		float max_x = -100000;


		if (x == 1) { //down
			//finding upest pin
			int indexToChange =0;
			float cordToChange =0;
			for(int i = 0 ; i < pinNum;i++){
				float p_y = pins[i].transform.position.y;
				int ok = 0;
				for(int j = 0 ; j < pointsNum;j++){
					if(figures[j].transform.position.y  > p_y)
						ok = 1;
				}
				if(p_y > max_y && ok == 1){
					max_y = p_y;
					indexToChange = i;
				}
		    }
			cordToChange = pins[indexToChange].transform.position.x;
			indexNowWait  = indexToChange;
			//seting mass
			int leftC = 0,rightC = 0;
			for(int i = 0; i < pointsNum;i++){
				if(figures[i].transform.position.x > cordToChange){
					rightC++;
				}
				if(figures[i].transform.position.x < cordToChange){
					leftC++;
				}
			}
			float sideMass = (leftC+rightC)/2;
			for(int i = 0; i < pointsNum;i++){
				if(figures[i].transform.position.x > cordToChange){
					figures[i].rigidbody.mass = sideMass/rightC;
				}
				if(figures[i].transform.position.x < cordToChange){
					figures[i].rigidbody.mass = sideMass/leftC;
				}
				if(figures[i].transform.position.x == cordToChange){
					figures[i].rigidbody.mass = 3;
					indexNowFly = i;
				}
			}
		}//ok,pritty nice for down


		//lets try fo up
		if (x == 2) { //up
			//finding upest pin
			int indexToChange = 0;
			float cordToChange = 0;
			for(int i = 0 ; i < pinNum;i++){
				float p_y = pins[i].transform.position.y;
				int ok = 0;
				for(int j = 0 ; j < pointsNum;j++){
					if(figures[j].transform.position.y  < p_y )
						ok = 1;
				}
				if(p_y < min_y && ok == 1){
					min_y = p_y;
					indexToChange = i;
				}
			}
			cordToChange = pins[indexToChange].transform.position.x;
			indexNowWait  = indexToChange;
			//seting mass
			int leftC = 0,rightC = 0;
			for(int i = 0; i < pointsNum;i++){
				if(figures[i].transform.position.x > cordToChange){
					rightC++;
				}
				if(figures[i].transform.position.x < cordToChange){
					leftC++;
				}
			}
			float sideMass = (pointsNum - 1)/2;
			for(int i = 0; i < pointsNum;i++){
				if(figures[i].transform.position.x > cordToChange){
					figures[i].rigidbody.mass = sideMass/rightC;
				}
				if(figures[i].transform.position.x < cordToChange){
					figures[i].rigidbody.mass = sideMass/leftC;
				}
				if(figures[i].transform.position.x == cordToChange){
					figures[i].rigidbody.mass = 3;
					indexNowFly = i;
				}
			}
		}//ok,pritty nice for down





	}

	void Update () {
		if(gdir == 1){

			if(figures[indexNowFly].transform.position.y <= pins[indexNowWait].transform.position.y+1){
				figures[indexNowFly].transform.position = new Vector3 (pins[indexNowWait].transform.position.x, pins[indexNowWait].transform.position.y+1,0);
			}
		}
		if(gdir == 2){
			if(figures[indexNowFly].transform.position.y >= pins[indexNowWait].transform.position.y-1){
				figures[indexNowFly].transform.position = new Vector3 (pins[indexNowWait].transform.position.x, pins[indexNowWait].transform.position.y-1,0);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)){
			gdir = 1; //down
			countMass (gdir);
			changeGravity(gdir);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)){
			gdir = 2; // up
			countMass (gdir);
			changeGravity(gdir);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			gdir = 4; //left
			countMass (gdir);
			changeGravity(gdir);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)){
			gdir = 3; //right
			countMass (gdir);
			changeGravity(gdir);
		}
	}
}