using UnityEngine;
using System.Collections;

public class Santo : CharacterClass {

	public Santo() {
		name = "Santo";
		level = 1;
		exp = 0;
		bonusStrength += 1;
		bonusDexterity += 3;
		bonusAgility += 4;
		bonusConstitution += 5;
		bonusWisdom += 8;
		bonusLife += 20;
		bonusFaith += 50;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 15;
		bonusResCut += 2;
		bonusResPierce += 2;
		bonusResBlunt += 2;
		bonusResMoral += 12;
		bonusMove += 0;
		bonusJump += 0;
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
					bonusWisdom += 2;
					bonusFaith += 3;
				}
				if (level % 4 == 0) {
					bonusDexterity++;
				}
				if (level % 6 == 0) { 
					bonusAgility++;
				}
				if (level % 10 == 0) {
					bonusStrength++;
				}
			}
		}
	}
}
