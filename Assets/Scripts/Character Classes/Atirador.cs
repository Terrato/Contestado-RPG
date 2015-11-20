using UnityEngine;
using System.Collections;

public class Atirador: CharacterClass {


	public Atirador() { }

	public Atirador(Cacador cacador) {
		name = "Atirador";
		description = "Os desempregados são os recrutas iniciais do Exército " +
						"antes de serem alocados à uma função específica.\n" +
						"Eles perderam seus empregos e bens, porém jamais sua fé," +
						" o que causou sua filiação com João Maria.";
		level = 1;
		exp = 0;
		bonusStrength += 0 + cacador.bonusStrength;
		bonusDexterity += 7 + cacador.bonusDexterity;
		bonusAgility += 5 + cacador.bonusAgility;
		bonusConstitution += 2 + cacador.bonusConstitution;
		bonusWisdom += 4 + cacador.bonusWisdom;
		bonusLife += 35 + cacador.bonusLife;
		bonusFaith += 30 + cacador.bonusFaith;
		bonusFury = 0;
		bonusPersistance = 0;
		bonusAttack += 20 + cacador.bonusAttack;
		bonusResCut += 2 + cacador.bonusResCut;
		bonusResPierce += 2 + cacador.bonusResPierce;
		bonusResBlunt += 2 + cacador.bonusResBlunt;
		bonusResMoral += 2 + cacador.bonusResMoral;
		bonusMove = 0 + cacador.bonusMove;
		bonusJump = 1 + cacador.bonusJump;
	}

	public override void LevelUp() {
		if (level < 99) {
			if (exp >= 99) {
				level += 1;
				exp -= 99;
				bonusDexterity++;
				if (level % 2 == 0) {
					bonusLife++;
				}
				if (level % 4 == 0) {
				}
				if (level % 6 == 0) {
					bonusAgility++;
					bonusConstitution++;
					bonusStrength++;
					bonusFaith += 3;
				}
				if (level % 10 == 0) {
					bonusWisdom++;
				}
			}
		}
	}
}
