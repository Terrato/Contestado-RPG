using UnityEngine;
using System.Collections;

public class Costureiro : CharacterClass {

	public Costureiro() {
		name = "Costureiro";
		level = 1;
		exp = 0;
		bonusStrength += 1;
		bonusDexterity += 7;
		bonusAgility += 4;
		bonusConstitution += 9;
		bonusWisdom += 3;
		bonusLife += 20;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance += 100;
		bonusAttack += 25;
		bonusResCut += 1;
		bonusResPierce += 1;
		bonusResBlunt += 1;
		bonusResMoral += 1;
		bonusMove += 0;
		bonusJump += 0;
		bonusReach += 1;
		weaponArray = new string[] { "Ancinho", "Foice", "Enxada", "Espada", "Facão" };
		headArray = new string[] { "Chapéu de couro", "Chapéu de palha" };
		bodyArray = new string[] { "Camiseta", "Colete", "Gibão", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				if (level % 2 == 0) {
					bonusDexterity++;
				}
				if (level % 3 == 0) {
					bonusLife += 2;
					bonusConstitution++;
				}
				if (level % 4 == 0) {
					bonusAgility++;
				}
				if (level % 5 == 0) {
					bonusStrength++;
				}
				if (level % 8 == 0) {
					bonusWisdom++;
				}
				if (level % 10 == 0) {
					bonusResMoral++;
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
				}
			}
		}
	}
}
