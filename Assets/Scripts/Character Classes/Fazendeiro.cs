using UnityEngine;
using System.Collections;

public class Fazendeiro : CharacterClass {

	public Fazendeiro() {
		name = "Fazendeiro";
		level = 1;
		exp = 0;
		bonusStrength += 5;
		bonusDexterity += 2;
		bonusAgility += 3;
		bonusConstitution += 9;
		bonusWisdom += 2;
		bonusLife += 30;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance += 100;
		bonusAttack += 15;
		bonusResCut += 5;
		bonusResPierce += 3;
		bonusResBlunt += 4;
		bonusResMoral += 1;
		bonusMove -= 1;
		bonusJump += 0;
		bonusReach += 1;
		weaponArray = new string[] { "Ancinho", "Foice", "Enxada" };
		headArray = new string[] { "Chapéu de couro", "Chapéu de palha" };
		bodyArray = new string[] { "Camiseta", "Colete", "Gibão", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife ++;
				if (level % 2 == 0) {
					bonusConstitution++;
				}
				if (level % 3 == 0) {
					bonusStrength++;
				}
				if (level % 4 == 0) {
					bonusAgility++;
				}
				if (level % 5 == 0) {
					bonusDexterity++;
				}
				if (level % 8 == 0) {
					bonusResMoral++;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
				}
			}
		}
	}
}

