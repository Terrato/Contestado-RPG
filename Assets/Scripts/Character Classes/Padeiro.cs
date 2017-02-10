using UnityEngine;
using System.Collections;

public class Padeiro : CharacterClass {

	public Padeiro() {
		name = "Padeiro";
		level = 1;
		exp = 0;
		bonusStrength += 7;
		bonusDexterity += 4;
		bonusAgility += 1;
		bonusConstitution += 9;
		bonusWisdom += 0;
		bonusLife += 50;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance += 100;
		bonusAttack += 20;
		bonusResCut += 5;
		bonusResPierce += 2;
		bonusResBlunt += 6;
		bonusResMoral += 4;
		bonusMove += 0;
		bonusJump += 0;
		weaponArray = new string[] { "Ancinho", "Foice", "Enxada", "Rolo" };
		headArray = new string[] { "Chapéu de couro", "Chapéu de palha" };
		bodyArray = new string[] { "Camiseta", "Colete", "Gibão", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife += 3;
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