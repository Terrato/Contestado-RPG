using UnityEngine;
using System.Collections;

public class Matador : CharacterClass {

	public Matador() {
		name = "Matador";
		level = 1;
		exp = 0;
		bonusStrength += 6;
		bonusDexterity += 6;
		bonusAgility += 3;
		bonusConstitution += 2;
		bonusWisdom += 0;
		bonusLife += 25;
		bonusFaith += 50;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 25;
		bonusResCut += 5;
		bonusResPierce += 5;
		bonusResBlunt += 5;
		bonusResMoral += 5;
		bonusMove = 1;
		bonusJump = 1;
		weaponArray = new string[] { "Faca", "Facão", "Espada", "Machado", "Martelo", "Marreta", "Foice" };
		headArray = new string[] { "Capacete", "Chapéu de couro", "Capuz" };
		bodyArray = new string[] { "Jaqueta", "Colete", "Robe", "Gibão" };
		feetArray = new string[] { "Sapatos", "Botinas", "Tênis" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				if (level % 2 == 0) {
					bonusStrength++;
					bonusDexterity++;
				}
				if (level % 3 == 0) { 
					bonusAgility++;
				}
				if (level % 6 == 0) {
					bonusFaith ++;
					bonusWisdom++;
				}
				if (level % 10 == 0) {
					bonusConstitution++;
				}
			}
		}
	}
}
