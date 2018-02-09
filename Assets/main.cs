using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Main : MonoBehaviour {
	public GameObject DropPrefab;
	public GameObject WallPrefab;
	public Collider OverlapPrefab;

	public static float speed = 1f;
	public static float scale = 1f;
	public static int	spx = 6;
	public static int	spy = 20;

	List<List<Collider>> rows = new List<List<Collider>>();
	List<GameObject> drop = new List<GameObject> ();


	// Use this for initialization
	void Awake () {
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 2;

		CreateDrop ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			MoveDropLeft ();
		} else if (Input.GetKeyDown (KeyCode.D)) {
			MoveDropRight ();
		} else if (Input.GetKeyDown (KeyCode.W)) {
			MoveDropDown ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			RotateDrop ();
		}
		MoveDropDown ();
	}

	void CreateDrop(){
		System.Random rng1 =new System.Random();
		System.Random rng2 =new System.Random();

		int type=rng1.Next(0, 6);
		int rotation=rng2.Next(1, 3);

		GameObject drop1 = Instantiate (DropPrefab);
		GameObject drop2 = Instantiate (DropPrefab);
		GameObject drop3 = Instantiate (DropPrefab);
		GameObject drop4 = Instantiate (DropPrefab);

		if (type == 0) {
			drop1.transform.localPosition = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx+2, spy, 0) * scale;
		} else if (type == 1) {
			drop1.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx, spy-1, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx+1, spy-1, 0) * scale;
		} else if (type == 2) {
			drop1.transform.localPosition = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx-1, spy-1, 0) * scale;
		} else if (type == 3) {
			drop1.transform.localPosition = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
		} else if (type == 4) {
			drop1.transform.localPosition = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
		} else if (type == 5) {
			drop1.transform.localPosition = new Vector3 (spx-1, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx, spy-1, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx+1, spy-1, 0) * scale;
		} else if (type == 6) {
			drop1.transform.localPosition = new Vector3 (spx, spy, 0) * scale;
			drop2.transform.localPosition = new Vector3 (spx+1, spy, 0) * scale;
			drop3.transform.localPosition = new Vector3 (spx-1, spy-1, 0) * scale;
			drop4.transform.localPosition = new Vector3 (spx, spy-1, 0) * scale;
		}

		drop1.transform.localScale = Vector3.one * scale;
		drop2.transform.localScale = Vector3.one * scale;
		drop3.transform.localScale = Vector3.one * scale;
		drop4.transform.localScale = Vector3.one * scale;

		FixedJoint joint1 = drop1.AddComponent<FixedJoint>();
		joint1.connectedBody = (drop2.GetComponent(typeof(Rigidbody)) as Rigidbody);
		joint1.connectedBody = (drop2.GetComponent(typeof(Rigidbody)) as Rigidbody);
		joint1.connectedBody = (drop2.GetComponent(typeof(Rigidbody)) as Rigidbody);

		System.Random rng =new System.Random();
		drop1.transform.RotateAround (new Vector3 (0, 20, 0), Vector3.forward, 90f * rotation);

		drop.Add (drop1);
		drop.Add (drop2);
		drop.Add (drop3);
		drop.Add (drop4);
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
		drop [0].transform.localPosition += new Vector3(-1, 0, 0)*scale;
		if (!CheckOverlap()) {
			drop [0].transform.localPosition += new Vector3(1, 0, 0)*scale;
		}
	}

	void MoveDropRight(){
		drop [0].transform.localPosition += new Vector3(1, 0, 0)*scale;
		if (!CheckOverlap()) {
			drop [0].transform.localPosition += new Vector3(-1, 0, 0)*scale;
		}
	}

	void MoveDropDown(){
		int j = 0;
		foreach (GameObject i in drop) {
			if (Physics.Raycast(i.transform.localPosition, Vector3.down, 0.5f*scale)) {
				j++;
			}
		}

		if (j == 0) {
			drop [0].transform.localPosition += new Vector3(0, -1, 0)*scale;
		} else {
			foreach (GameObject k in drop) {
				Rigidbody rb = k.GetComponent(typeof(Rigidbody)) as Rigidbody;
				rb.isKinematic = true;
			}
			drop.Clear();
			CreateDrop ();
		}
	}

	void RotateDrop(){
		drop[0].transform.RotateAround (drop[1].transform.localPosition, Vector3.forward, 90f);

		if (!CheckOverlap()) {
			drop[0].transform.RotateAround (drop[1].transform.localPosition, Vector3.forward, -90f);
		}
	}

	bool CheckOverlap(){
		int cl = 0;
		foreach (GameObject i in drop) {
			Collider[] c = Physics.OverlapBox (i.transform.localPosition, new Vector3 (1, 1, 1));
			cl = (c.Length > 1) ? cl : cl + 1;
		}

		return (cl > 0);
	}
}
