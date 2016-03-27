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

    public enum CharacterSuit
    {
        A = 18,
        B = 19,
        C = 20,
        D = 21,
        E = 22,
        F = 23,
        G = 24,
        H = 25,
        I = 26,
        J = 27,
        K = 28,
    }


    public int movementForce;
    private Rigidbody2D rigidBody;

    private Sprite[] characterSprites;
    private SpriteRenderer skin;
    private SpriteRenderer suit;

    public void Init()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        characterSprites = Resources.LoadAll<Sprite>("Sprites/Guildies");
        skin = spriteRenderers[0];
        suit = spriteRenderers[1];
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

    public CharacterSuit GetRandomSuit()
    {
        var suits = System.Enum.GetValues(typeof(Character.CharacterSuit));
        return (Character.CharacterSuit)suits.GetValue((int)Random.Range(0, suits.Length - 0.00001f));
    }

    public void MoveAcrossScreen(int force)
    {
        var multiplier = Random.Range(0.5f, 2f);
        rigidBody.AddForce(new Vector2(force * multiplier, 0));
    }

    public void InitializeLookAndFeel(CharacterSkinTone skintone, CharacterSuit suitStyle)
    {
        skin.sprite = characterSprites[(int)skintone];
        suit.sprite = characterSprites[(int)suitStyle];
    }


}