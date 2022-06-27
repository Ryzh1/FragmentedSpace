using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collision2D))]
public class Item : MonoBehaviour
{
    public SpriteRenderer IndicatorSpriteRenderer;
    public Sprite correct;
    public Sprite incorrect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Silent"))
        {
            var rotation = Quaternion.FromToRotation(Vector2.up, collision.contacts[0].normal);
            Instantiate(Resources.Load("DustParticle") as GameObject, collision.contacts[0].point, rotation);
        }
        
    }
    public void SetIndicator(bool? isCorrect)
    {
        if (!isCorrect.HasValue)
        {
            IndicatorSpriteRenderer.sprite = null;
        }
        else if (isCorrect.Value)
        {
            IndicatorSpriteRenderer.sprite = correct;
        }
        else
        {
            IndicatorSpriteRenderer.sprite = incorrect;
        }
    }
}
