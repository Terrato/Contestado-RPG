using UnityEngine;
using System.Collections;

public class Diacono : CharacterClass {

	public Diacono() {
		name = "Diácono";
		level = 1;
		exp = 0;
		bonusStrength += 1;
		bonusDexterity += 3;
		bonusAgility += 1;
		bonusConstitution += 2;
		bonusWisdom += 5;
		bonusLife += 25;
		bonusFaith += 50;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 5;
		bonusResCut += 0;
		bonusResPierce += 0;
		bonusResBlunt += 3;
		bonusResMoral += 5;
		bonusMove = 0 ;
		bonusJump = -1;
		weaponArray = new string[] { "Galho de árv.", "Vassoura", "Bastão", "Porrete" };
		headArray = new string[] { "Chapéu de palha", "Chapéu de couro", "Coroa de cipó" };
		bodyArray = new string[] { "Camiseta", "Colete", "Robe", "Camisa", "Roupão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Sandalhas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife += 2;
				if (level % 2 == 0) {
					bonusWisdom++;
					bonusDexterity++;
				}
				if (level % 4 == 0) { 
					bonusConstitution++;
					bonusFaith += 3;
				}
				if (level % 6 == 0) {
					bonusStrength++;
					bonusAgility++;
				}
				if (level % 10 == 0) {
					bonusResBlunt++;
					bonusResMoral += 2;
				}
			}
		}
	}
}
