using UnityEngine;
using System.Collections;

public class Atirador: CharacterClass {


	public Atirador() {
		name = "Atirador";
		level = 1;
		exp = 0;
		bonusStrength += 0;
		bonusDexterity += 7;
		bonusAgility += 5;
		bonusConstitution += 2;
		bonusWisdom += 4;
		bonusLife += 35;
		bonusFaith += 30;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 20;
		bonusResCut += 2;
		bonusResPierce += 2;
		bonusResBlunt += 2;
		bonusResMoral += 2;
		bonusMove = 0;
		bonusJump = 1;
		bonusReach = 4;
	}


	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusDexterity++;
				if (level % 2 == 0) {
					bonusLife++;
				}
				if (level % 4 == 0) {
				}
				if (level % 6 == 0) {
					bonusAgility++;
					bonusConstitution++;
					bonusStrength++;
					bonusFaith += 3;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
				}
			}
		}
	}
}
