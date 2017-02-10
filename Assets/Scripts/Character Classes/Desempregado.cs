using UnityEngine;
using System.Collections;

public class Desempregado : CharacterClass {

	public Desempregado() {
		name = "Desempregado";
		level = 1;
		exp = 0;
		bonusStrength += 3;
		bonusDexterity += 1;
		bonusAgility += 4;
		bonusConstitution += 2;
		bonusWisdom += 1;
		bonusLife += 20;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance = 50;
		bonusAttack += 10;
		bonusResCut += 1;
		bonusResPierce += 1;
		bonusResBlunt += 3;
		bonusResMoral += 5;
		bonusMove = 0;
		bonusJump = 1;
		weaponArray = new string[] { "Cano de ferro", "Cabo de vassoura", "Bastão de madeira", "Porrete", "Faca", "Facão" };
		headArray = new string[] { "Chapéu de palha", "Chapéu de couro" };
		bodyArray = new string[] { "Camiseta", "Colete", "Camisa", "Gibão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Sandalhas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				bonusLife += 2;
				if (level % 2 == 0) {
					bonusAgility++;
				}
				if (level % 3 == 0) {
					bonusStrength++;
				}
				if (level % 4 == 0) {
					bonusConstitution++;
				}
				if (level % 5 == 0) {
					bonusDexterity++;
				}
				if (level % 6 == 0) {
					bonusWisdom++;
				}
				if (level % 10 == 0) {
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
					bonusResMoral++;
				}
			}
		}
	}

	
}
