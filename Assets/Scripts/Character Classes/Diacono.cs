using UnityEngine;
using System.Collections;

public class Diacono : CharacterClass {

	public Diacono() { }

	public Diacono(Desempregado desempregado) {
		name = "Diácono";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 1 + desempregado.bonusStrength;
		bonusDexterity += 3 + desempregado.bonusDexterity;
		bonusAgility += 1 + desempregado.bonusAgility;
		bonusConstitution += 2 + desempregado.bonusConstitution;
		bonusWisdom += 5 + desempregado.bonusWisdom;
		bonusLife += 25 + desempregado.bonusLife;
		bonusFaith += 50;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 5 + desempregado.bonusAttack;
		bonusResCut += 0 + desempregado.bonusResCut;
		bonusResPierce += 0 + desempregado.bonusResPierce;
		bonusResBlunt += 3 + desempregado.bonusResBlunt;
		bonusResMoral += 5 + desempregado.bonusResMoral;
		bonusMove = 0 + desempregado.bonusMove;
		bonusJump = -1 + desempregado.bonusJump;
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusLife += 2;
				if (level % 2 == 0) {
					bonusWisdom++;
					bonusDexterity++;
				}
				if (level % 4 == 0) { 
					bonusConstitution++;
					bonusFaith += 3;
				}
				if (level % 6 == 0) {
					bonusStrength++;
					bonusAgility++;
				}
				if (level % 10 == 0) {
					bonusResBlunt++;
					bonusResMoral += 2;
				}
			}
		}
	}
}
