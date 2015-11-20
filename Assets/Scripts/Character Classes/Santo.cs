using UnityEngine;
using System.Collections;

public class Santo : CharacterClass {

	public Santo() { }

	public Santo(Diacono diacono) {
		name = "Santo";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 1 + diacono.bonusStrength;
		bonusDexterity += 3 + diacono.bonusDexterity;
		bonusAgility += 4 + diacono.bonusAgility;
		bonusConstitution += 5 + diacono.bonusConstitution;
		bonusWisdom += 8 + diacono.bonusWisdom;
		bonusLife += 20 + diacono.bonusLife;
		bonusFaith += 50 + diacono.bonusFaith;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 15 + diacono.bonusAttack;
		bonusResCut += 2 + diacono.bonusResCut;
		bonusResPierce += 2 + diacono.bonusResPierce;
		bonusResBlunt += 2 + diacono.bonusResBlunt;
		bonusResMoral += 12 + diacono.bonusResMoral;
		bonusMove += 0 + diacono.bonusMove;
		bonusJump += 0 + diacono.bonusJump;
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
