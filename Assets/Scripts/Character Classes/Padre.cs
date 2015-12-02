using UnityEngine;
using System.Collections;

public class Padre : CharacterClass {

	public Padre() {
		name = "Padre";
		level = 1;
		exp = 0;
		bonusStrength += 1;
		bonusDexterity += 4;
		bonusAgility += 3;
		bonusConstitution += 2;
		bonusWisdom += 6;
		bonusLife += 25;
		bonusFaith += 25;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 5;
		bonusResCut += 1;
		bonusResPierce += 1;
		bonusResBlunt += 4;
		bonusResMoral += 6;
		bonusMove += 1;
		bonusJump += 0;
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife += 2;
				if (level % 2 == 0) {
					bonusFaith += 2;
				}
				if (level % 3 == 0) { 
					bonusConstitution++;
					bonusWisdom++;
				}
				if (level % 4 == 0) {
					bonusDexterity++;
				}
				if (level % 5 == 0) {
					bonusStrength++;
					bonusFaith += 1;
				}
				if (level % 6 == 0) {
					bonusAgility++;
				}
				if (level % 10 == 0) {
					bonusResPierce++;
					bonusResBlunt++;
					bonusResMoral++;
				}
			}
		}
	}
}
