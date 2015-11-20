using UnityEngine;
using System.Collections;

public class Padre : CharacterClass {


	public Padre() { }

	public Padre(Diacono diacono) {
		name = "Padre";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 1 + diacono.bonusStrength;
		bonusDexterity += 4 + diacono.bonusDexterity;
		bonusAgility += 3 + diacono.bonusAgility;
		bonusConstitution += 2 + diacono.bonusConstitution;
		bonusWisdom += 6 + diacono.bonusWisdom;
		bonusLife += 25 + diacono.bonusLife;
		bonusFaith += 25 + diacono.bonusFaith;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 5 + diacono.bonusAttack;
		bonusResCut += 1 + diacono.bonusResCut;
		bonusResPierce += 1 + diacono.bonusResPierce;
		bonusResBlunt += 4 + diacono.bonusResBlunt;
		bonusResMoral += 6 + diacono.bonusResMoral;
		bonusMove += 1 + diacono.bonusMove;
		bonusJump += 0 + diacono.bonusJump;
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
