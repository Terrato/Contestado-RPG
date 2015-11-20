using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

	public GameObject marker, cityMenu;
	public Vector3 position;

	public void ShowMarker() {
		marker.SetActive(true);
		marker.transform.position = position;
	}

	public void AlterCityMenuState() {
		cityMenu.SetActive(!cityMenu.activeSelf);	
	}
}
