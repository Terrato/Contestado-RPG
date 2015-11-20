using UnityEngine;
using System.Collections;

public class ResolutionControl : MonoBehaviour {
	Camera Cam1; // where you will set your default ( main ) camera

	void Start () {
		Cam1.aspect = (Screen.currentResolution.width / Screen.currentResolution.height); 
		//so this would stretch the game scene in order to adjust it to the Device's screen  
	}

	void Update () {
	
	}
}
