using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Todo dynamically the current character
    public Character currentCharacter;
    public Player player;
     
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        Conductor.OnCompleteBeatHit += OnAttackHit;
    }

    private void OnDisable()
    {
        Conductor.OnCompleteBeatHit -= OnAttackHit;
    }

    private void OnAttackHit()
    {
        currentCharacter.OnHitAttack();
    }
}
