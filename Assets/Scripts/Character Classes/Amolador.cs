using UnityEngine;
using System.Collections;

public class Amolador : CharacterClass {

	public Amolador() {
		name = "Amolador";
		level = 1;
		exp = 0;
		bonusStrength += 2;
		bonusDexterity += 4;
		bonusAgility += 3;
		bonusConstitution += 1;
		bonusWisdom += 3;
		bonusLife += 25;
		bonusFaith = 0;
		bonusFury = 100;
		bonusPersistance = 0;
		bonusAttack += 20;
		bonusResCut += 3;
		bonusResPierce += 2;
		bonusResBlunt += 2;
		bonusResMoral += 2;
		bonusMove = 0;
		bonusJump = 0;
		weaponArray = new string[] { "Cano de ferro", "Machado", "Faca", "Facão" };
		headArray = new string[] { "Chapéu de palha", "Chapéu de couro" };
		bodyArray = new string[] { "Camiseta", "Colete", "Camisa" };
		feetArray = new string[] { "Sapatos", "Botinas", "Sandalhas" };
	}

	public override void LevelUp() {
		if (level < 99){
			if (exp >= 99) {
				level++;
				exp -= 99;
				if (level % 2 == 0) {
					bonusDexterity++;
				}
				if (level % 3 == 0) {
					bonusLife += 2;	
				}
				if (level % 4 == 0) {
					bonusConstitution++;
				}
				if (level % 5 == 0) {
					bonusStrength++;
				}
				if (level % 7 == 0) {
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
