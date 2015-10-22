using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InterfaceController : MonoBehaviour  {
	public GameObject battlefieldView, orthographicView; // Camera da visão geral e da visão de mira respectivamente
	public Transform centerOfRotation; // Centro de rotação, que será o personagem atual
	private int stateOfRotation; // 0 = parado, 1 = esquerda, 2 = direita
	private int stateOfGapClosing; // 0 = parado, 1 = aproxima, 2 = distancia
	private int stateOfMovimentation; // 0 = parado, 1 = esquerda, 2 = direita, 3 = para cima, 4 = para baixo
	private float zLimit; // Auxiliar para o distanciamento da câmera
	private Canvas canvas; // Canvas. A princípio é mais auxiliar do que qualquer coisa
	private Button buttonUp, buttonDown, buttonLeft, buttonRight; // Botões direcionais
	private Button buttonConfirm, buttonCancel; // Botões lógicos
	private Button buttonSwitchCamera, buttonResetPosition; // Troca de câmera e reseta a posição da atual
	private bool currentCameraState; // false = Battlefield View, true = Orthographic View
	private bool currentButtonState; // false = Movement, true = Menu
	private Transform menu, menuSquare, menuArrow; // Quadrado do menu e recipiente do menu
	private Transform[] menuOptions, skillOptions; // Matriz que armazena a quantidade de opções tanto do menu quanto das opções de skills
	private int currentHighlightedOption; // int que indica qual opção no menu está "acesa"
	private int currentVisibleOption; //current = parte visível do menu
	private int totalAvailableOptions; // total = todas as opções, sejam visíveis ou não

	// Ao "acordar" define tais variáveis
	void Awake () {
		// Para prevenir cagadas futuras, desabilitei o multitouch
		Input.multiTouchEnabled = false;
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); // Encontra um Canvas caso haja
		// Matriz auxiliar que armazena todos os Botões existentes no Canvas
		Button[] canvasButtonList = Resources.FindObjectsOfTypeAll<Button>();
		// Um for que percorre toda a extensão do canvasButtonList e pega apenas os botões com os respectivos nomes
		for (int i = 0; i < canvasButtonList.Length; i++) {
			if (canvasButtonList[i].name == "Up") {
				buttonUp = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Down") {
				buttonDown = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Left") {
				buttonLeft = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Right") {
				buttonRight = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Cancel") {
				buttonCancel = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Confirm") {
				buttonConfirm = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Switch Camera") {
				buttonSwitchCamera = canvasButtonList[i];
			}
			if (canvasButtonList[i].name == "Reset Position") {
				buttonResetPosition = canvasButtonList[i];
			}
		}
		// Matriz auxiliar que armazena o painel do Menu
		Transform[] canvasMenu = Resources.FindObjectsOfTypeAll<Transform>();
		for (int i = 0; i < canvasMenu.Length; i++) {
			if (canvasMenu[i].name == "Menu" && menu == null) { 
				menu = canvasMenu[i];
			}
			if (canvasMenu[i].name == "Menu Panel" && menuSquare == null) { 
				menuSquare = canvasMenu[i];
			}
		}
		// Matrizes que encontram quantas opções tem no menu
		MenuOption[] auxMenuOptions = Resources.FindObjectsOfTypeAll<MenuOption>();
		int auxIntMenuOptions = 0;
		// Apenas checa quantas opções existem para setar o tamanho da matriz menuOptions
		for (int i = 0; i < auxMenuOptions.Length; i++) { 
			if (auxMenuOptions[i].tag == "Menu Option") {
				auxIntMenuOptions++;
			}
		}
		// Seta o tamanho da matriz de opções, para não dar merda apenas
		menuOptions = new Transform[auxIntMenuOptions]; 
		for (int i = 0; i < menuOptions.Length; i++) {
			menuOptions[i] = auxMenuOptions[i].transform;	
		}
		// Organiza os índices desse caralho
		for (int i = 0; i < menuOptions.Length; i++) { 
			if (menuOptions[i].name == "Move") {
				Transform pivot = menuOptions[0];
				menuOptions[0] = menuOptions[i];
				menuOptions[i] = pivot;
			}
			if (menuOptions[i].name == "Attack") {
				Transform pivot = menuOptions[1];
				menuOptions[1] = menuOptions[i];
				menuOptions[i] = pivot;
			}
			if (menuOptions[i].name == "Skill") {
				Transform pivot = menuOptions[2];
				menuOptions[2] = menuOptions[i];
				menuOptions[i] = pivot;
			}
			if (menuOptions[i].name == "Status") {
				Transform pivot = menuOptions[3];
				menuOptions[3] = menuOptions[i];
				menuOptions[i] = pivot;
			}
			if (menuOptions[i].name == "End") {
				Transform pivot = menuOptions[4];
				menuOptions[4] = menuOptions[i];
				menuOptions[i] = pivot;
			}
		}

		// For para encontrar a Seta do menu
		Transform[] auxMenuArrow = Resources.FindObjectsOfTypeAll<Transform>();
		for (int i = 0; i < auxMenuArrow.Length; i++) {
			if (auxMenuArrow[i].tag == "Menu Arrow" && menuArrow == null) {
				menuArrow = auxMenuArrow[i];
			} 
		}

		// Seta o tamanho do totalAvailableOptions para ser do mesmo tamanho das opções disponiveis para serem selecionadas
		totalAvailableOptions = menuOptions.Length;
	}
	
	void FixedUpdate () {
		// ------------------------------------------------------
		// Controladores de posicionamento do Battlefield View
		// ------------------------------------------------------

		// Máquina de estados para a rotação do Battlefield View
		if (stateOfRotation == 1 && battlefieldView.activeSelf && currentButtonState == false) {
			battlefieldView.transform.RotateAround(centerOfRotation.position, Vector3.up, 3f);
		}
		if (stateOfRotation == 2 && battlefieldView.activeSelf && currentButtonState == false) {
			battlefieldView.transform.RotateAround(centerOfRotation.position, Vector3.up, -3f);
		}

		// Máquina de estados para a aproximação do Battlefield View
		if (stateOfGapClosing == 1 && battlefieldView.activeSelf && zLimit < 10 && currentButtonState == false) {
			battlefieldView.transform.Translate(0f, 0f, 0.4f);
			zLimit += 0.4f;
		}
		if (stateOfGapClosing == 2 && battlefieldView.activeSelf && zLimit >= -10 && currentButtonState == false) {
			battlefieldView.transform.Translate(0f, 0f, -0.4f);
			zLimit -= 0.4f;
		}

		// ------------------------------------------------------
		// Controladores de posicionamento do Orthographic View
		// ------------------------------------------------------

		// Máquina de estados para o Orthographic View
		if (stateOfMovimentation == 1 && orthographicView.activeSelf && orthographicView.transform.position.x <= 22.5f
			&& currentButtonState == false) {
			orthographicView.transform.Translate(0.25f, 0, 0);
		}
		if (stateOfMovimentation == 2 && orthographicView.activeSelf && orthographicView.transform.position.x >= -22.5f
			&& currentButtonState == false) {
			orthographicView.transform.Translate(-0.25f, 0, 0);
		}
		if (stateOfMovimentation == 3 && orthographicView.activeSelf && orthographicView.transform.position.z >= -11f
			&& currentButtonState == false) {
			orthographicView.transform.Translate(0, -0.25f, 0);
		}
		if (stateOfMovimentation == 4 && orthographicView.activeSelf && orthographicView.transform.position.z <= 11f
			&& currentButtonState == false) {
			orthographicView.transform.Translate(0, 0.25f, 0);
		}

		// ------------------------------------------------------
		// Controladores do Menu
		// ------------------------------------------------------

		if (currentButtonState) {
			menu.gameObject.SetActive(true);
			buttonResetPosition.gameObject.SetActive(false);
			buttonSwitchCamera.gameObject.SetActive(false);
		} else {
			menu.gameObject.SetActive(false);
			buttonResetPosition.gameObject.SetActive(true);
			buttonSwitchCamera.gameObject.SetActive(true);
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

	// Mover para a esquerda a câmera ativa
	public void LeftPressMovementation() {
		stateOfMovimentation = 1;
	}
	// Mover para a direita a câmera ativa
	public void RightPressMovementation() {
		stateOfMovimentation = 2;
	}
	// Mover para cima a câmera ativa
	public void UpPressMovementation() {
		stateOfMovimentation = 3;
	}
	// Mover para baixo a câmera ativa
	public void DownPressMovementation() {
		stateOfMovimentation = 4;
	}
	// Parar a movimentação
	public void StopMoving() {
		stateOfMovimentation = 0;
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
		if (battlefieldView.activeSelf) {
			// Seta a posição igual à padrão
			battlefieldView.transform.position = new Vector3(0f, 15.5f, -18f);
			// Variável auxiliar que armazena os ângulos padrões
			Vector3 auxRotation = new Vector3(30, 0, 0);
			// Seta a rotação igual à padrão
			battlefieldView.transform.rotation = Quaternion.Euler(auxRotation);
			// Reseta o zLimit
			zLimit = 0f;
		}
		if (orthographicView.activeSelf) {
			// Seta a posição igual à padrão
			orthographicView.transform.position = new Vector3(0f, 30f, 0f);
			// Variável auxiliar que armazena os ângulos padrões
			Vector3 auxRotation = new Vector3(90, 0, 0);
			// Seta a rotação igual à padrão
			orthographicView.transform.rotation = Quaternion.Euler(auxRotation);
		}
	}

	// Troca as câmeras conforme seu estado
	public void SwitchCameras() {
		currentCameraState = !currentCameraState; // Inverte o estado do currentCameraState
		// Se for false o Battlefield View se ativa, caso contrário o Orthographic View
		if (currentCameraState == false) {
			battlefieldView.SetActive(true);
			orthographicView.SetActive(false);
			canvas.worldCamera = battlefieldView.GetComponent<Camera>();
		} else {
			battlefieldView.SetActive(false);
			orthographicView.SetActive(true);
			canvas.worldCamera = orthographicView.GetComponent<Camera>();
		}
	}

	// ---------------------------------------------------------
	// APRENDER COMO CARALHOS O LERP FUNCIONA E APLICAR EM TUDO DAQUI PRA BAIXO
	//---------------------------------------------------------
	// Mostra a opção inferior oculta do menu
	private void ScrollSnapDown() {
		if (currentButtonState) {
			if (currentCameraState) {
				menuSquare.transform.Translate(0, 1.56f, 0);
			} else {
				menuSquare.transform.Translate(0, 0.12f, 0);
			}
		}
	}
	// Mostra a opção superior oculta do menu
	private void ScrollSnapUp() {
		if (currentButtonState) {
			if (currentCameraState) {
				menuSquare.transform.Translate(0, -1.56f, 0);
			} else {
				menuSquare.transform.Translate(0, -0.12f, 0);
			}
		}
	}

	// FAZER O SNAP DIREITINHO

	// Move a seta do menu para baixo
	public void ArrowSnapDown() {
		if (currentButtonState) {
			if (currentCameraState) {
				if (currentHighlightedOption < totalAvailableOptions - 1) {
					currentHighlightedOption++;
					if (currentVisibleOption >= 4) {
						ScrollSnapDown();
						print(currentHighlightedOption);
					} else {
						currentVisibleOption++;
						menuArrow.transform.Translate(0, -1.56f, 0);
						print(currentHighlightedOption);
					}
				}
			} else {
				if (currentHighlightedOption < totalAvailableOptions - 1) {
					currentHighlightedOption++;
					if (currentVisibleOption >= 4) {
						ScrollSnapDown();
						print(currentHighlightedOption);
					} else {
						currentVisibleOption++;
						menuArrow.transform.Translate(0, -0.12f, 0);
						print(currentHighlightedOption);
					}
				}
			}
		}
	}
	// Move a seta do menu para cima
	public void ArrowSnapUp() {
		if (currentButtonState) {
			if (currentCameraState) {
				if (currentHighlightedOption > 0) {
					currentHighlightedOption--;
					if (currentVisibleOption > 0) {
						currentVisibleOption--;
						menuArrow.transform.Translate(0, 1.56f, 0);
						print(currentHighlightedOption);
					} else {
						ScrollSnapUp();
						print(currentHighlightedOption);
					}
				}
			} else {
				if (currentHighlightedOption > 0) {
					currentHighlightedOption--;
					if (currentVisibleOption > 0) {
						currentVisibleOption--;
						menuArrow.transform.Translate(0, 0.12f, 0);
						print(currentHighlightedOption);
					} else {
						ScrollSnapUp();
						print(currentHighlightedOption);
					}
				}
			}
		}
	}

	//























	// DELETAR DEPOIS, PARA FINS DE TESTE APENAS
	public void _NIGGER() {
		currentButtonState = !currentButtonState;
		for (int i = 0; i < menuOptions.Length; i++) {
			//print(menuOptions[i]+" INDEX "+i);
		}
	}
}
