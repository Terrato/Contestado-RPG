using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour {

	public Text warfunds;

	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money"));
	}
	
	// Update is called once per frame
	void Update () {
		print(PlayerPrefs.GetInt("money"));
		warfunds.text = "Fundos de guerra: " + PlayerPrefs.GetInt("money");
	}
}

