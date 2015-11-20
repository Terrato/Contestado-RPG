using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopOption : MonoBehaviour {

	private Button button;

	void Awake() {
		button = this.gameObject.GetComponent<Button>();
	}

	public void BuyShit() {
		button.interactable = false;
	}
}
