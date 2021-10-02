using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public Transform character;
    public HealthBar healthBar;

    public float currentHeath = 0;
    public float maxHeath = 100;

    void Start()
    {
        currentHeath = maxHeath;
    }

    public void OnHitAttack(float damage)
    {
        character.DOPunchPosition(new Vector3(0.5f,0.5f,0.5f), 0.3f, 10, 0.5f);
        currentHeath = currentHeath - damage;
        float percent =  currentHeath / maxHeath;
        print(percent);
        healthBar.BarPercent = percent;
    }
}
