using UnityEngine;
using System.Collections;

public class Matador : CharacterClass {

	public Matador() { }

	public Matador(Cacador cacador) {
		name = "Matador";
		description =	"Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 6 + cacador.bonusStrength;
		bonusDexterity += 6 + cacador.bonusDexterity;
		bonusAgility += 3 + cacador.bonusAgility;
		bonusConstitution += 2 + cacador.bonusConstitution;
		bonusWisdom += 0 + cacador.bonusWisdom;
		bonusLife += 25 + cacador.bonusLife;
		bonusFaith += 50 + cacador.bonusFaith;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 25 + cacador.bonusAttack;
		bonusResCut += 5 + cacador.bonusResCut;
		bonusResPierce += 5 + cacador.bonusResPierce;
		bonusResBlunt += 5 + cacador.bonusResBlunt;
		bonusResMoral += 5 + cacador.bonusResMoral;
		bonusMove = 1 + cacador.bonusMove;
		bonusJump = 1 + cacador.bonusJump;
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
