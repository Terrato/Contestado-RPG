using UnityEngine;
using System.Collections;

public class Cavaleiro : CharacterClass {

	public Cavaleiro() {
		name = "Cavaleiro";
		level = 1;
		exp = 0;
		bonusStrength += 4;
		bonusDexterity += 4;
		bonusAgility += 9;
		bonusConstitution += 4;
		bonusWisdom += 1;
		bonusLife += 25;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance = 100;
		bonusAttack += 12;
		bonusResCut += 2;
		bonusResPierce += 2;
		bonusResBlunt += 3;
		bonusResMoral += 0;
		bonusMove = 1;
		bonusJump = 0;
		bonusReach = 1;
		weaponArray = new string[] { "Cano de ferro", "Vassoura", "Bastão", "Foice", "Lança", "Tridente", "Enxada"};
		headArray = new string[] { "Capacete", "Chapéu de couro" };
		bodyArray = new string[] { "Sobretudo", "Colete", "Jaqueta", "Gibão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				bonusLife++;
				if (level % 2 == 0) {
					bonusAgility++;
				}
				if (level % 3 == 0) {
					bonusStrength++;
				}
				if (level % 5 == 0) {
					bonusConstitution++;
				}
				if (level % 7 == 0) {
					bonusDexterity++;
				}
				if (level % 9 == 0) {
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
