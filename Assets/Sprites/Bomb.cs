using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float ForcePower = 10f;
    private CircleCollider2D circleCollider;
    private bool canExplode = true;
    // Start is called before the first frame update
    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    public void Explode()
    {
        if (!canExplode)
        {
            return;
        }
        canExplode = false;
        List<Collider2D> colliders = new List<Collider2D>();
        circleCollider.OverlapCollider(new ContactFilter2D(), colliders);
        var rigidBodies = colliders.Where(x => x.GetComponent<Rigidbody2D>() != null);
        foreach (var rigid in rigidBodies)
        {

            if (rigid.gameObject.tag == "Bomb" && !rigid.gameObject.Equals(gameObject))
            {
                var bomb = rigid.GetComponent<Bomb>();
                if (bomb != null)
                {
                    bomb.Explode();
                }
            }
            else if(rigid.gameObject.tag != "Bomb")
            {
                rigid.GetComponent<Rigidbody2D>().AddForce((rigid.transform.position - transform.position).normalized * ForcePower);
            }
           
        }
        //instantiate explosion object with play on awake sound, that deletes self after delay
        Instantiate(Resources.Load("Explosion") as GameObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
