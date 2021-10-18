using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class CombatManager : MonoBehaviour
{
    //Todo dynamically change to the current character
    public Character currentCharacter;
    public Player player;

    public CombatState state;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.Start;
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
        currentCharacter.OnHitAttack(player.attackDamage);
    }
}
