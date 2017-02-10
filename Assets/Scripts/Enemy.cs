using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public string name, className;
	public int HP, MP, HPMax, MPMax, damage, armor, xPosition, yPosition, level, damageCaused, reach, move, jump;
	public bool turn = false;
	public Camera camera, camera2;
	public BattleController battleController;
	public InterfaceController interfaceController;
	public Text visibleHP, visibleName, visibleNameHUD, visibleClass, visibleClassHUD, visibleClassLevelHUD;
	public Color color;
	public Sprite charAttack, charFace, charBody;
	private string[] nameArray = new string[] { "Sargento", "Cabo", "Soldado", "Aspirante", "Tenente", "Capitão", "Major"};
	private string[] surnameArray = new string[] { " Silva", " Souza", " Vieira", " Demarchi", " Scherer", " Spaghetti", " Alves", " Alencar", " Bason", " Dutra", " Ellard", " Guerra", " Bigode", " Loch", " Macedo", " Nascimento", " Romão", " Schwartz" };
	private string[] classArray = new string[] { "Infantaria", "Fuzileiro", "Franco-Atirador", "Esp. em explosivos", "Médico", "Socorrista", "Engenheiro" };
	public Character turnTarget;

	// Use this for initialization
	void Awake () {
		string pickedName = nameArray[Random.Range(0, nameArray.Length)] + surnameArray[Random.Range(0, surnameArray.Length)];
		name = pickedName;

		jump = 2;
		move = Random.Range(2, 5);
		reach = Random.Range(1, 5);

		string pickedClass = classArray[Random.Range(0, classArray.Length)];
		className = pickedClass;

		level = Random.Range(30, 50);

		color = this.gameObject.GetComponent<Renderer>().material.color;

		HPMax = Random.Range(150, 300);
		HP = HPMax;
		MPMax = Random.Range(10, 50);
		MP = MPMax;
		damage = Random.Range(20, 45);
		armor = Random.Range(5, 10);


		SetTurnTarget();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (HP <= 0) {
			this.gameObject.SetActive(false);
			Maps.map01[xPosition][yPosition].occupant = null;
			Maps.map01[xPosition][yPosition].isOccupiedByEnemy = false;
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
		this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
		print(battleController.whoIsActive);
		
		interfaceController.ForceCamera();
		interfaceController.CursorAim(this.gameObject);
		if (turnTarget.gameObject.activeSelf == false) {
			SetTurnTarget();
		}
		StartCoroutine(Act());
	}

	private IEnumerator Act() {
		int delay = 1;
		yield return new WaitForSeconds(delay);
		float distanceToTheFoe = DistanceOfTwoPoints(xPosition, yPosition, turnTarget.xPosition, turnTarget.yPosition);
		if (distanceToTheFoe > reach || distanceToTheFoe < -reach) {
			List<Tile> moveRange = ShowMovementRange(Maps.map01, xPosition, yPosition);
			// Tem que se mexer até o ponto mais próximo do alvo
			// FAZER ESSA MERDA
			Tile tileToMoveTo = new Tile();
			foreach (var tile in moveRange) {
				float distanceToTheFoe2 = DistanceOfTwoPoints(tile.xCoordinate, tile.yCoordinate, turnTarget.xPosition, turnTarget.yPosition);
				// TIPO TEM QUE CHECAR A DISTANCIA ENTRE DOIS PONTOS
				// SE PEGAR APENAS A GRANDEZA, DÁ MERDA
				// FAZER ESSA BOSTA DIREITO
				if (distanceToTheFoe2 < distanceToTheFoe) {
					tileToMoveTo = tile;
				}
			}
			yield return new WaitForSeconds(delay);
			try {
				tileToMoveTo.plane.GetComponent<Renderer>().material.color = Color.yellow;
			} catch (System.NullReferenceException) {
				HideRange(moveRange);
				End();
				yield break;
			}
			camera2.transform.position = new Vector3(tileToMoveTo.transform.position.x, 30, tileToMoveTo.transform.position.z);
			yield return new WaitForSeconds(delay);
			ExecuteMovement(tileToMoveTo, tileToMoveTo.xCoordinate, tileToMoveTo.yCoordinate);
		} else if (distanceToTheFoe <= reach && distanceToTheFoe >= -reach) {
			print("FELL HERE");

				List<Tile> attackRange = ShowAttackRange(Maps.map01, xPosition, yPosition);
				Tile tileToAttack = turnTarget.currentTile;
				foreach (var tile in attackRange) {
					if (tile.occupant != null &&
						tile.occupant.GetComponent<Character>() == turnTarget) {
						//print(tile.occupant.GetComponent<Character>().ToString());
						//print(turnTarget.ToString());
						tileToAttack = tile;
					}
				}

				yield return new WaitForSeconds(delay);
			try {
				tileToAttack.plane.GetComponent<Renderer>().material.color = Color.yellow; 
			} catch (System.NullReferenceException) {
				HideRange(attackRange);
				End();
				yield break;

			}
			camera2.transform.position = new Vector3(tileToAttack.transform.position.x, 30, tileToAttack.transform.position.z);
			yield return new WaitForSeconds(delay);
			ExecuteAttack(tileToAttack, tileToAttack.xCoordinate, tileToAttack.yCoordinate);
			interfaceController.ShowAttackScreen(this.gameObject, tileToAttack.occupant);
			yield return new WaitForSeconds(3);
			End();
			yield break;
			
		}
		yield return new WaitForSeconds(delay);
		if (distanceToTheFoe > reach) {
			End();
		} else {
			List<Tile> attackRange = ShowAttackRange(Maps.map01, xPosition, yPosition);
			Tile tileToAttack = turnTarget.currentTile;
			foreach (var tile in attackRange) {
				if (tile.occupant != null &&
					tile.occupant.GetComponent<Character>() == turnTarget) {
					print(tile.occupant.GetComponent<Character>().ToString());
					print(turnTarget.ToString());
					tileToAttack = tile;
				}
			}
			
			yield return new WaitForSeconds(delay);
			tileToAttack.plane.GetComponent<Renderer>().material.color = Color.yellow;
			camera2.transform.position = new Vector3(tileToAttack.transform.position.x, 30, tileToAttack.transform.position.z);
			yield return new WaitForSeconds(delay);
			ExecuteAttack(tileToAttack, tileToAttack.xCoordinate, tileToAttack.yCoordinate);
			interfaceController.ShowAttackScreen(this.gameObject, tileToAttack.occupant);
			yield return new WaitForSeconds(3);
			End();
		}
		
	}

	public bool End() {
		turn = false;
		battleController.whoIsActive++;

		if (battleController.whoIsActive >= battleController.array.Length) {
			battleController.whoIsActive = 0;
		}

		battleController.SetActiveUnit(battleController.whoIsActive);

		this.gameObject.GetComponent<Renderer>().material.color = color;
		return true;
	}

	public int TakeHPDamage(int damage, int reduction) {
		int damageToBeTaken = (int)((damage - reduction) * Random.Range(0.8f, 1f));

		if (damageToBeTaken <= 0) {
			damageToBeTaken = 1;
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		}

		if (damage >= HP) {
			HP -= damageToBeTaken;
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		} else {
			HP -= damageToBeTaken;
			visibleHP.text = HP.ToString();
			damageCaused = damageToBeTaken;
			return damageToBeTaken;
		}

	}

	public List<Tile> ShowMovementRange(Tile[][] array, int positionX, int positionY) {

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
		for (int i = 0; i < move; i++) {
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
						differenceOfHeigth <= jump) {
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

		List<Tile> aux = new List<Tile>();
		foreach (Tile tile in movementRange) {
			if (tile.occupant == null) {
				aux.Add(tile);
			}
		}

		movementRange = aux;

		// Pinta de azul tudo o que pode ser movimentado sobre
		foreach (Tile tile in movementRange) {
			if (tile.occupant == null) {
				tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.blue;
			}
		}
		// Retorna os tiles pelo qual o personagem pode chegar à
		return movementRange;
	}

	public List<Tile> ShowAttackRange(Tile[][] array, int positionX, int positionY) {

		xPosition = positionX;
		yPosition = positionY;

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighboursAttack(array, positionX, positionY));

		// Lista que possuirá todos os tiles cujo o personagem pode atacar
		List<Tile> autoAttackRange = new List<Tile>();

		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < reach; i++) {
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
						differenceOfHeigth <= reach) {
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
				tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}

		// Retorna os tiles pelo qual o personagem pode atacar
		return autoAttackRange;
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
			target.isOccupiedByEnemy = true;
			target.plane.gameObject.GetComponent<Renderer>().material.color = target.color;
			this.gameObject.transform.position = new Vector3(target.transform.position.x
											, target.gameObject.transform.localScale.y
											, target.transform.position.z);
		}
	}

	public void ExecuteAttack(Tile target, int targetPositionX, int targetPositionY) {
		if (targetPositionX >= 0 && targetPositionY >= 0) {
			List<Tile> attackRange = ShowAttackRange(Maps.map01, xPosition, yPosition);

			HideRange(attackRange);

			interfaceController.charFace.GetComponent<Animator>().SetBool("Appear", true);

			if (target.isOccupiedByAlly) {
				Character targetAlly = target.occupant.GetComponent<Character>();
				damageCaused = targetAlly.TakeHPDamage(damage, targetAlly.totalCharacterResCut);
				targetAlly.RefreshHPMP();
			}
			if (target.isOccupiedByEnemy) {
				Enemy targetEnemy = target.occupant.GetComponent<Enemy>();
				damageCaused = targetEnemy.TakeHPDamage(damage, targetEnemy.armor);
			}
		}
	}

	public void HideRange(List<Tile> list) {
		foreach (Tile tile in list) {
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = tile.color;
		}
	}

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
					if (array[positionXAuxiliary][positionY].isOccupiedByAlly == false &&
						differenceOfHeigth1 <= jump) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (array[positionX][positionYAuxiliary].isOccupiedByAlly == false &&
						differenceOfHeigth2 <= jump) {
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
					if (differenceOfHeigth1 <= reach) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (differenceOfHeigth2 <= reach) {
						listOfAdjacentTiles.Add(array[positionX][positionYAuxiliary]);
					}

				}
			}
		}
		return listOfAdjacentTiles;
	}

	private void SetTurnTarget() {
		List<Character> utilitaryList = new List<Character>();
		for (int i = 0; i < battleController.array.Length; i++) {
			if (battleController.array[i].GetComponent<Character>() &&
				battleController.array[i].gameObject.activeSelf) {
				utilitaryList.Add(battleController.array[i].GetComponent<Character>());
			}
		}
		int utilitaryRandom = Random.Range(0, utilitaryList.Count);
		turnTarget = utilitaryList[utilitaryRandom];
	}

	private float DistanceOfTwoPoints(int point1X, int point1Y, int point2X, int point2Y) {
		//print("POINT1 " + point1X + "|" + point1Y + "/ POINT2 " + point2X + "|" + point2Y);
		//print("X " + (point2X - point1X) + "/ Y " + (point2Y - point1Y));
		print(Mathf.Sqrt((Mathf.Pow((point2X - point1X), 2) + Mathf.Pow((point2Y - point1Y), 2))));
		return Mathf.Sqrt((Mathf.Pow((point2X - point1X), 2) + Mathf.Pow((point2Y - point1Y), 2)));
		//return (point2X - point1X) + (point2Y - point1Y);
	}
}
