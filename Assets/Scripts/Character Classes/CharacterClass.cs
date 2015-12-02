using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	public string name { get; set; }
	public string description { get; set; }
	public int level { get; set; }
	public int exp { get; set; }
	public int bonusStrength { get; set; }
	public int bonusDexterity { get; set; }
	public int bonusAgility { get; set; }
	public int bonusConstitution { get; set; }
	public int bonusWisdom { get; set; }
	public int bonusLife { get; set; }
	public int bonusFaith { get; set; }
	public int bonusFury { get; set; }
	public int bonusPersistance { get; set; }
	public int bonusAttack { get; set; }
	public int bonusResCut { get; set; }
	public int bonusResPierce { get; set; }
	public int bonusResBlunt { get; set; }
	public int bonusResMoral { get; set; }
	public int bonusMove { get; set; }
	public int bonusJump { get; set; }
	public int bonusReach { get; set; }
	

	public virtual void LevelUp() {

	}

	public void Print() {
		print("LV " + level + "-------------------------------------------------------------------");
		print("EXP " + exp);
		print("FOR " + bonusStrength);
		print("DES " + bonusDexterity);
		print("AGI " + bonusAgility);
		print("CON " + bonusConstitution);
		print("SAB " + bonusWisdom);
		print("VIDA " + bonusLife);
		print("FÉ " + bonusFaith);
		print("FUR " + bonusFury);
		print("PER " + bonusPersistance);
		print("ATQ " + bonusAttack);
		print("RESC " + bonusResCut);
		print("RESP " + bonusResPierce);
		print("RESB " + bonusResBlunt);
		print("RESM " + bonusResMoral);
	}

	
}
