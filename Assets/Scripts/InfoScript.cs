using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InfoScript : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> Sprites;
    public GoalZone GoalZoneObjectWithList;
    public float CycleTime = 1f;
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;
    private Color _startColor;
    private int lastChoseSprite;
    private bool isCycling;
    private Vector3 _scale;
    private bool mouseHasExited = true;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _sprite = _spriteRenderer.sprite;
        _scale = transform.localScale;
        if(GoalZoneObjectWithList != null)
        {
            Sprites = GoalZoneObjectWithList.objectsNeeded.Select(x => x.GetComponent<SpriteRenderer>().sprite).Distinct().ToList();
        }
        
        if (!Sprites.Any())
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (mouseHasExited)
        {
            _spriteRenderer.color = _startColor;
            _spriteRenderer.sprite = _sprite;
            transform.localScale = _scale;
        }
    }
    private void OnMouseEnter()
    {
        mouseHasExited = false;
        _audioSource.Play();
        var newColour = _startColor;
        newColour.a = 255;
        _spriteRenderer.color = newColour;
        transform.localScale = _scale * 1.1f;
    }
    private void OnMouseExit()
    {
        mouseHasExited = true;
    }
    private void OnMouseOver()
    {
        if (!isCycling)
        {
            isCycling = true;
            StartCoroutine(CycleSprites(CycleTime));
        }
    }
    IEnumerator CycleSprites(float time)
    {
        StopCoroutine(CycleSprites(time));
        if (!isCycling)
        {
            yield return null;
        }
        yield return new WaitForSeconds(time);
       
        if(lastChoseSprite < Sprites.Count)
        {
            _spriteRenderer.sprite = Sprites[lastChoseSprite];
        }
        else
        {
            lastChoseSprite = 0;
            _spriteRenderer.sprite = Sprites[0];
        }
        lastChoseSprite++;
        isCycling = false;
    }
}
