using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Tile : MonoBehaviour {

	public int height, distance = 1, xCoordinate, yCoordinate;
	public string typeOfTile;
	public bool isOccupiedByAlly, isOccupiedByEnemy;
	public GameObject occupant;
	public Camera camera;
	public Image charPanel;
	public Text characterName, characterClass, classLevel, life, lifeMax,
				characterResourceName, characterResource, characterResourceMax;
	public bool canBeClicked;
	public Color color;
	public GameObject plane;
	public BattleController battleController;

	void Awake() {
		color = this.gameObject.GetComponent<Renderer>().material.color;
	}

	// Use this for initialization
	void OnMouseUp () {
		Character test = battleController.GetCurrentActiveUnit();

		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (InterfaceController.currentCameraState) {
				//print(xCoordinate + "," + yCoordinate);
				camera.transform.position = new Vector3(this.transform.position.x, 30, this.transform.position.z);
				if (occupant != null) {
					if (occupant.GetComponent<Character>()) {
						occupant.GetComponent<Character>().interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
						charPanel.color = new Color32(0, 142, 13, 255);
						characterName.text = occupant.GetComponent<Character>().characterName;
						characterClass.text = occupant.GetComponent<Character>().characterClass.name.ToString();
						classLevel.text = "NV " + occupant.GetComponent<Character>().characterClass.level.ToString();
						life.text = occupant.GetComponent<Character>().currentLife.ToString();
						lifeMax.text = occupant.GetComponent<Character>().totalCharacterLife.ToString();
						if (occupant.GetComponent<Character>().characterClass.bonusFaith != 0) {
							characterResourceName.text = "FE";
							characterResource.text = occupant.GetComponent<Character>().currentResource.ToString();
							characterResourceMax.text = occupant.GetComponent<Character>().totalCharacterFaith.ToString();
						}
						if (occupant.GetComponent<Character>().characterClass.bonusFury != 0) {
							characterResourceName.text = "FR";
							characterResource.text = occupant.GetComponent<Character>().currentResource.ToString();
							characterResourceMax.text = occupant.GetComponent<Character>().totalCharacterFury.ToString();
						}
						if (occupant.GetComponent<Character>().characterClass.bonusPersistance != 0) {
							characterResourceName.text = "PR";
							characterResource.text = occupant.GetComponent<Character>().currentResource.ToString();
							characterResourceMax.text = occupant.GetComponent<Character>().totalCharacterPersistance.ToString();
						}
					}
					if (occupant.GetComponent<Enemy>()) {
						occupant.GetComponent<Enemy>().interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
						charPanel.color = new Color32(9, 66, 94, 255);
						characterName.text = occupant.GetComponent<Enemy>().name;
						characterClass.text = occupant.GetComponent<Enemy>().className;
						classLevel.text = "NV " + occupant.GetComponent<Enemy>().level;
						life.text = occupant.GetComponent<Enemy>().HP.ToString();
						lifeMax.text = occupant.GetComponent<Enemy>().HPMax.ToString();
						characterResourceName.text = "--";
						characterResource.text = "--";
						characterResourceMax.text = "--";
					}
				} else {
					// Aqui é no caso do tile estar vazio
					Character c = battleController.GetCurrentActiveUnit();
					c.interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", false);
				}
				
				if (canBeClicked) {
					if (test.attacking) {
						// FAZER UM PROPRIO PRO ATAQUE
						
						test.interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
						test.ExecuteAttack(this, xCoordinate,yCoordinate);
						//test.targetXPosition = xCoordinate;
						//test.targetYPosition = yCoordinate;
					}
					if (test.moving) {
						test.targetXPosition = xCoordinate;
						test.targetYPosition = yCoordinate;
						test.ExecuteMovement(this, xCoordinate, yCoordinate);
					}
					//print(test);
				}
			}
		}
	}
}
