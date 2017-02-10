using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BaseSkill : MonoBehaviour {

	public string name { get; set; }
	public string description { get; set; }
	public int damage { get; set; }
	public int reach { get; set; }
	public int targetAreaRange { get; set; }
	public Tile target { get; set; }
	public List<Tile> targetArea { get; set; }

	public List<Tile> ShowSkillRange(Tile[][] array, int initialPositionX, int initialPositionY) {

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighboursAttack(array, initialPositionX, initialPositionY));

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
					if (neighbours != array[initialPositionX][initialPositionX] &&
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
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.magenta;
			if (tile.occupant != null) {
				tile.canBeClicked = true;
				tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}

		// Retorna os tiles pelo qual o personagem pode atacar
		return autoAttackRange;
	}

	public List<Tile> ShowAreaRange(Tile[][] array, int selectedPositionX, int selectedPositionY) {

		// Lista de tiles visitados
		List<Tile> visitedPositions = new List<Tile>();
		// Lista com os próximos tiles a serem visitados
		List<Tile> nextToVisit = new List<Tile>(CheckNeighboursAttack(array, selectedPositionX, selectedPositionY));

		// Lista que possuirá todos os tiles cujo o personagem pode atacar
		List<Tile> autoAttackRange = new List<Tile>();

		// Gera a lista de tiles pelo quais o personagem pode se mexer
		for (int i = 0; i < targetAreaRange; i++) {
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
						differenceOfHeigth <= targetAreaRange) {
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
					if (neighbours != array[selectedPositionX][selectedPositionX] &&
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
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = Color.yellow;
			if (tile.occupant != null) {
				tile.canBeClicked = true;
			}
		}

		// Retorna os tiles pelo qual o personagem pode atacar
		return autoAttackRange;
	}

	public void HideRange(List<Tile> list) {
		foreach (Tile tile in list) {
			tile.gameObject.GetComponent<Tile>().plane.GetComponent<Renderer>().material.color = tile.color;
			tile.canBeClicked = false;
		}
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
					if (differenceOfHeigth1 <= targetAreaRange) {
						listOfAdjacentTiles.Add(array[positionXAuxiliary][positionY]);
					}
					if (differenceOfHeigth2 <= targetAreaRange) {
						listOfAdjacentTiles.Add(array[positionX][positionYAuxiliary]);
					}

				}
			}
		}
		return listOfAdjacentTiles;
	}

}
