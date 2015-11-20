using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Menu : MonoBehaviour{

	public Transform menu, menuSquare, menuArrow; // Quadrado do menu e recipiente do menu
	public GameObject statusScreen; // 
	public Transform[] menuOptions, skillOptions; // Matriz que armazena a quantidade de opções tanto do menu quanto das opções de skill
	public Character character; 
	private int currentHighlightedOption; // int que indica qual opção no menu está "acesa"
	public int selectedOption {get; set;} // Qual opção será selecionada
	private int currentVisibleOption; //current = parte visível do menu
	private int totalAvailableOptions; // total = todas as opções, sejam visíveis ou não
	private int cancelCount = 0;



	public void Awake(){
		totalAvailableOptions = menuOptions.Length;
	}

	private void ScrollSnapDown() {
		if (InterfaceController.currentCameraState) {
			menuSquare.transform.Translate(0, 1.56f, 0);
		} else {
			menuSquare.transform.Translate(0, 0.12f, 0);
		}
	}
	// Mostra a opção superior oculta do menu
	private void ScrollSnapUp() {
		if (InterfaceController.currentCameraState) {
			menuSquare.transform.Translate(0, -1.56f, 0);
		} else {
			menuSquare.transform.Translate(0, -0.12f, 0);
		}
	}

	// Move a seta do menu para baixo
	public void ArrowSnapDown() {
		if (this.gameObject.activeSelf) {
			if (InterfaceController.menuOptionLock == false) {
				if (InterfaceController.currentCameraState) {
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
	}
	// Move a seta do menu para cima
	public void ArrowSnapUp() {
		if (this.gameObject.activeSelf) {
			if (InterfaceController.menuOptionLock == false) {
				if (InterfaceController.currentCameraState) {
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
	}

	public void SelectOption() {
		if (this.gameObject.activeSelf) {
			if (currentHighlightedOption == 0) {
				selectedOption = currentHighlightedOption;
				// base.ShowMovementRange(Maps.map01, base.xPosition, base.yPosition);
				// Tipo, tem que de alguma forma chamar aqui o método Character.showMovementRange()
				// Move
			}
			if (currentHighlightedOption == 1) {
				selectedOption = currentHighlightedOption;

				// Ataca
			}
			if (currentHighlightedOption == 2) {
				selectedOption = currentHighlightedOption;

				// Skill
			}
			if (currentHighlightedOption == 3) {
				selectedOption = currentHighlightedOption;
				statusScreen.gameObject.SetActive(true);
				InterfaceController.menuOptionLock = true;
				// Status
			}
			if (currentHighlightedOption == 4) {
				selectedOption = currentHighlightedOption;

				// Finalizar turno
			}
			if (cancelCount < -1) {
				
			}
		}
	}

	public void CancelOption() {
		if (this.gameObject.activeSelf) {
			if (currentHighlightedOption == 0) {
				selectedOption = currentHighlightedOption;
				cancelCount--;
				// Move
			}
			if (currentHighlightedOption == 1) {
				selectedOption = currentHighlightedOption;
				cancelCount--;
				// Ataca
			}
			if (currentHighlightedOption == 2) {
				selectedOption = currentHighlightedOption;
				cancelCount--;
				// Skill
			}
			if (currentHighlightedOption == 3) {
				selectedOption = currentHighlightedOption;
				statusScreen.gameObject.SetActive(false);
				InterfaceController.menuOptionLock = false;
				cancelCount--;
				// Status
			}
			if (currentHighlightedOption == 4) {
				selectedOption = currentHighlightedOption;
				cancelCount--;
				// Finalizar turno
			}
			if (cancelCount < -1) {
				AlterMenuState();
			}
		}
	}

	public void AlterMenuState() {
		if (this.gameObject.activeSelf) {
			this.gameObject.SetActive(false);
		} else {
			this.gameObject.SetActive(true);
		}
	}
}

