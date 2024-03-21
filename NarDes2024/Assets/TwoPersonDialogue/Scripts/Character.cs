using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Create New Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite talkingSprite;
    public Color nameColor;
    public Color textColor;
}
