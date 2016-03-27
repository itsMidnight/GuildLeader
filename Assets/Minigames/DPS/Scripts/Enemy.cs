using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int force;
    private SpriteRenderer skin;
    private SpriteRenderer suit;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        var spRends = GetComponentsInChildren<SpriteRenderer>();
        skin = spRends[0];
        suit = spRends[1];
        skin.sprite = Resources.LoadAll<Sprite>("Sprites/Guildies")[(int)Random.Range(0, 4.99999f)];
        suit.sprite = Resources.LoadAll<Sprite>("Sprites/Guildies")[(int)Random.Range(18, 28.99999f)];
        var multiplier = Random.Range(0.5f, 2f);
        rb2d.AddForce(new Vector2(force * multiplier, 0));
    }
}