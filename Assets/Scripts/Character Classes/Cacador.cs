using UnityEngine;
using System.Collections;

public class Cacador : CharacterClass {

	public Cacador() { }

	public Cacador(Desempregado desempregado) {
		name = "Caçador";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 1 + desempregado.bonusStrength;
		bonusDexterity += 3 + desempregado.bonusDexterity;
		bonusAgility += 5 + desempregado.bonusAgility;
		bonusConstitution += 3 + desempregado.bonusConstitution;
		bonusWisdom += 2 + desempregado.bonusWisdom;
		bonusLife += 35 + desempregado.bonusLife;
		bonusFaith += 30;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 10 + desempregado.bonusAttack;
		bonusResCut += 2 + desempregado.bonusResCut;
		bonusResPierce += 4 + desempregado.bonusResPierce;
		bonusResBlunt += 1 + desempregado.bonusResBlunt;
		bonusResMoral += 1 + desempregado.bonusResMoral;
		bonusMove = 0 + desempregado.bonusMove;
		bonusJump = 0 + desempregado.bonusJump;
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
