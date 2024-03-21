using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.TextCore.Text;

public class SpeakerName : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public TextMeshProUGUI text;
    public Image talkingSprite;

    [HideInInspector] public Character character;
    [HideInInspector] public bool active = false;

    public void SetName(Character setCharacter)
    {
        character = setCharacter;
        text.text = character.characterName;
        text.color = character.nameColor;
        talkingSprite.sprite = character.talkingSprite;
        talkingSprite.color = Color.white;
    }
    
    public void StartSpeaking()
    {
        if (!active)
        {
            animator.SetTrigger("Activate");
            active = true;
        }
    }

    public void StopSpeaking()
    {
        if (active)
        {
            animator.SetTrigger("Deactivate");
            active = false;
        }
    }

    public void RemoveName()
    {
        text.text = "";
        talkingSprite.color = Color.clear;
        talkingSprite.sprite = null;
        character = null;
    }
}
