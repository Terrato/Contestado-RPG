using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

	private GameObject shop;
	public List<GameObject> ShopOptions;

	void Awake() {
		shop = this.gameObject;
		shop.SetActive(false);
	}

	public void ShowShop() {
		shop.SetActive(!shop.activeSelf);	
	}
}
