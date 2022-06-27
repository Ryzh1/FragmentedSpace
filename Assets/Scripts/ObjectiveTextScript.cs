using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveTextScript : MonoBehaviour
{
    public string ObjectiveText;
    public float TimeBetweenCharacters = 0.1f;
    public GameObject Player;
    private TextMeshProUGUI text;
    private char[] charText;
    private int currentChar;
    private bool writingText;
    private Animation textFade;
    private Shooting shooting;
    private bool skipped;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        charText = ObjectiveText.ToCharArray();
        textFade = GetComponent<Animation>();
        shooting = Player.GetComponentInChildren<Shooting>();
        shooting.enabled = false;
        //shooting.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = charText.Length > 0 ? "Objective: " : string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0) && !skipped && !shooting.enabled) || (string.IsNullOrEmpty(ObjectiveText) && !skipped && !shooting.enabled))
        {
            skipped = true;
            StopAllCoroutines();
            if(!string.IsNullOrEmpty(ObjectiveText))
            {
                text.text = "Objective: " + ObjectiveText;
            }
            
            textFade.Play();
            //shooting.enabled = true;
            //Destroy(gameObject);
        }
        if(currentChar < charText.Length && !writingText && !skipped)
        {
            StartCoroutine(TypeCharacter(0.1f));
        }
        else if(currentChar >= charText.Length && !writingText && textFade.clip != null && !skipped)
        {
            textFade.Play();
        }
    }

    IEnumerator TypeCharacter(float seconds)
    {
        writingText = true;
        yield return new WaitForSeconds(seconds);
        text.text += charText[currentChar];
        currentChar++;
        writingText = false;
    }

    public void EndAnimation()
    {
        textFade.RemoveClip(textFade.clip);
        text.color = Color.white;
        text.fontSize *= 4f;
        text.text = "Start!";
        shooting.enabled = true;
        StartCoroutine(RemoveStart(1f));
    }
    IEnumerator RemoveStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.text = string.Empty;
        skipped = true;
    }
}
