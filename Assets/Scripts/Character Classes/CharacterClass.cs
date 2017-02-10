using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	public Sprite charFace, charBody, charAttack;
	public BaseSkill[] classSkills;
	public string name { get; set; }
	public string weapon { get; set; }
	public string head { get; set; }
	public string body { get; set; }
	public string feet { get; set; }
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
	public string[] weaponArray { get; set; }
	public string[] headArray { get; set; }
	public string[] bodyArray { get; set; }
	public string[] feetArray { get; set; }

	public virtual void LevelUp() {

	}

	public void PickEquipments() {
		string pickedWeapon = weaponArray[Random.Range(0, weaponArray.Length)];
		weapon = pickedWeapon;
		string pickedHead = headArray[Random.Range(0, headArray.Length)];
		head = pickedHead;
		string pickedBody = bodyArray[Random.Range(0, bodyArray.Length)];
		body = pickedBody;
		string pickedFeet = feetArray[Random.Range(0, feetArray.Length)];
		feet = pickedFeet;
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
