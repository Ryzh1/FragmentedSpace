using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    public float MinSpeedToDestroy = 0.5f;
    public LayerMask layerMask;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var spr = GetComponent<SpriteRenderer>();
        var col = Random.ColorHSV(0,1,1,1,0.7f,1,1,1);
        spr.color = col;
        var trail = GetComponent<TrailRenderer>();
        trail.material.color = col;
        var light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light.color = col;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        //if(Mathf.Abs(rb.velocity.magnitude) < MinSpeedToDestroy)
        //{
        //    Destroy(gameObject);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Silent"))
        {
            var rotation = Quaternion.FromToRotation(Vector2.up, collision.contacts[0].normal);
            Instantiate(Resources.Load("DustParticle") as GameObject, collision.contacts[0].point, rotation);
        }
    }
}
