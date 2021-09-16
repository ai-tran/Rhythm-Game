using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skin", order = 1)]
public class Skin : ScriptableObject
{
    public Sprite perfectText;
    public Sprite missText;
    public Sprite arrow;
    public Sprite arrowPressedBg;
    public Sprite arrowDefaultBg;
}
