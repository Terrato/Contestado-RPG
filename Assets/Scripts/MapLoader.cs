using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour {

	public static Tile[][] map01;
	//Jagged arrays
	public GameObject TilePrefab;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		// É assim que se muda a cor de um tile por código
		//map01[0][1].gameObject.GetComponent<Renderer>().material.color = Color.blue;
	}

	



	
}
