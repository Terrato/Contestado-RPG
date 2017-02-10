using UnityEngine;
using System.Collections;

public class Cacador : CharacterClass {

	public Cacador() {
		name = "Caçador";
		level = 1;
		exp = 0;
		bonusStrength += 1;
		bonusDexterity += 3;
		bonusAgility += 5;
		bonusConstitution += 3;
		bonusWisdom += 2;
		bonusLife += 35;
		bonusFaith += 30;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 10;
		bonusResCut += 2;
		bonusResPierce += 4 ;
		bonusResBlunt += 1;
		bonusResMoral += 1;
		bonusMove = 0;
		bonusJump = 0;
		bonusReach = 3;
		weaponArray = new string[] { "Arco Composto", "Luger P08", "Remington M10", "Springfield1903", "Colt M1898", "Colt Walker" };
		headArray = new string[] { "Chapéu de palha", "Chapéu de couro", "Capacete", "Cap", "Boina" };
		bodyArray = new string[] { "Camiseta", "Colete", "Jaqueta", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife++;
				if (level % 2 == 0) {
					bonusDexterity++;
				}
				if (level % 4 == 0) { 
					bonusAgility++;
				}
				if (level % 6 == 0) {
					bonusConstitution++;
					bonusStrength++;
					bonusFaith += 3;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
					bonusResCut++;
					bonusResPierce++;
				}
			}
		}
	}
}
