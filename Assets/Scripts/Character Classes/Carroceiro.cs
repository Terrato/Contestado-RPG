using UnityEngine;
using System.Collections;

public class Carroceiro : CharacterClass {

	public Carroceiro() {
		name = "Carroceiro";
		level = 1;
		exp = 0;
		bonusStrength += 3;
		bonusDexterity += 2;
		bonusAgility += 5;
		bonusConstitution += 3;
		bonusWisdom += 2;
		bonusLife += 30;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance = 50;
		bonusAttack += 10;
		bonusResCut += 2;
		bonusResPierce += 2;
		bonusResBlunt += 3;
		bonusResMoral += 2;
		bonusMove = 1;
		bonusJump = 0;
		weaponArray = new string[] { "Cano de ferro", "Vassoura", "Bastão"};
		headArray = new string[] { "Chapéu de palha", "Chapéu de couro" };
		bodyArray = new string[] { "Camiseta", "Colete", "Jaqueta", "Gibão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Sandalhas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				if (level % 2 == 0) {
					bonusAgility++;
					bonusLife++;
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
				if (level % 11 == 0) {
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
					bonusResMoral++;
				}
			}
		}
	}
}
