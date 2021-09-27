using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public Transform character;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitAttack()
    {
        character.DOPunchPosition(new Vector3(0.5f,0.5f,0.5f), 0.3f, 10, 0.5f);
    }
}
