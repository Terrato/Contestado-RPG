using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

	public string name, className;
	public int HP, MP, HPMax, MPMax, damage, armor, xPosition, yPosition, level;
	public bool turn = false;
	public Camera camera;
	public BattleController battleController;
	public InterfaceController interfaceController;
	public Text visibleHP, visibleName, visibleNameHUD, visibleClass, visibleClassHUD, visibleClassLevelHUD;
	public Color color;
	private string[] nameArray = new string[] { "Sargento", "Cabo", "Soldado", "Aspirante", "Tenente", "Capitão", "Major"};
	private string[] surnameArray = new string[] { " Silva", " Souza", " Vieira", " Demarchi", " Scherer", " Spaghetti", " Alves", " Alencar", " Bason", " Dutra", " Ellard", " Guerra", " Bigode", " Loch", " Macedo", " Nascimento", " Romão", " Schwartz" };
	private string[] classArray = new string[] { "Infantaria", "Fuzileiro", "Franco-Atirador", "Especialista em explosivos", "Médico", "Socorrista", "Engenheiro" };

	// Use this for initialization
	void Awake () {
		string pickedName = nameArray[Random.Range(0, nameArray.Length)] + surnameArray[Random.Range(0, surnameArray.Length)];
		name = pickedName;

		string pickedClass = classArray[Random.Range(0, classArray.Length)];
		className = pickedClass;

		level = Random.Range(30, 50);

		color = this.gameObject.GetComponent<Renderer>().material.color;

		HPMax = Random.Range(150, 300);
		HP = HPMax;
		MPMax = Random.Range(10, 50);
		MP = MPMax;
		damage = Random.Range(10, 30);
		armor = Random.Range(5, 10);

	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (HP <= 0) {
			this.gameObject.SetActive(false);
		}
		if (InterfaceController.currentCameraState) {
			this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
		} else {
			this.gameObject.transform.LookAt(camera.transform.position, Vector3.up);
		}
	}

	public void SetPosition(Tile[][] array, int positionX, int positionY) {
		xPosition = positionX;
		yPosition = positionY;
		array[positionX][positionY].occupant = this.gameObject;
		array[xPosition][yPosition].isOccupiedByEnemy = true;
		this.gameObject.transform.position = new Vector3(array[positionX][positionY].gameObject.transform.position.x
															, array[positionX][positionY].gameObject.transform.localScale.y
															, array[positionX][positionY].gameObject.transform.position.z);
	}

	public void ResetPosition(Tile[][] array) {
		array[xPosition][yPosition].occupant = null;
		array[xPosition][yPosition].isOccupiedByEnemy = false;
	}

	public void StartTurn() {
		//print("ENEMY");
		End();
	}

	public bool Move() {

		return true;
	}

	public bool Attack() {

		return true;
	}

	public bool End() {
		turn = false;
		battleController.whoIsActive++;

		if (battleController.whoIsActive >= battleController.array.Length) {
			battleController.whoIsActive = 0;
			print(battleController.whoIsActive);
		}

		battleController.SetActiveUnit(battleController.whoIsActive);
		//print("ENEMY " + battleController.whoIsActive);

		this.gameObject.GetComponent<Renderer>().material.color = color;
		return true;
	}

	public void TakeHPDamage(int damage, int reduction) {
		int damageToBeTaken = damage - reduction;

		if (damageToBeTaken <= 0) {
			damageToBeTaken = 1;
		}

		if (damage >= HP) {
			HP -= damageToBeTaken;
		} else {
			HP -= damageToBeTaken;
			visibleHP.text = HP.ToString();
		}

	}
}
