using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public enum CharacterSkinTone
    {
        Lightest = 0,
        Light = 1,
        Medium = 2,
        Dark = 3,
        Darkest = 4
    }

	public enum CharacterEyes
	{
		Blue = 5,
		Green = 6,
		Brown = 7,
		Orange = 8,
		Black = 9
	}

	public enum CharacterWeapon
	{
		SwordSheild = 10,
		Daggers = 11,
		Spear = 12,
		Bow = 13,
		Staff = 14,
		Magic = 15
	}

    public enum CharacterSuit
    {
        LightArmor1 = 16,
        MediumArmor1 = 17,
        HeavyArmor1 = 18,
        Mage1 = 19,
        Cleric1 = 20,
        Glamazon1 = 21,
        Ranger1 = 22,
        Ninja1 = 23,
        SuperNinja1 = 24,
        Mario1 = 25,
        Cape1 = 26,
		Baby1 = 27,

		LightArmor2 = 28,
		MediumArmor2 = 29,
		HeavyArmor2 = 30,
		Mage2 = 31,
		Cleric2 = 32,
		Glamazon2 = 33,
		Ranger2 = 34,
		Ninja2 = 35,
		SuperNinja2 = 36,
		Mario2 = 37,
		Cape2 = 38,
		Baby2 = 39,

		LightArmor3 = 40,
		MediumArmor3 = 41,
		HeavyArmor3 = 42,
		Mage3 = 43,
		Cleric3 = 44,
		Glamazon3 = 45,
		Ranger3 = 46,
		Ninja3 = 47,
		SuperNinja3 = 48,
		Mario3 = 49,
		Cape3 = 50,
		Baby3 = 51
    }


    public int movementForce;
    protected Rigidbody2D rigidBody;

    protected Sprite[] characterSprites;
    protected SpriteRenderer skin;
    protected SpriteRenderer suit;
    protected SpriteRenderer weapon;
    protected SpriteRenderer eyes;

    public virtual void Init()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		characterSprites = Resources.LoadAll<Sprite>("Sprites/Guildies");
        skin = spriteRenderers[0];
        suit = spriteRenderers[1];
		weapon = spriteRenderers[2];
		eyes = spriteRenderers[3];
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Init();
    }

    public void RegisterDirectHit()
    {
        rigidBody.gravityScale = 6.5F;
        rigidBody.AddForce(new Vector2(0, 100 * Random.Range(0.8f, 2.2f)));
        rigidBody.AddTorque(10 * Random.Range(2, 3), ForceMode2D.Force);
    }

    public CharacterSkinTone GetRandomSkinTone()
    {
        var skinTones = System.Enum.GetValues(typeof(Character.CharacterSkinTone));
        return (Character.CharacterSkinTone)skinTones.GetValue((int)Random.Range(0, skinTones.Length - 0.00001f));
    }

	public CharacterSkinTone GetSpecificSkinTone(int skinValue)
	{
		var skinTones = System.Enum.GetValues(typeof(Character.CharacterSkinTone));
		return (Character.CharacterSkinTone)skinTones.GetValue(skinValue);
	}

    public CharacterSuit GetRandomSuit()
    {
        var suits = System.Enum.GetValues(typeof(Character.CharacterSuit));
        return (Character.CharacterSuit)suits.GetValue((int)Random.Range(0, suits.Length - 0.00001f));
    }

	public CharacterSuit GetSpecificSuit(int suitValue)
	{
		var suits = System.Enum.GetValues(typeof(Character.CharacterSuit));
		return (Character.CharacterSuit)suits.GetValue(suitValue);
	}

	public CharacterWeapon GetRandomWeapon()
	{
		var weapons = System.Enum.GetValues(typeof(Character.CharacterWeapon));
		return (Character.CharacterWeapon)weapons.GetValue((int)Random.Range(0, weapons.Length - 0.00001f));
	}

	public CharacterWeapon GetSpecificWeapon(int weaponValue)
	{
		var weapons = System.Enum.GetValues(typeof(Character.CharacterWeapon));
		return (Character.CharacterWeapon)weapons.GetValue(weaponValue);
	}

	public CharacterEyes GetRandomEyes()
	{
		var eyes = System.Enum.GetValues(typeof(Character.CharacterEyes));
		return (Character.CharacterEyes)eyes.GetValue((int)Random.Range(0, eyes.Length - 0.00001f));
	}

	public CharacterEyes GetSpecificEyes(int eyeValue)
	{
		var eyes = System.Enum.GetValues(typeof(Character.CharacterEyes));
		return (Character.CharacterEyes)eyes.GetValue(eyeValue);
	}

    public void MoveAcrossScreen(int force)
    {
        var multiplier = Random.Range(0.75f, 1.5f);
        rigidBody.AddForce(new Vector2(force * multiplier, 0));
    }

    public void InitializeLookAndFeel(CharacterSkinTone skintone, CharacterSuit suitStyle, CharacterWeapon weaponStyle, CharacterEyes eyeColor)
    {
        skin.sprite = characterSprites[(int)skintone];
        suit.sprite = characterSprites[(int)suitStyle];
		weapon.sprite = characterSprites[(int)weaponStyle];
		eyes.sprite = characterSprites[(int)eyeColor];
    }
}