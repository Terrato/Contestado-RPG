using UnityEngine;
using System.Collections;

public class Mecanico : CharacterClass {

	public Mecanico() {
		name = "Mecanico";
		level = 1;
		exp = 0;
		bonusStrength += 6;
		bonusDexterity += 0;
		bonusAgility += 5;
		bonusConstitution += 6;
		bonusWisdom += 1;
		bonusLife += 50;
		bonusFaith = 0;
		bonusFury = 0;
		bonusPersistance = 100;
		bonusAttack += 10;
		bonusResCut += 6;
		bonusResPierce += 6;
		bonusResBlunt += 6;
		bonusResMoral += 1;
		bonusMove = 0;
		bonusJump = 0;
		weaponArray = new string[] { "Cano de ferro", "Mertelo", "Marreta"};
		headArray = new string[] { "Capacete", "Chapéu de couro" };
		bodyArray = new string[] { "Camiseta", "Colete", "Jaqueta", "Gibão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				bonusLife += 2;
				if (level % 2 == 0) {
					bonusStrength++;
				}
				if (level % 3 == 0) {
					bonusAgility++;
				}
				if (level % 5 == 0) {
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
				}
				if (level % 7 == 0) {
					bonusDexterity++;
				}
				if (level % 9 == 0) {
					bonusConstitution++;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
					bonusResMoral++;
				}
			}
		}
	}
}
