using UnityEngine;
using System.Collections;

public class Monge : CharacterClass {

	public Monge() {
		name = "Monge";
		level = 1;
		exp = 0;
		bonusStrength += 7;
		bonusDexterity += 3;
		bonusAgility += 5;
		bonusConstitution += 3;
		bonusWisdom += 2;
		bonusLife += 50;
		bonusFaith += 10;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 25;
		bonusResCut += 5;
		bonusResPierce += 3;
		bonusResBlunt += 8;
		bonusResMoral += 2 ;
		bonusMove += 1;
		bonusJump += 1;
		weaponArray = new string[] { "Marreta", "Bastão", "Porrete", "Martelo" };
		headArray = new string[] { "Bandana", "Chapéu de couro", "Coroa de cipó" };
		bodyArray = new string[] { "Camiseta", "Colete", "Robe", "Camisa"};
		feetArray = new string[] { "Sapatos", "Botinas", "Botas" };
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife += 4;
				if (level % 2 == 0) {
					bonusStrength++;
				}
				if (level % 3 == 0) { 
					bonusAgility++;
				}
				if (level % 4 == 0) {
					bonusDexterity++;
				}
				if (level % 5 == 0) {
					bonusConstitution++;
					bonusFaith += 1;
				}
				if (level % 6 == 0) { 
					bonusResCut++;
					bonusResPierce++;
					bonusResBlunt++;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
				}
			}
		}
	}
}
