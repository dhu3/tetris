using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Tetris;

public class Main : MonoBehaviour {
	public GameObject DropPrefab;
	public GameObject WallPrefab;
	public Collider OverlapPrefab;

	public static int speed = 2;
	public static float scale = 0.1f;
	public static int	spx = 6;
	public static int	spy = 20;

	List<List<Collider>> rows = new List<List<Collider>>();
	List<GameObject> drop = new List<GameObject> ();


	// Use this for initialization
	void Start () {
		//QualitySettings.vSyncCount = 0;
		//Application.targetFrameRate = 2;

		GameObject WallS = Instantiate (WallPrefab);
		WallS.transform.position = new Vector3 (6, 0, 0)*scale;
		WallS.transform.localScale = new Vector3 (12, 1, 1)*scale;

		GameObject WallW = Instantiate (WallPrefab);
		WallW.transform.position = new Vector3 (0, 10, 0)*scale;
		WallW.transform.localScale = new Vector3 (1, 20, 1)*scale;

		GameObject WallE = Instantiate (WallPrefab);
		WallE.transform.position = new Vector3 (12, 10, 0)*scale;
		WallE.transform.localScale= new Vector3 (1,20,1)*scale;

		CreateDrop ();
	}

	// Update is called once per frame
	int frame=1;
	void Update () {
		frame++;
		if (frame >= 60) {
			frame = 0;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			MoveDropLeft ();
			if (CheckOverlap) {
				MoveDropRight();
			}
		} else if (Input.GetKeyDown (KeyCode.D)) {
			MoveDropRight ();
			if (CheckOverlap) {
				MoveDropLeft();
			}
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			RotateDrop (1);
		}

		if (60 % (60 / speed)) {
			MoveDropDown ();
			if (CheckOverlap) {
				MoveDropUp ();
				foreach (GameObject k in drop) {
					k.AddComponent<BoxCollider>();
				}
				drop.Clear();
				CreateDrop ();
			}
		}

		frame = (frame >= 60) ? 1 : frame;
	}

	void CreateDrop(){
		System.Random rng1 =new System.Random();
		System.Random rng2 =new System.Random();

		//int type=rng1.Next(0, 6);
		int type = 0; 
		//int rotation=rng2.Next(1, 3);
		int rotation=1;

		GameObject drop1 = Instantiate (DropPrefab);
		GameObject drop2 = Instantiate (DropPrefab);
		GameObject drop3 = Instantiate (DropPrefab);
		GameObject drop4 = Instantiate (DropPrefab);

		if (type == 0) {
			drop1.transform.position = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.position = new Vector3 (spx+2, spy, 0) * scale;
		} else if (type == 1) {
			drop1.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx, spy-1, 0) * scale;
			drop4.transform.position = new Vector3 (spx+1, spy-1, 0) * scale;
		} else if (type == 2) {
			drop1.transform.position = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.position = new Vector3 (spx-1, spy-1, 0) * scale;
		} else if (type == 3) {
			drop1.transform.position = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.position = new Vector3 (spx, spy-1, 0) * scale;
		} else if (type == 4) {
			drop1.transform.position = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.position = new Vector3 (spx+1, spy-1, 0) * scale;
		} else if (type == 5) {
			drop1.transform.position = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx, spy-1, 0) * scale;
			drop4.transform.position = new Vector3 (spx+1, spy-1, 0) * scale;
		} else if (type == 6) {
			drop1.transform.position = new Vector3 (spx, spy, 0) * scale;
			drop2.transform.position = new Vector3 (spx+1, spy, 0) * scale;
			drop3.transform.position = new Vector3 (spx-1, spy-1, 0) * scale;
			drop4.transform.position = new Vector3 (spx, spy-1, 0) * scale;
		}

		drop1.transform.localScale = Vector3.one * scale;
		drop2.transform.localScale = Vector3.one * scale;
		drop3.transform.localScale = Vector3.one * scale;
		drop4.transform.localScale = Vector3.one * scale;

//		FixedJoint joint1 = drop1.AddComponent<FixedJoint>();
//		joint1.connectedBody = (drop2.GetComponent(typeof(Rigidbody)) as Rigidbody);
//		joint1.connectedBody = (drop3.GetComponent(typeof(Rigidbody)) as Rigidbody);
//		joint1.connectedBody = (drop4.GetComponent(typeof(Rigidbody)) as Rigidbody);

		drop1.transform.parent = drop2.transform;
		drop3.transform.parent = drop2.transform;
		drop4.transform.parent = drop2.transform;

		drop.Add (drop1);
		drop.Add (drop2);
		drop.Add (drop3);
		drop.Add (drop4);

		//drop2.transform.RotateAround (drop2.transform.position, drop2.transform.forward, Time.deltaTime*90f * rotation);
		//drop2.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		RotateDrop(rotation);

	}

	void ClearRows(){
		for (int j = 0; j < 20; j++) {
			
			List<Collider> row = new List<Collider>();
			int cl = 0;
			for (int i = 0; i < 12; i++) {
				Collider[] c = Physics.OverlapBox (new Vector3 (i, j, 0), new Vector3 (1, 1, 1));
				cl = (c.Length > 0) ? cl : cl + 1;
				row.Add (c [0]);
			}

			if (cl == 12) {
				foreach (Collider k in row) {
					Destroy(k.gameObject);
				}
			}
		}
	}
		

	void MoveDropLeft(){
		drop [1].transform.position += new Vector3(-1, 0, 0)*scale;
	}

	void MoveDropRight(){
		drop [1].transform.position += new Vector3(1, 0, 0)*scale;
	}

	void MoveDropUp(){
		drop [1].transform.position += new Vector3(0, 1, 0)*scale;
	}

	void MoveDropDown(){
		drop [1].transform.position += new Vector3(0, -1, 0)*scale;
	}

//	void MoveDropDown(){
//		int j = 0;
//		foreach (GameObject i in drop) {
//			if (Physics.Raycast(i.transform.position, Vector3.down, 0.5f*scale)) {
//				j++;
//			}
//		}
//
//		if (j == 0) {
//			drop [1].transform.position += new Vector3(0, -1, 0)*scale;
//		} else {
//			foreach (GameObject k in drop) {
//				//Rigidbody rb = k.GetComponent(typeof(Rigidbody)) as Rigidbody;
//				//rb.isKinematic = true;
//				k.AddComponent<BoxCollider>();
//			}
//			drop.Clear();
//			CreateDrop ();
//		}
//	}

	void RotateDrop(int r){
		foreach (GameObject i  in drop) {
			//float o = 2250 * Time.deltaTime * r;
			i.transform.RotateAround (drop [1].transform.position, Vector3.forward, 45f*r);
		}

		if (CheckOverlap()) {
			foreach (GameObject i  in drop) {
				i.transform.RotateAround (drop [1].transform.position, Vector3.forward, 45f*r);
			}
		}
	}

	bool CheckOverlap(){
		int cl = 0;
		foreach (GameObject i in drop) {
			Collider[] c = Physics.OverlapBox (i.transform.position, new Vector3 (1, 1, 1)*scale);
			cl = (c.Length > 0) ? cl : cl + 1;
		}

		return (cl > 0);
	}
}
