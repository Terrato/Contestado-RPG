using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

	public Character char1, char2, char3, char4, char5;
	public Enemy enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, enemy7, enemy8, enemy9;

	bool endedBattle;

	public GameObject endBattleScreen;
	public Text text, text1;
	public GameObject[] array = new GameObject[14];
	public int whoIsActive = 0;
	public int money;

	// Use this for initialization
	void Start () {
		money = Random.Range(5000, 10000);
		int b = 0;

		char1.SetPosition(Maps.map01, 1, b);
		char2.SetPosition(Maps.map01, 2, b);
		char3.SetPosition(Maps.map01, 3, b);
		char4.SetPosition(Maps.map01, 4, b);
		char5.SetPosition(Maps.map01, 5, b);
		
		enemy1.SetPosition(Maps.map01, 1, 8);
		enemy2.SetPosition(Maps.map01, 2, 7);
		enemy3.SetPosition(Maps.map01, 3, 7);
		enemy4.SetPosition(Maps.map01, 4, 7);
		enemy5.SetPosition(Maps.map01, 5, 8);
		enemy6.SetPosition(Maps.map01, 1, 9);
		enemy7.SetPosition(Maps.map01, 2, 9);
		enemy8.SetPosition(Maps.map01, 4, 9);
		enemy9.SetPosition(Maps.map01, 5, 9);

		SetActiveUnit(whoIsActive);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enemy1.gameObject.activeSelf == false && enemy2.gameObject.activeSelf == false && enemy3.gameObject.activeSelf == false &&
			enemy4.gameObject.activeSelf == false && enemy5.gameObject.activeSelf == false && enemy6.gameObject.activeSelf == false &&
			enemy7.gameObject.activeSelf == false && enemy8.gameObject.activeSelf == false && enemy9.gameObject.activeSelf == false &&
			endedBattle == false) {
			EndBattleWin();
		}
		if (char1.gameObject.activeSelf == false && char2.gameObject.activeSelf == false && char3.gameObject.activeSelf == false &&
			char4.gameObject.activeSelf == false && char5.gameObject.activeSelf == false && endedBattle == false) {
			EndBattleLose();
		}	
	}

	public Character GetCurrentActiveUnit() {
		if (endedBattle == false) {
			for (int i = 0; i < array.Length; i++) {
				if (array[i].gameObject.GetComponent<Character>() && i == whoIsActive) {
					return array[i].gameObject.GetComponent<Character>();
				}
			}
		}
		return null;
	}

	public void SetActiveUnit(int unitIndex) {
		if (endedBattle == false) {
			if (array[unitIndex].activeSelf) {
				if (array[unitIndex].gameObject.GetComponent<Character>()) {
					array[unitIndex].gameObject.GetComponent<Character>().turn = true;
					//array[unitIndex].gameObject.GetComponent<Renderer>().material.color = Color.blue;
					array[unitIndex].gameObject.GetComponent<Character>().StartTurn();
				}
				if (array[unitIndex].gameObject.GetComponent<Enemy>()) {
					array[unitIndex].gameObject.GetComponent<Enemy>().turn = true;
					//array[unitIndex].gameObject.GetComponent<Renderer>().material.color = Color.red;
					array[unitIndex].gameObject.GetComponent<Enemy>().StartTurn();
				}
			} else {
				if (unitIndex < array.Length-1) {
					for (int i = unitIndex; i < array.Length; i++) {
						if (i < array.Length) {
							if (array[i].activeSelf) {
								if (array[i].gameObject.GetComponent<Character>()) {
									whoIsActive = i;
									//array[unitIndex].gameObject.GetComponent<Character>().turn = true;
									array[unitIndex].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
									array[i].gameObject.GetComponent<Character>().StartTurn();
									break;
								}
								if (array[i].gameObject.GetComponent<Enemy>()) {
									whoIsActive = i;
									//array[unitIndex].gameObject.GetComponent<Enemy>().turn = true;
									array[unitIndex].gameObject.GetComponent<Renderer>().material.color = Color.red;
									array[i].gameObject.GetComponent<Enemy>().StartTurn();
									break;
								}
							}
						} else {
							break;
						}
					}
				}
				if (unitIndex == array.Length - 1){
					for (int i = 0; i < unitIndex; i++) {
						if (array[i].activeSelf) {
							if (array[i].gameObject.GetComponent<Character>()) {
								whoIsActive = i;
								array[unitIndex].gameObject.GetComponent<Renderer>().material.color = Color.magenta;
								array[i].gameObject.GetComponent<Character>().StartTurn();
								break;
							}
							if (array[i].gameObject.GetComponent<Enemy>()) {
								whoIsActive = i;
								array[i].gameObject.GetComponent<Enemy>().StartTurn();
								break;
							}
						}
					}
				}
			}
		}
	}

	void EndBattleWin() {
		endedBattle = true;
		text.color = new Color32(238, 212, 0, 255);
		text.text = "Parabéns!";
		text1.text = "Seu esquadrão ganhou a batalha!\n\n\nDinheiro arrecadado: " + money;
		endBattleScreen.SetActive(true);
		endBattleScreen.GetComponent<Animator>().SetBool("Victory", true);
		// Mostrar o resultado e etc.
		PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + money);
		print(PlayerPrefs.GetInt("money"));
	}

	void EndBattleLose() {
		endedBattle = true;
		text.color = new Color32(0, 80, 238, 255);
		text.text = "Derrota";
		text1.text = "Seu esquadrão foi vencido em combate...";
		endBattleScreen.SetActive(true);
		endBattleScreen.GetComponent<Animator>().SetBool("Defeat", true);
		// Mostrar o resultado e etc.
		print("END");
	}

	public void ReturnToMap() {
		Application.LoadLevel("WorldMap");
	}
}
