using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public string publicCharacterName;
	public bool turn = false, moving, attacking, moved, attacked;
	public Button buttonMove, buttonAttack, buttonStatus, buttonEnd, confirm, cancel;  // buttonSkill, buttonItem,
	public GameObject camera1, camera2, statusScreen, descriptionGameObject;
	public BattleController battleController;
	public InterfaceController interfaceController;
	public Color color;
	public CharacterClass characterClass;
	public int damageCaused;
	public Image charFace, statusCharFace, charPanel;
	public GameObject menuSkills, mSkill1, mSkill2, mSkill3, mSkill4;
	public Text mSkill1Text, mSkill2Text, mSkill3Text, mSkill4Text;

	public Text description,
		visibleName, visibleNameHUD, visibleClass, visibleClassHUD, visibleClassLevelHUD,
		baseStrength, classStrength, totalStrength,
		baseDexterity, classDexterity, totalDexterity,
		baseAgility, classAgility, totalAgility,
		baseConstitution, classConstitution, totalConstitution,
		baseWisdom, classWisdom, totalWisdom,
		baseLife, classLife, totalLife,
		resourceName, baseResource, classResource, totalResource,
		baseAttack, classAttack, totalAttack,
		baseResCut, classResCut, totalResCut,
		baseResPierce, classResPierce, totalResPierce,
		baseResBlunt, classResBlunt, totalResBlunt,
		baseResMoral, classResMoral, totalResMoral, 
		visibleMove, visibleJump, visibleReach, visibleTurnCooldown,
		currentLifeHUD, currentResourceHUD, maxLifeHUD, maxResourceHUD, HPHUD, MPHUD,
		weapon, head, body, feet;
		
	private GameObject characterGameObject;
	private Canvas canvas;
	private bool isShowingActionMenu;
	public string characterName;				// Nome
	public int	characterStrength = 5,			// Aumenta o ataque com Cajados, Martelos, Machados e Lanças. Reduz a penalidade de peso do equipamento. Reduz o Tempo de Recarga de habilidades de Persistência.
				characterDexterity = 5,			// Aumenta o ataque com Espadas, Pistolas, Rifles e Arcos. Aumenta a taxa de acerto. Reduz Tempo de Recarga de habilidades de Fé.
				characterAgility = 5,			// Reduz o Turn Cooldown. Aumenta a taxa de esquiva. Reduz o Tempo de Recarga de habilidades de Fúria.
				characterConstitution = 5,		// Aumenta efeitos de recuperação. Gera Persistência numa taxa de 1:1. Gera Fúria numa taxa de 1:1.
				characterWisdom = 5,			// Para uso com habilidades especiais.
				characterLife = 100,			// HP, se chegar a 0, já era.
				characterFaith,					// 
				characterFury,					// 
				characterPersistance,			// 
				characterAttack = 5,			// Determina o dano que será causado por ataques normais e habilidades.
				characterResCut = 1,			// Reduz dano de corte numa taxa de 1:1.
				characterResPierce = 1,			// Reduz dano de perfuração numa taxa de 1:1.
				characterResBlunt = 1,			// Reduz dano de concussão numa taxa de 1:1.
				characterResMoral = 1,			// Reduz efeitos morais.
				characterMove = 3,				// Movimentação
				characterJump = 2,				// Pulo
				characterReach = 1,				// Alcance. Pré-determinado como 1, será alterado com a arma utilizada.
				turnCooldown;					// Determina a ordem de ação na batalha, quanto menor for, maior a prioridade do personagem.
	public int currentLife, currentResource;

	public int totalCharacterStrength {get;set;}
	public int totalCharacterDexterity {get;set;}	
	public int totalCharacterAgility {get;set;}	
	public int totalCharacterConstitution {get;set;}
	public int totalCharacterWisdom {get;set;}		
	public int totalCharacterLife {get;set;}		
	public int totalCharacterFaith {get;set;}		
	public int totalCharacterFury {get;set;}		
	public int totalCharacterPersistance {get;set;}	
	public int totalCharacterAttack {get;set;}		
	public int totalCharacterResCut {get;set;}		
	public int totalCharacterResPierce {get;set;}	
	public int totalCharacterResBlunt {get;set;}	
	public int totalCharacterResMoral {get;set;}
	public int totalCharacterMove {get;set;}
	public int totalCharacterJump {get;set;}
	public int totalCharacterReach {get;set;}

	public int	xPosition, yPosition, targetXPosition, targetYPosition;

	public List<Tile> targetArea;

	private string[] nameArray = new string[] { "José", "João", "Márcio", "Álvaro", "Bartolomeu", "Horácio", "Irineu", "Jairo", "Moacir", "Plínio", "Saulo" };
	private string[] surnameArray = new string[] { " da Silva", " Souza", " de Oliveira Quatro", " Grütner", " Bortoluzzi", " Spaghetti", " Peperonni", " Paparetutti" };
	
	public CharacterClass[] classArray;

	public Tile currentTile;

	// Use this for initialization
	void Awake () {

		targetXPosition = -1;
		targetYPosition = -1;

		

		characterAttack = Random.Range(10, 25);
		characterLife = Random.Range(100, 125);

		color = this.gameObject.GetComponent<Renderer>().material.color;

		string pickedName = nameArray[Random.Range(0, nameArray.Length)] + surnameArray[Random.Range(0, surnameArray.Length)];
		characterName = pickedName;

		characterClass = classArray[Random.Range(0, classArray.Length)];
		characterClass.exp = 999999;
		characterClass.PickEquipments();
		for (int i = 0; i < Random.Range(50, 70); i++) {
			characterClass.LevelUp();
		}

		RefreshHPMP();

		currentLife = totalCharacterLife;
		if (characterClass.bonusFaith != 0) {
			currentResource = characterClass.bonusFaith;
		}
		if (characterClass.bonusPersistance != 0) {
			currentResource = characterClass.bonusPersistance;
		}
		if (characterClass.bonusFury != 0) {
			currentResource = characterClass.bonusFury;
		}

		characterGameObject = this.gameObject;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = characterClass.charBody;
				
		RefreshStatusValues();
		RefreshStatusText();
		}

	// Update is called once per frame
	void FixedUpdate () {
		if (InterfaceController.currentCameraState) {
			this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
		} else {
			this.gameObject.transform.LookAt(camera2.transform.position, Vector3.up);
		}
		if (currentLife <= 0) {
			Die();
		}
	}

	public int TakeHPDamage(int damage, int reduction) {
		int damageToBeTaken = (int)((damage - reduction) * Random.Range(0.8f, 1f));

		if (damageToBeTaken <= 0) {
			damageToBeTaken = 1;
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		}
		
		if (damage >= currentLife) {
			currentLife -= damageToBeTaken;
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		} else {
			currentLife -= damageToBeTaken;
			currentLifeHUD.text = currentLife.ToString();
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		}
	}

	public void Die() {
		this.gameObject.SetActive(false);
		Maps.map01[xPosition][yPosition].occupant = null;
		Maps.map01[xPosition][yPosition].isOccupiedByAlly = false;
	}

	public void RefreshHPMP(){
		if (characterClass.bonusFaith != 0) {
			totalCharacterFaith = characterFaith + characterClass.bonusFaith;			
		}
		if (characterClass.bonusFury != 0) {
			totalCharacterFaith = characterFaith + characterClass.bonusFury;
		}
		if (characterClass.bonusPersistance != 0) {
			totalCharacterFaith = characterFaith + characterClass.bonusPersistance;
		}
		totalCharacterJump = characterJump + characterClass.bonusJump;
		totalCharacterLife = characterLife + characterClass.bonusLife;
	}

	public void RefreshStatusText() {
		description.text = "";
		visibleName.text = characterName;
		visibleNameHUD.text = characterName;
		visibleClass.text = characterClass.name + ", Nível " + characterClass.level;
		visibleClassHUD.text = characterClass.name;
		visibleClassLevelHUD.text = "NV " + characterClass.level;
		baseStrength.text = characterStrength.ToString();
		classStrength.text = characterClass.bonusStrength.ToString();
		totalStrength.text = totalCharacterStrength.ToString();
		baseDexterity.text = characterDexterity.ToString();
		classDexterity.text = characterClass.bonusDexterity.ToString();
		totalDexterity.text = totalCharacterDexterity.ToString();
		baseAgility.text = characterAgility.ToString();
		classAgility.text = characterClass.bonusAgility.ToString();
		totalAgility.text = totalCharacterAgility.ToString();
		baseConstitution.text = characterConstitution.ToString();
		classConstitution.text = characterClass.bonusConstitution.ToString();
		totalConstitution.text = totalCharacterConstitution.ToString();
		baseWisdom.text = characterWisdom.ToString();
		classWisdom.text = characterClass.bonusWisdom.ToString();
		totalWisdom.text = totalCharacterWisdom.ToString();
		baseLife.text = characterLife.ToString();
		classLife.text = characterClass.bonusLife.ToString();
		totalLife.text = totalCharacterLife.ToString();
		currentLifeHUD.text = currentLife.ToString();
		HPHUD.text = "PV";
		maxLifeHUD.text = totalLife.text;
		if (characterClass.bonusFaith !=0) {
			resourceName.text = "Fé";
			MPHUD.text = "FE";
			baseResource.text = "0";
			classResource.text = characterClass.bonusFaith.ToString();
			totalResource.text = classResource.text;
			currentResourceHUD.text = currentResource.ToString();
			maxResourceHUD.text = totalResource.text;
		}
		if (characterClass.bonusFury != 0) {
			resourceName.text = "Fúria";
			MPHUD.text = "FR";
			baseResource.text = "0";
			classResource.text = characterClass.bonusFury.ToString();
			totalResource.text = classResource.text;
			currentResourceHUD.text = currentResource.ToString();
			maxResourceHUD.text = totalResource.text;
		}
		if (characterClass.bonusPersistance != 0) {
			resourceName.text = "Persistência";
			MPHUD.text = "PR";
			baseResource.text = "0";
			classResource.text = characterClass.bonusPersistance.ToString();
			totalResource.text = classResource.text;
			currentResourceHUD.text = currentResource.ToString();
			maxResourceHUD.text = totalResource.text;
		}
		baseAttack.text = characterAttack.ToString();
		classAttack.text = characterClass.bonusAttack.ToString();
		totalAttack.text = totalCharacterAttack.ToString();
		baseResCut.text = characterResCut.ToString();
		classResCut.text = characterClass.bonusResCut.ToString();
		totalResCut.text = totalCharacterResCut.ToString();
		baseResPierce.text = characterResPierce.ToString();
		classResPierce.text = characterClass.bonusResPierce.ToString();
		totalResPierce.text = totalCharacterResPierce.ToString();
		baseResBlunt.text = characterResBlunt.ToString();
		classResBlunt.text = characterClass.bonusResBlunt.ToString();
		totalResBlunt.text = totalCharacterResBlunt.ToString();
		baseResMoral.text = characterResMoral.ToString();
		classResMoral.text = characterClass.bonusResMoral.ToString();
		totalResMoral.text = totalCharacterResMoral.ToString();
		visibleMove.text = "Movimentação: " + totalCharacterMove.ToString();
		visibleJump.text = "Pulo: " + totalCharacterJump.ToString();
		visibleReach.text = "Alcance: " + totalCharacterReach.ToString();
		visibleTurnCooldown.text = "Iniciativa: " + turnCooldown.ToString();
		weapon.text = characterClass.weapon;
		head.text = characterClass.head;
		body.text = characterClass.body;
		feet.text = characterClass.feet;
	}

	public void RefreshStatusValues() {
		totalCharacterAgility = characterAgility + characterClass.bonusAgility;
		totalCharacterAttack = characterAttack + characterClass.bonusAttack;// + weapon.attack;
		totalCharacterConstitution = characterConstitution + characterClass.bonusConstitution;
		totalCharacterDexterity = characterDexterity + characterClass.bonusDexterity;
		totalCharacterFaith = characterFaith + characterClass.bonusFaith;
		totalCharacterFury = characterFury + characterClass.bonusFury;
		totalCharacterJump = characterJump + characterClass.bonusJump;
		totalCharacterLife = characterLife + characterClass.bonusLife;
		totalCharacterMove = characterMove + characterClass.bonusMove;
		totalCharacterPersistance = characterPersistance + characterClass.bonusPersistance;
		totalCharacterReach = characterReach + characterClass.bonusReach; // + WEAPON REACH
		totalCharacterResBlunt = characterResBlunt + characterClass.bonusResBlunt; // + head.resBlunt + armor.resBlunt + feet.resBlunt;
		totalCharacterResCut = characterResCut + characterClass.bonusResCut;// +head.resCut + armor.resCut + feet.resCut;
		totalCharacterResMoral = characterResMoral + characterClass.bonusResMoral;// +head.resMoral + armor.resMoral + feet.resMoral;
		totalCharacterResPierce = characterResPierce + characterClass.bonusResPierce;// +head.resPierce + armor.resPierce + feet.resPierce;
		totalCharacterStrength = characterStrength +characterClass.bonusStrength;
		totalCharacterWisdom = characterWisdom +characterClass.bonusWisdom;
	}

	public void StartTurn() {

		print(battleController.whoIsActive);

		charFace.sprite = characterClass.charFace;
		statusCharFace.sprite = characterClass.charFace;

		this.gameObject.GetComponent<Renderer>().material.color = Color.green;
		charPanel.color = new Color32(0, 142, 13, 255);

		interfaceController.CursorAim(this.gameObject);	

		RefreshHPMP();
		RefreshStatusText();
		RefreshStatusValues();

		UnityEngine.Events.UnityAction moveSelf = () => {
			if (moving) {
				HideRange(ShowMovementRange(Maps.map01, xPosition, yPosition));
				confirm.onClick.RemoveAllListeners();
				cancel.onClick.RemoveAllListeners();
				ShowMenuButtons();
				targetXPosition = -1;
				targetYPosition = -1;
				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
			} else {
				interfaceController.ForceCamera();
				interfaceController.CursorAim(this.gameObject);
				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", false);
				ShowMovementRange(Maps.map01, xPosition, yPosition);
			}};
		UnityEngine.Events.UnityAction attackSelf = () => {
			if (attacking) {
				HideRange(ShowAttackRange(Maps.map01, xPosition, yPosition));
				confirm.onClick.RemoveAllListeners();
				cancel.onClick.RemoveAllListeners();
				ShowMenuButtons();
				targetXPosition = -1;
				targetYPosition = -1;
				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
			} else {
				interfaceController.ForceCamera();
				interfaceController.CursorAim(this.gameObject);
				ShowAttackRange(Maps.map01, xPosition, yPosition);
				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", false);
			}};
		UnityEngine.Events.UnityAction skillSelf = () => {
			if (attacking) {

				confirm.onClick.RemoveAllListeners();
				cancel.onClick.RemoveAllListeners();
				HideMenuSkills();
				ShowMenuButtons();
				targetXPosition = -1;
				targetYPosition = -1;
				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
			} else {
				interfaceController.ForceCamera();
				interfaceController.CursorAim(this.gameObject);
				ShowMenuSkills();

				interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", false);
			}
		};
		UnityEngine.Events.UnityAction itemSelf = () => {  };
		UnityEngine.Events.UnityAction statusSelf = () => {
			AlterStatus();
			interfaceController.CursorAim(this.gameObject);
		};
		UnityEngine.Events.UnityAction endTurn = () => { EndTurn(); };

		buttonMove.GetComponent<Button>().interactable = true;
		buttonAttack.GetComponent<Button>().interactable = true;
		//buttonSkill.GetComponent<Button>().interactable = true;
		//buttonItem.GetComponent<Button>().interactable = true;
		buttonStatus.GetComponent<Button>().interactable = true;
		buttonEnd.GetComponent<Button>().interactable = true;

		buttonMove.onClick.AddListener(moveSelf);
		buttonAttack.onClick.AddListener(attackSelf);
		//buttonSkill.onClick.AddListener(skillSelf);
		//buttonItem.onClick.AddListener(itemSelf);
		buttonStatus.onClick.AddListener(statusSelf);
		buttonEnd.onClick.AddListener(endTurn);
	}

	public bool EndTurn() {

		interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);

		buttonMove.GetComponent<Button>().interactable = false;
		buttonAttack.GetComponent<Button>().interactable = false;
		//buttonSkill.GetComponent<Button>().interactable = false;
		//buttonItem.GetComponent<Button>().interactable = false;
		buttonStatus.GetComponent<Button>().interactable = false;
		buttonEnd.GetComponent<Button>().interactable = false;
		menuSkills.SetActive(false);

		buttonMove.onClick.RemoveAllListeners();
		buttonAttack.onClick.RemoveAllListeners();
		//buttonSkill.onClick.RemoveAllListeners();
		//buttonItem.onClick.RemoveAllListeners();
		buttonStatus.onClick.RemoveAllListeners();
		buttonEnd.onClick.RemoveAllListeners();
		turn = false;
		
		battleController.whoIsActive++;

		if (battleController.whoIsActive >= battleController.array.Length) {
			battleController.whoIsActive = 0;
			print(battleController.whoIsActive);
		}

		battleController.SetActiveUnit(battleController.whoIsActive);

		moved = false;
		attacked = false;

		this.gameObject.GetComponent<Renderer>().material.color = color;
		return true;
	}

	public void AlterStatus() {
		statusScreen.gameObject.SetActive(!statusScreen.activeSelf);

		if (moved == false) {
			buttonMove.interactable = !buttonMove.interactable;			
		}
		if (attacked == false) {
			buttonAttack.interactable = !buttonAttack.interactable;
		}
		//buttonSkill.interactable = !buttonSkill.interactable;
		//buttonItem.interactable = !buttonItem.interactable;
		buttonEnd.interactable = !buttonEnd.interactable;
	}

	public void SetPosition(Tile[][] array, int positionX, int positionY) {
		xPosition = positionX;
		yPosition = positionY;
		array[positionX][positionY].occupant = this.gameObject;
		array[xPosition][yPosition].isOccupiedByAlly = true;
		this.gameObject.transform.position = new Vector3(array[positionX][positionY].gameObject.transform.position.x
															, array[positionX][positionY].gameObject.transform.localScale.y
															, array[positionX][positionY].gameObject.transform.position.z);
		
	}

	public void ResetPosition(Tile[][] array) {
		array[xPosition][yPosition].occupant = null;
		array[xPosition][yPosition].isOccupiedByAlly = false;
	}

	public List<Tile> ShowMovementRange(Tile[][] array, int positionX, int positionY) {

		moving = true;

		buttonAttack.interactable = false;
		//buttonSkill.interactable = false;
		//buttonItem.interactable = false;
		buttonStatus.interactable = false;
		buttonEnd.interactable = false;

		xPosition = positionX;
		yPosition = positionY;

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighbours(array, positionX, positionY));
		// Seta a posição inicial como impassável
		array[positionX][positionY].occupant = this.gameObject;
		this.gameObject.transform.position = new Vector3(array[positionX][positionY].gameObject.transform.position.x
															, array[positionX][positionY].gameObject.transform.localScale.y
															, array[positionX][positionY].gameObject.transform.position.z);

		// Lista que possuirá todos os tiles cujo o personagem pode passar por
		List<Tile> movementRange = new List<Tile>();
			
		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < totalCharacterMove; i++) {
			// Lista Borda que possui todos os tiles pelos quais o personagem pode passar por, conforme
			// seu status Move e Jump.
			List<Tile> border = new List<Tile>();

			foreach (Tile tile in nextToVisit) {
				// Lista de posições vizinhas ao tile
				List<Tile> neighbours = CheckNeighbours(array, tile.xCoordinate, tile.yCoordinate);
					
				// Iteração sobre as posições vizinhas
				foreach (Tile neighbour in neighbours) {
					// Diferença de altura entre o tile e um de seus vizinhos
					int differenceOfHeigth = tile.height - neighbour.height;
					// Só pra manter a diferença positiva
					if (differenceOfHeigth < 0) {
						differenceOfHeigth = differenceOfHeigth * (-1);
					}
					// Se a borda não ter o dito vizinho, adiciona o vizinho à ela
					if (neighbour != border.Contains(neighbour) &&
						neighbour == null &&
						differenceOfHeigth <= totalCharacterJump) {
						//print("Tile Analisado (" + tile + ") / Vizinho Analisado (" + neighbour + ") / Diferença de altura = " + differenceOfHeigth);
						border.Add(neighbour);
					} 
				}

				/* Pinta a borda de Magenta
				foreach (Tile item in border) {
					item.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
				}*/

				// Se a visitedPositions não tiver o tile, adiciona ele a ela
				if (tile != visitedPositions.Contains(tile)) {
					visitedPositions.Add(tile);
				}
					
			}
			foreach (Tile tile in visitedPositions) {
				// Lista de tiles vizinhos e checados
				List<Tile> checkedNeighbours = CheckNeighbours(array, tile.xCoordinate, tile.yCoordinate);
				// foreach que adiciona as posições vizinhas contanto que não haja iguais
				foreach (Tile neighbours in checkedNeighbours) {
					if (neighbours != nextToVisit.Contains(neighbours)) {
						nextToVisit.Add(neighbours);
					}
				}
			}
		}

		// Seta os tiles de movimentação para serem retornados
		movementRange = visitedPositions;

		// Pinta de azul tudo o que pode ser movimentado sobre
		foreach (Tile tile in movementRange) {
			if (tile.occupant == null) {
				tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.blue;
				tile.canBeClicked = true;
			}
				
		}

		// Retorna os tiles pelo qual o personagem pode chegar à
		return movementRange;		
	}

	public List<Tile> ShowAttackRange(Tile[][] array, int positionX, int positionY) {

		attacking = true;

		buttonMove.interactable = false;
		//buttonSkill.interactable = false;
		//buttonItem.interactable = false;
		buttonStatus.interactable = false;
		buttonEnd.interactable = false;

		xPosition = positionX;
		yPosition = positionY;

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighboursAttack(array, positionX, positionY));

		// Lista que possuirá todos os tiles cujo o personagem pode atacar
		List<Tile> autoAttackRange = new List<Tile>();

		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < totalCharacterReach; i++) {
			// Lista Borda que possui todos os tiles pelos quais o personagem pode passar por, conforme
			// seu status Move e Jump.
			List<Tile> border = new List<Tile>();

			foreach (Tile tile in nextToVisit) {
				// Lista de posições vizinhas ao tile
				List<Tile> neighbours = CheckNeighboursAttack(array, tile.xCoordinate, tile.yCoordinate);

				// Iteração sobre as posições vizinhas
				foreach (Tile neighbour in neighbours) {
					// Diferença de altura entre o tile e um de seus vizinhos
					int differenceOfHeigth = tile.height - neighbour.height;
					// Só pra manter a diferença positiva
					if (differenceOfHeigth < 0) {
						differenceOfHeigth = differenceOfHeigth * (-1);
					}
					// Se a borda não ter o dito vizinho, adiciona o vizinho à ela
					if (neighbour != border.Contains(neighbour) &&
						differenceOfHeigth <= characterReach) {
						//print("Tile Analisado (" + tile + ") / Vizinho Analisado (" + neighbour + ") / Diferença de altura = " + differenceOfHeigth);
						border.Add(neighbour);
					}
				}
				// Se a visitedPositions não tiver o tile, adiciona ele a ela
				if (tile != visitedPositions.Contains(tile)) {
					visitedPositions.Add(tile);
				}

			}
			foreach (Tile tile in visitedPositions) {
				// Lista de tiles vizinhos e checados
				List<Tile> checkedNeighbours = CheckNeighboursAttack(array, tile.xCoordinate, tile.yCoordinate);
				// foreach que adiciona as posições vizinhas contanto que não haja iguais
				foreach (Tile neighbours in checkedNeighbours) {
					if (neighbours != array[xPosition][yPosition] &&
						neighbours != nextToVisit.Contains(neighbours)) {
						nextToVisit.Add(neighbours);
					}
				}
			}
		}

		// Seta os tiles de ataque para serem retornados
		autoAttackRange = visitedPositions;

		// Pinta de vermelho tudo o que pode ser atacado
		foreach (Tile tile in autoAttackRange) {
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.red;
			if (tile.occupant != null) {
				tile.canBeClicked = true;
				tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}

		// Retorna os tiles pelo qual o personagem pode atacar
		return autoAttackRange;
	}

	public void ShowMenuSkills() {
		attacking = true;
		menuSkills.SetActive(true);

		buttonMove.GetComponent<Button>().interactable = false;
		buttonAttack.GetComponent<Button>().interactable = false;
		//buttonItem.GetComponent<Button>().interactable = false;
		buttonStatus.GetComponent<Button>().interactable = false;
		buttonEnd.GetComponent<Button>().interactable = false;
	}

	public void HideMenuSkills() {
		attacking = false;
		menuSkills.SetActive(false);

		buttonMove.GetComponent<Button>().interactable = true;
		buttonAttack.GetComponent<Button>().interactable = true;
		//buttonItem.GetComponent<Button>().interactable = true;
		buttonStatus.GetComponent<Button>().interactable = true;
		buttonEnd.GetComponent<Button>().interactable = true;
	}

	public void HideRange(List<Tile> list) {

		moving = false;
		attacking = false;

		foreach (Tile tile in list) {
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = tile.color;
			tile.canBeClicked = false;
		}
	}

	public void ExecuteMovement(Tile target, int targetPositionX, int targetPositionY) {
		if (targetPositionX >= 0 && targetPositionY >= 0) {
			List<Tile> moveRange = ShowMovementRange(Maps.map01, xPosition, yPosition);
			interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);
			// TEM QUE ESVAZIAR A POSIÇÃO INICIAL - PORRA QUE NOJO DA MINHA SOLUÇÃO
			ResetPosition(Maps.map01);

			xPosition = targetPositionX;
			yPosition = targetPositionY;

			HideRange(moveRange);

			target.occupant = this.gameObject;
			target.isOccupiedByAlly = true;
			target.plane.gameObject.GetComponent<Renderer>().material.color = target.color;
			characterGameObject.transform.position = new Vector3(target.transform.position.x
											, target.gameObject.transform.localScale.y
											, target.transform.position.z);

			buttonMove.interactable = false;
			moved = true;

			RefreshHPMP();
			RefreshStatusText();
			RefreshStatusValues();
		}
	}

	public void ExecuteAttack(Tile target, int targetPositionX, int targetPositionY) {
		if (targetPositionX >= 0 && targetPositionY >= 0) {
			List<Tile> attackRange = ShowAttackRange(Maps.map01, xPosition, yPosition);

			HideRange(attackRange);

			buttonAttack.interactable = false;
			//buttonSkill.interactable = false;
			attacked = true;
			interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);

			if (target.isOccupiedByAlly) {
				Character targetAlly = target.occupant.GetComponent<Character>();
				damageCaused = targetAlly.TakeHPDamage(totalCharacterAttack, targetAlly.totalCharacterResCut);
				targetAlly.RefreshHPMP();
			}
			if (target.isOccupiedByEnemy) {
				Enemy targetEnemy = target.occupant.GetComponent<Enemy>();
				damageCaused = targetEnemy.TakeHPDamage(totalCharacterAttack, targetEnemy.armor);
			}
		}
	}

	// Métodos privados --------------------------------------------------------------------
	private List<Tile> CheckNeighbours(Tile[][] array, int positionX, int positionY) {
		// Lista que será retornada
		List<Tile> listOfAdjacentTiles = new List<Tile>();

		// fors que vão de -1 à 1 para pegar as posições adjacentes
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				// if para prevenir que a posição estipulada no método seja iterada sobre
				if (i != 0 && j != 0) {
					int positionXAuxiliary = positionX + i;
					int positionYAuxiliary = positionY + j;

					if (((positionXAuxiliary < 0) || (positionXAuxiliary >= array.Length)) ||
						((positionYAuxiliary < 0) || (positionYAuxiliary >= array[0].Length))) {
						// O continue possui lógica negada
						// Tipo, ele exclui os casos referidos no if da iteração do for
						continue;
					}

					// Diferenças de altura, cada uma para a sua condicional em questão, ambas positivas
					int differenceOfHeigth1 = array[positionX][positionY].height - array[positionXAuxiliary][positionY].height;
					int differenceOfHeigth2 = array[positionX][positionY].height - array[positionX][positionYAuxiliary].height;
					if (differenceOfHeigth1 < 0) {
						differenceOfHeigth1 = differenceOfHeigth1 * (-1);
					}
					if (differenceOfHeigth2 < 0) {
						differenceOfHeigth2 = differenceOfHeigth2 * (-1);
					}

					// Condicionais de vizinhança
					// Estão atualmente limitadas por ocupação do tile e status de pulo do personagem
					if (array[positionXAuxiliary][positionY].isOccupiedByEnemy == false &&
						differenceOfHeigth1 <= totalCharacterJump) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (array[positionX][positionYAuxiliary].isOccupiedByEnemy == false &&
						differenceOfHeigth2 <= totalCharacterJump) {
						listOfAdjacentTiles.Add(array[positionX][positionYAuxiliary]);
					}

				}
			}
		}
		return listOfAdjacentTiles;
	}

	private List<Tile> CheckNeighboursAttack(Tile[][] array, int positionX, int positionY) {
		// Lista que será retornada
		List<Tile> listOfAdjacentTiles = new List<Tile>();

		// fors que vão de -1 à 1 para pegar as posições adjacentes
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				// if para prevenir que a posição estipulada no método seja iterada sobre
				if (i != 0 && j != 0) {
					int positionXAuxiliary = positionX + i;
					int positionYAuxiliary = positionY + j;

					if (((positionXAuxiliary < 0) || (positionXAuxiliary >= array.Length)) ||
						((positionYAuxiliary < 0) || (positionYAuxiliary >= array[0].Length))) {
						// O continue possui lógica negada
						// Tipo, ele exclui os casos referidos no if da iteração do for
						continue;
					}

					// Diferenças de altura, cada uma para a sua condicional em questão, ambas positivas
					int differenceOfHeigth1 = array[positionX][positionY].height - array[positionXAuxiliary][positionY].height;
					int differenceOfHeigth2 = array[positionX][positionY].height - array[positionX][positionYAuxiliary].height;
					if (differenceOfHeigth1 < 0) {
						differenceOfHeigth1 = differenceOfHeigth1 * (-1);
					}
					if (differenceOfHeigth2 < 0) {
						differenceOfHeigth2 = differenceOfHeigth2 * (-1);
					}

					// Condicionais de vizinhança
					if (differenceOfHeigth1 <= totalCharacterReach) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (differenceOfHeigth2 <= totalCharacterReach) {
						listOfAdjacentTiles.Add(array[positionX][positionYAuxiliary]);
					}

				}
			}
		}
		return listOfAdjacentTiles;
	}

	private int DistanceOfTwoPoints(int point1X, int point1Y, int point2X, int point2Y){
		return ((point1X - point2X) + (point1Y - point2Y));
	}

	private int SumOfTheDistanceOfTwoPoints(Tile tile1, Tile tile2) {
		return tile1.distance + tile2.distance;
	}

	private void ShowMenuButtons() {
		if (moved == false) {
			buttonMove.GetComponent<Button>().interactable = true;
		} else {
			buttonMove.GetComponent<Button>().interactable = false;
		}
		if (attacked == false) {
			buttonAttack.GetComponent<Button>().interactable = true;
			//buttonSkill.GetComponent<Button>().interactable = true;
		} else {
			buttonAttack.GetComponent<Button>().interactable = false;
			//buttonSkill.GetComponent<Button>().interactable = false;
		}
		//buttonItem.GetComponent<Button>().interactable = false;
		buttonStatus.GetComponent<Button>().interactable = true;
		buttonEnd.GetComponent<Button>().interactable = true;
	}
}