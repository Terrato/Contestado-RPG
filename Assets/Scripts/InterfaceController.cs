﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InterfaceController : MonoBehaviour  {
	public GameObject battlefieldView, orthographicView; // Camera da visão geral e da visão de mira respectivamente
	public static GameObject staticBattlefieldView, staticOrthographicView;
	private static int[] cursorPositionArray = new int[2];
	public GameObject cursor;
	public Menu menu;
	public static Vector3 battlefieldViewStandardPosition, battlefieldViewStandardRotation;
	public static Transform centerOfRotation; // Centro de rotação, que será o personagem atual
	public static bool menuActiveLock, menuOptionLock;
	private int stateOfRotation; // 0 = parado, 1 = esquerda, 2 = direita
	private int stateOfGapClosing; // 0 = parado, 1 = aproxima, 2 = distancia
	private int stateOfMovimentation; // 0 = parado, 1 = esquerda, 2 = direita, 3 = para cima, 4 = para baixo
	private float zLimit; // Auxiliar para o distanciamento da câmera
	public Canvas canvas; // Canvas. A princípio é mais auxiliar do que qualquer coisa
	public static bool currentCameraState; // false = Battlefield View, true = Orthographic View
	private int yCoordinateCursor = 0, xCoordinateCursor = 0;

	// Ao "acordar" define tais variáveis
	void Awake () {

		menuOptionLock = false;
		menuActiveLock = false;
		staticBattlefieldView = battlefieldView;
		staticOrthographicView = orthographicView;
		cursor.SetActive(false);
		centerOfRotation = GameObject.Find("Center of Rotation").transform;

		battlefieldViewStandardPosition = new Vector3(0f, 15.5f, -18f);
		battlefieldViewStandardRotation = new Vector3(30f, 0f, 0f);
		
		// Para prevenir cagadas futuras, desabilitei o multitouch
		Input.multiTouchEnabled = false;
		
	}
	
	void FixedUpdate () {
		// ------------------------------------------------------
		// Controladores de posicionamento do Battlefield View
		// ------------------------------------------------------

		// Máquina de estados para a rotação do Battlefield View
		if ((menuOptionLock == false && menuActiveLock == false) &&
			stateOfRotation == 1 && battlefieldView.activeSelf ) {
			battlefieldView.transform.RotateAround(centerOfRotation.position, Vector3.up, 3f);
		}
		if ((menuOptionLock == false && menuActiveLock == false) &&
			stateOfRotation == 2 && battlefieldView.activeSelf ) {
			battlefieldView.transform.RotateAround(centerOfRotation.position, Vector3.up, -3f);
		}

		// Máquina de estados para a aproximação do Battlefield View
		if ((menuOptionLock == false && menuActiveLock == false) &&
			stateOfGapClosing == 1 && battlefieldView.activeSelf && zLimit < 10 ) {
			battlefieldView.transform.Translate(0f, 0f, 0.4f);
			zLimit += 0.4f;
		}
		if ((menuOptionLock == false && menuActiveLock == false) &&
			stateOfGapClosing == 2 && battlefieldView.activeSelf && zLimit >= -10)  {
			battlefieldView.transform.Translate(0f, 0f, -0.4f);
			zLimit -= 0.4f;
		}


	}

	// ------------------------------------------------------
	// Para uso com Event Triggers
	// ------------------------------------------------------

	// Rotacionar para a esquerda a câmera ativa
	public void LeftPressRotation() {
		stateOfRotation = 1;
	}
	// Rotacionar para a direita a câmera ativa
	public void RightPressRotation() {
		stateOfRotation = 2;
	}
	// Parar a rotação
	public void StopRotating() {
		stateOfRotation = 0;
	}

	// Aproximar a câmera ativa do ponto desejado
	public void GapCloseTowards() {
		stateOfGapClosing = 1;
	}
	// Afastar a câmera ativa do ponto desejado
	public void GapCloseAwayFrom() {
		stateOfGapClosing = 2;
	}
	// Parar a aproximação
	public void StopGapClosing() {
		stateOfGapClosing = 0;
	}
	
	// Resetar a posição da câmera ativa
	public void ResetCameraPosition() {
		if (battlefieldView.activeSelf && menuOptionLock == false) {
			// Seta a posição igual à padrão
			battlefieldView.transform.position = battlefieldViewStandardPosition; //new Vector3(0f, 15.5f, -18f);
			// Variável auxiliar que armazena os ângulos padrões
			Vector3 auxRotation = battlefieldViewStandardRotation; //new Vector3(30, 0, 0);
			// Seta a rotação igual à padrão
			battlefieldView.transform.rotation = Quaternion.Euler(auxRotation);
			// Reseta o zLimit
			zLimit = 0f;
		}
	}

	// Troca as câmeras conforme seu estado
	public void SwitchCameras() {
		currentCameraState = !currentCameraState; // Inverte o estado do currentCameraState
		// Se for false o Battlefield View se ativa, caso contrário o Orthographic View
		if (menuOptionLock == false) {
			if (currentCameraState == false) {
				canvas.worldCamera = battlefieldView.GetComponent<Camera>();
				battlefieldView.SetActive(true);
				orthographicView.SetActive(false);
				cursor.SetActive(false);
			} else {
				canvas.worldCamera = orthographicView.GetComponent<Camera>();
				battlefieldView.SetActive(false);
				orthographicView.SetActive(true);
				cursor.SetActive(true);
				CursorAim();
			}
		}
	}

	// Tem que aplicar uma lógica similar a do Aim nos Moves
	public void CursorMoveUp() {
		if (menuOptionLock == false && menuActiveLock == false) {
			if (yCoordinateCursor < Maps.map01[0].Length - 1 ) {
				yCoordinateCursor++;
				orthographicView.transform.position = new Vector3(Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.x, 30, Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.z);
			}
		}

	}

	public void CursorMoveDown() {
		if (menuOptionLock == false && menuActiveLock == false) {
			if (yCoordinateCursor > 0 ) {
				yCoordinateCursor--;
				orthographicView.transform.position = new Vector3(Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.x, 30, Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.z);
			}
		}
	}

	public void CursorMoveLeft() {
		if (menuOptionLock == false && menuActiveLock == false) {
			if (xCoordinateCursor > 0 ) {
				xCoordinateCursor--;
				orthographicView.transform.position = new Vector3(Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.x, 30, Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.z);
			}
		}
	}

	public void CursorMoveRight() {
		if (menuOptionLock == false && menuActiveLock == false) {
			if (xCoordinateCursor < Maps.map01.Length - 1 ) {
				xCoordinateCursor++;
				orthographicView.transform.position = new Vector3(Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.x, 30, Maps.map01[xCoordinateCursor][yCoordinateCursor].transform.position.z);//Translate(new Vector3(2, 0, 0));
			}
		}		
	}

	public void CursorAim() {
		if (menuOptionLock == false && menuActiveLock == false) {
			Character player = GameObject.Find("Player").GetComponent<Character>();// cursor get coordinates
			xCoordinateCursor = player.xPosition;
			yCoordinateCursor = player.yPosition;
			orthographicView.transform.position = new Vector3(player.transform.position.x, 30, player.transform.position.z);
		}
			
		
	}

	// Retorna null se o ocupante for null, caso contrário as coordenadas XY
	public void CursorGetCoordinatesIfHasOccupant() {
		cursorPositionArray[0] = xCoordinateCursor;
		cursorPositionArray[1] = yCoordinateCursor;
		if (Maps.map01[cursorPositionArray[0]][cursorPositionArray[1]].occupant != null) {
			// Aqui retorna o personagem ocupante do tile
			Character c = Maps.map01[cursorPositionArray[0]][cursorPositionArray[1]].gameObject.GetComponent<Tile>().occupant.GetComponent<Character>();
			print(c.xPosition + ","	+ c.yPosition);
		}
	}

	public void CursorGetCoordinatesGeneric() {
		cursorPositionArray[0] = xCoordinateCursor;
		cursorPositionArray[1] = yCoordinateCursor;

		print("ASDASD "+cursorPositionArray[0] + "," + cursorPositionArray[1]);

	}




	// DELETAR DEPOIS, PARA FINS DE TESTE APENAS
	// DEU MERDA AQUI
	public void _ASDASDASDAS() {
		int a = 1	, b = 0;
		Character asd = GameObject.Find("Player").GetComponent<Character>();
		asd.SetPosition(Maps.map01, a, b);
		asd.ShowMovementRange(Maps.map01, a, b);
		asd.ShowAttackRange(Maps.map01, a, b, "");

		int q = 4, w = 5;
		Character asd2 = GameObject.Find("Player2").GetComponent<Character>();
		asd2.SetPosition(Maps.map01, q, w);
		asd2.ShowMovementRange(Maps.map01, q, w);
	}
}
