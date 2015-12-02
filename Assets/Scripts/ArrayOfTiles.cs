using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayOfTiles : MonoBehaviour {

	public string mapID;
	public Tile[] arrayOfTiles;

	// Use this for initialization
	void Awake () {
		Tile[][] map;

		// Maiores coordenadas X e Y que serão utilizadas posteriormente
		int quantityOfXCoordinates = 0;
		int quantityOfYCoordinates = 0;

		// Encontra e seta as ditas coordenadas
		for (int i = 0; i < arrayOfTiles.Length; i++) {
			if (arrayOfTiles[i].xCoordinate >= quantityOfXCoordinates) {
				quantityOfXCoordinates = arrayOfTiles[i].xCoordinate + 1;
			}
			if (arrayOfTiles[i].yCoordinate >= quantityOfYCoordinates) {
				quantityOfYCoordinates = arrayOfTiles[i].yCoordinate + 1;
			}
		}
		
		// Uma lista que contém todos as parcelas da arrayOfTiles splitada
		List<Tile[]> splitted = new List<Tile[]>(); // This list will contain all the splitted arrays.
		int lengthToSplit = quantityOfYCoordinates;
		int arrayLength = arrayOfTiles.Length;

		for (int i = 0; i < arrayLength; i = i + lengthToSplit) {
			Tile[] val = new Tile[lengthToSplit];

			if (arrayLength < i + lengthToSplit) {
				lengthToSplit = arrayLength - i;
			}
			Array.Copy(arrayOfTiles, i, val, 0, lengthToSplit);
			splitted.Add(val);
		}
		
		// Cria as dimensões do mapa nos conformes da lista splitada
		map = new Tile[splitted.Count][];
		for (int i = 0; i < map.Length; i++) {
			map[i] = new Tile[splitted[i].Length];
		}
		
		// Define cada posição do mapa como a respectiva da lista
		for (int i = 0; i < splitted.Count; i++) {
			for (int j = 0; j < splitted[i].Length; j++) {
				map[i] = splitted[i];
			}
		}
		
		Maps.map01 = map;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
