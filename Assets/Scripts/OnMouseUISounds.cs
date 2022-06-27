using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class OnMouseUISounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip MouseEnterClip = null;
    public AudioClip MouseExitClip = null;
    private AudioSource _audioSource;
    private Vector3 _originalScale;

    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _originalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.Stop();
        transform.localScale = _originalScale * 1.1f;
        if (MouseEnterClip != null)
        {
            _audioSource.clip = MouseEnterClip;
            _audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _audioSource.Stop();
        transform.localScale = _originalScale;
        if (MouseExitClip != null)
        {
            _audioSource.clip = MouseExitClip;
            _audioSource.Play();
        }
    }

}
