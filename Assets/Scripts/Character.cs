using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public string publicCharacterName;
	public Sprite characterFace;
	
	public CharacterClass characterClass { get; set; }
	public Text description,
		visibleName, visibleClass,
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
		hudCurrentLife, hudCurrentResource, hudMaxLife, hudMaxResource;
		
	private GameObject characterGameObject;
	private Canvas canvas;
	private bool isShowingActionMenu;
	public static string  characterName = "Zé Mané Ipsum";		// Nome
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
	public int totalCharacterReach { get; set; }	

	public int	xPosition, yPosition;

	private Desempregado desempregado = new Desempregado();
		private Diacono diacono;
			private Padre padre;
			private Monge monge;
			private Santo santo;
		private Cacador cacador;
			private Atirador atirador;
			private Matador matador;
		private Campones campones;
			private Lenhador lenhador;
				private Carpinteiro carpinteiro;
			private Operario operario;
				private Pedreiro pedreiro;
			private Amolador amolador;
				private Barbeiro barbeiro;
		private Fazendeiro fazendeiro;
			private Padeiro padeiro;
			private Costureiro costureiro;
		private Carroceiro carroceiro;
			private Cavaleiro cavaleiro;
			private Mecanico mecanico;


	

	// Use this for initialization
	void Awake () {
		//menu = new Menu(this);
		characterGameObject = this.gameObject;
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		characterClass = desempregado;
		RefreshStatusValues();
		RefreshStatusText();

		diacono = new Diacono(desempregado);
			padre = new Padre(diacono);
			monge = new Monge(diacono);
			santo = new Santo(diacono);
		cacador = new Cacador(desempregado);
			atirador = new Atirador(cacador);
			matador = new Matador(cacador);
		campones = new Campones();//desempregado);
			lenhador = new Lenhador();//campones);
				carpinteiro = new Carpinteiro();//lenhador);
			operario = new Operario();//campones);
				pedreiro = new Pedreiro();//operario);
			amolador = new Amolador();//campones);
				barbeiro = new Barbeiro();//amolador);
		fazendeiro = new Fazendeiro();//desempregado);
			padeiro = new Padeiro();//fazendeiro);
			costureiro = new Costureiro();//fazendeiro);
		carroceiro = new Carroceiro();//desempregado);
			cavaleiro = new Cavaleiro();//carroceiro);
			mecanico = new Mecanico();//carroceiro);
		
		//ShowMovementRange(Maps.map01, xPosition, yPosition);
		//ShowAttackRange(Maps.map01, xPosition, yPosition, "Sword");
		//ExecuteMovement(CharacterClassDatabase.ShowMovementRange(Maps.map01, xPosition, yPosition), xPosition + move, yPosition);
	}

	// Update is called once per frame
	void FixedUpdate () {


	}

	public void RefreshStatusText() {
		description.text = "";
		visibleName.text = characterName;
		visibleClass.text = characterClass.name + ", Nível " + characterClass.level;
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
		totalConstitution.text = totalConstitution.ToString();
		baseWisdom.text = characterWisdom.ToString();
		classWisdom.text = characterClass.bonusWisdom.ToString();
		totalWisdom.text = totalCharacterWisdom.ToString();
		baseLife.text = characterLife.ToString();
		classLife.text = characterClass.bonusLife.ToString();
		totalLife.text = totalCharacterLife.ToString();
		hudMaxLife.text = totalLife.ToString();
		if (characterClass.bonusFaith !=0) {
			resourceName.text = "FÉ";
			baseResource.text = "0";
			classResource.text = characterClass.bonusFaith.ToString();
			totalResource.text = classResource.text;
			hudMaxResource.text = totalResource.ToString();
		}
		if (characterClass.bonusFury != 0) {
			resourceName.text = "FUR";
			baseResource.text = "0";
			classResource.text = characterClass.bonusFury.ToString();
			totalResource.text = classResource.text;
			hudMaxResource.text = totalResource.ToString();
		}
		if (characterClass.bonusPersistance != 0) {
			resourceName.text = "PER";
			baseResource.text = "0";
			classResource.text = characterClass.bonusPersistance.ToString();
			totalResource.text = classResource.text;
			hudMaxResource.text = totalResource.ToString();
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
		visibleMove.text = "Movimentação: "+characterMove.ToString();
		visibleJump.text = "Pulo: " + characterJump.ToString();
		visibleReach.text = "Alcance: " + characterReach.ToString();
		visibleTurnCooldown.text = "Iniciativa: " + turnCooldown.ToString();
	}

	public void RefreshStatusValues() {
		totalCharacterAgility = characterAgility + characterClass.bonusAgility;
		//totalCharacterAttack = characterAttack + characterClass.bonusAttack + weapon.attack;
		totalCharacterConstitution = characterConstitution + characterClass.bonusConstitution;
		totalCharacterDexterity = characterDexterity + characterClass.bonusDexterity;
		totalCharacterFaith = characterFaith + characterClass.bonusFaith;
		totalCharacterFury = characterFury + characterClass.bonusFury;
		totalCharacterJump = characterJump + characterClass.bonusJump;
		totalCharacterLife = characterLife + characterClass.bonusLife;
		totalCharacterMove = characterMove + characterClass.bonusMove;
		totalCharacterPersistance = characterPersistance + characterClass.bonusPersistance;
		//totalCharacterReach = characterReach + weapon.reach;
		//totalCharacterResBlunt = characterResBlunt + head.resBlunt + armor.resBlunt + feet.resBlunt;
		//totalCharacterResCut = characterResCut + head.resCut + armor.resCut + feet.resCut;
		//totalCharacterResMoral = characterResMoral + head.resMoral + armor.resMoral + feet.resMoral;
		//totalCharacterResPierce = characterResPierce + head.resPierce + armor.resPierce + feet.resPierce;
		totalCharacterStrength = characterStrength + characterClass.bonusStrength;
		totalCharacterWisdom = characterWisdom + characterClass.bonusWisdom;
	}


	public void ChangeClass() {

	}

	public void ShowChangeClassMenu() {

	}

	public void ShowStatus() {

	}

	public void ShowLearntSkills() {

	}

	public void ExecuteAttack() {

	}

	public void SetPosition(Tile[][] array, int positionX, int positionY) {
		array[positionX][positionY].occupant = this.gameObject;
	}

	public List<Tile> ShowMovementRange(Tile[][] array, int positionX, int positionY) {

		xPosition = positionX;
		yPosition = positionY;

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighbours(array, positionX, positionY));
		// Seta a posição inicial como impassável
		array[positionX][positionY].occupant = this.gameObject;
		characterGameObject.gameObject.transform.position = new Vector3(array[positionX][positionY].gameObject.transform.position.x
															,array[positionX][positionY].gameObject.transform.position.y + array[positionX][positionY].gameObject.transform.localScale.y + 0.5f
															,array[positionX][positionY].gameObject.transform.position.z);

		// Lista que possuirá todos os tiles cujo o personagem pode passar por
		List<Tile> movementRange = new List<Tile>();
			
		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < characterMove; i++) {
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
						differenceOfHeigth <= characterJump) {
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
				tile.gameObject.GetComponent<Renderer>().material.color = Color.blue;
			}
				
		}

		// Retorna os tiles pelo qual o personagem pode chegar à
		return movementRange;
	}

	public List<Tile> ShowAttackRange(Tile[][] array, int positionX, int positionY, string typeOfWeapon) {
		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighbours(array, positionX, positionY));
		// Seta a posição inicial como impassável
		array[positionX][positionY].occupant = this.gameObject;
			

		// Lista que possuirá todos os tiles cujo o personagem pode atacar
		List<Tile> autoAttackRange = new List<Tile>();

		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < characterReach; i++) {
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
				List<Tile> checkedNeighbours = CheckNeighbours(array, tile.xCoordinate, tile.yCoordinate);
				// foreach que adiciona as posições vizinhas contanto que não haja iguais
				foreach (Tile neighbours in checkedNeighbours) {
					if (neighbours != nextToVisit.Contains(neighbours)) {
						nextToVisit.Add(neighbours);
					}
				}
			}
		}

		// Seta os tiles de ataque para serem retornados
		autoAttackRange = visitedPositions;

		// Pinta de vermelho tudo o que pode ser atacado
		foreach (Tile tile in autoAttackRange) {
			if (tile.occupant == null) {
				tile.gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
		}

		// Retorna os tiles pelo qual o personagem pode atacar
		return autoAttackRange;
	}

	public void ExecuteMovement(List<Tile> movementRange, int targetPositionX, int targetPositionY) {

		//print(movementRange.Count);
		foreach (Tile item in movementRange) {
			//print(item);
		}


		foreach (Tile tile in movementRange) {
			if (tile.name == targetPositionX+","+targetPositionY) {
				tile.gameObject.GetComponent<Renderer>().material.color = Color.black;
				characterGameObject.transform.position = tile.transform.position;
				characterGameObject.transform.position = new Vector3(tile.transform.position.x 
					,(tile.gameObject.transform.position.y + tile.gameObject.transform.localScale.y + 1),
						tile.transform.position.z);
				break;
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
						((positionYAuxiliary < 0) || (positionYAuxiliary >= array.Length))) {
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
					if (array[positionXAuxiliary][positionY].occupant == null &&
						differenceOfHeigth1 <= characterJump) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (array[positionX][positionYAuxiliary].occupant == null &&
						differenceOfHeigth2 <= characterJump) {
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
}