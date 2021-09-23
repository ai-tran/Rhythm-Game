using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/Skin", order = 1)]
public class Skin : ScriptableObject
{
    [Header("General Appearance")]
    public Sprite perfectText;
    public Sprite greatText;
    public Sprite coolText;
    public Sprite missText;
    public AudioClip perfectSfx;
    public AudioClip coolSfx;
    public AudioClip missSfx;

    [Header("Move Set")]
    public Sprite arrow;
    public Sprite arrowPressedBg;
    public Sprite arrowDefaultBg;
}
