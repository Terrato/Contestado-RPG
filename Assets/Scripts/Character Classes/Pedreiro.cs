using UnityEngine;
using System.Collections;

public class Pedreiro : CharacterClass {

	public Pedreiro() {
		name = "Pedreiro";
		level = 1;
		exp = 0;
		bonusStrength += 5;
		bonusDexterity += 1;
		bonusAgility += 2;
		bonusConstitution += 4;
		bonusWisdom += 3;
		bonusLife += 45;
		bonusFaith = 0;
		bonusFury = 100;
		bonusPersistance = 0;
		bonusAttack += 15;
		bonusResCut += 4;
		bonusResPierce += 4;
		bonusResBlunt += 1;
		bonusResMoral += 3;
		bonusMove = 0;
		bonusJump = 0;
		weaponArray = new string[] { "Cano de ferro", "Machado", "Martelo", "Porrete"};
		headArray = new string[] { "Chapéu de palha", "Capacete" };
		bodyArray = new string[] { "Camiseta", "Colete", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Sandalhas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				bonusLife += 3;
				if (level % 2 == 0) {
					bonusStrength++;
				}
				if (level % 4 == 0) {
					bonusConstitution++;
				}
				if (level % 7 == 0) {
					bonusDexterity++;
					bonusWisdom++;
					bonusResBlunt++;
				}
				if (level % 10 == 0) {
					bonusAgility++;
					bonusResCut++;
					bonusResPierce++;
					bonusResMoral++;
				}
			}
		}
	}
}
