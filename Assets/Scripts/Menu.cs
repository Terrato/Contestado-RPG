using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Menu : MonoBehaviour{

	public GameObject statusScreen;
	public Character character;

	public void AlterMenuState() {
		this.gameObject.SetActive(!this.gameObject.activeSelf);
	}

	public void AlterStatusScreenState() {
		statusScreen.gameObject.SetActive(!statusScreen.activeSelf);
		InterfaceController.menuOptionLock = !InterfaceController.menuOptionLock;
	}
}

