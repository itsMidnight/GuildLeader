using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int force;
    private SpriteRenderer spRend;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
        var multiplier = Random.Range(0.5f, 2f);
        rb2d.AddForce(new Vector2(force * multiplier, 0));
    }
}