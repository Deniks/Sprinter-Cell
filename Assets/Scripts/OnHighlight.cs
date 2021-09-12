using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHighlight : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField]
    AudioClip sfx;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer");
        SoundManager.Instance.Play(sfx);
    }


    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("select");
        SoundManager.Instance.Play(sfx);
    }
}
