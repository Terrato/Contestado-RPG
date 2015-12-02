using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Save : MonoBehaviour {

	public void SaveShit () {
		//PlayerPrefs.SetInt("INsolb fls ks knmf T", 10);
		PlayerPrefs.Save();
		//PlayerPrefs.DeleteAll();
	}
	
}
