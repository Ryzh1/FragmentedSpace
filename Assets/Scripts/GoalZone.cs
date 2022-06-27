using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZone : MonoBehaviour
{
    public List<GameObject> objectsNeeded;
    List<Collider2D> results = new List<Collider2D>();
    public LayerMask layers;
    public bool GoalZoneValid = false;
    private SpriteRenderer spr;
    private Color originalColour;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        originalColour = spr.color;
    }
    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D() { layerMask = layers, useLayerMask = true }, results);
        var gameObjs = results.Select(x => x.gameObject).ToList();
        var validObjs = gameObjs.Intersect(objectsNeeded).ToList();
     

        foreach(var valid in validObjs)
        {
            var item = valid.GetComponent<Item>();
            if(item != null)
            {
                item.SetIndicator(true);
            }
        }
        validObjs.ForEach(x => x.GetComponent<Item>().SetIndicator(true));
        var validState = validObjs.Count() == objectsNeeded.Count() && objectsNeeded.Count == gameObjs.Count;

        if (validState)
        {
            var yellow = new Color(255,204,0, originalColour.a);
            spr.color = yellow;
            if (validObjs.All(x => x.GetComponent<Rigidbody2D>().velocity == Vector2.zero))
            {
                GoalZoneValid = true;
            }
            else
            {
                GoalZoneValid = false;
            }
        }
        else
        {
            GoalZoneValid = false;
            gameObjs.RemoveAll(x => validObjs.Contains(x));
            if (gameObjs.Any())
            {
                var red = Color.red;
                red.a = originalColour.a;
                spr.color = red;
                gameObjs.ForEach(x => x.GetComponent<Item>().SetIndicator(false));
            }
            else
            {
                spr.color = originalColour;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var script = collision.gameObject.GetComponent<Item>();
        if (script != null)
        {
            script.SetIndicator(null);
        }
    }
}
