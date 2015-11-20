using UnityEngine;
using System.Collections;

public class Monge : CharacterClass {

	public Monge() { }

	public Monge(Diacono diacono) {
		name = "Monge";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 7 + diacono.bonusStrength;
		bonusDexterity += 3 + diacono.bonusDexterity;
		bonusAgility += 5 + diacono.bonusAgility;
		bonusConstitution += 3 + diacono.bonusConstitution;
		bonusWisdom += 2 + diacono.bonusWisdom;
		bonusLife += 50 + diacono.bonusLife;
		bonusFaith += 10 + diacono.bonusFaith;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 25 + diacono.bonusAttack;
		bonusResCut += 5 + diacono.bonusResCut;
		bonusResPierce += 3 + diacono.bonusResPierce;
		bonusResBlunt += 8 + diacono.bonusResBlunt;
		bonusResMoral += 2 + diacono.bonusResMoral;
		bonusMove += 1 + diacono.bonusMove;
		bonusJump += 1 + diacono.bonusJump;
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
