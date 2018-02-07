using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class main : MonoBehaviour {
	public GameObject DropPrefab;
	public GameObject WallPrefab;
	public Collider OverlapPrefab;

	public static float speed = 1f;
	public static float scale = 1f;

	List<List<Collider>> rows = new List<List<Collider>>();


	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateDrop(int type, int rotation){
		GameObject drop1 = Instantiate (DropPrefab);
		GameObject drop2 = Instantiate (DropPrefab);
		GameObject drop3 = Instantiate (DropPrefab);
		GameObject drop4 = Instantiate (DropPrefab);

		if (type == 0) {
			drop1.transform.localPosition = new Vector3 (-1, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop4.transform.localPosition = new Vector3 (2, 20, 0) * scale;
		} else if (type == 1) {
			drop1.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (0, 19, 0) * scale;
			drop4.transform.localPosition = new Vector3 (1, 19, 0) * scale;
		} else if (type == 2) {
			drop1.transform.localPosition = new Vector3 (-1, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop4.transform.localPosition = new Vector3 (-1, 19, 0) * scale;
		} else if (type == 3) {
			drop1.transform.localPosition = new Vector3 (-1, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop4.transform.localPosition = new Vector3 (0, 20, 0) * scale;
		} else if (type == 4) {
			drop1.transform.localPosition = new Vector3 (-1, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop4.transform.localPosition = new Vector3 (1, 20, 0) * scale;
		} else if (type == 5) {
			drop1.transform.localPosition = new Vector3 (-1, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (0, 19, 0) * scale;
			drop4.transform.localPosition = new Vector3 (1, 19, 0) * scale;
		} else if (type == 6) {
			drop1.transform.localPosition = new Vector3 (0, 20, 0) * scale;
			drop2.transform.localPosition = new Vector3 (1, 20, 0) * scale;
			drop3.transform.localPosition = new Vector3 (-1, 19, 0) * scale;
			drop4.transform.localPosition = new Vector3 (0, 19, 0) * scale;
		}

		drop1.transform.localScale = Vector3.one * scale;
		drop2.transform.localScale = Vector3.one * scale;
		drop3.transform.localScale = Vector3.one * scale;
		drop4.transform.localScale = Vector3.one * scale;

		FixedJoint joint1 = drop1.AddComponent<FixedJoint>();
		joint1.connectedBody = drop2.rigidbody;
		joint1.connectedBody = drop3.rigidbody;
		joint1.connectedBody = drop4.rigidbody;

		System.Random rng =new System.Random();
		drop1.transform.RotateAround (new Vector3 (0, 20, 0), Vector3.forward, 90f * rng.Next (1, 3));
	}

	void CreateOverlaps(){
		for (int j = 0; j < 12; j++) {
			List<Collider> row = new List<Collider>();
			for (int i = 0; i < 20; i++) {
				Collider c = Physics.OverlapBox (new Vector3 (i, j, 0), new Vector3 (1, 1, 1));
				row.Add (c);
			}
		}
	}
}
