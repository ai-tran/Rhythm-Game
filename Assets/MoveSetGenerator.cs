using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetGenerator : MonoBehaviour
{
    public int MoveSetCount;
    //amount of moves, amount of arrow per move, list of directions
    List<ArrowSequence> arrowSequences = new List<ArrowSequence>();

    [Header("Move Set Assets")]
    public GameObject arrowPrefab;

    private void OnEnable()
    {
        EventManager.OnArrowKeyPress += ProcessArrowSequence;
    }

    private void OnDisable()
    {
        EventManager.OnArrowKeyPress -= ProcessArrowSequence;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ProcessArrowSequence(Direction direction)
    {
        for(int i = 0; i < arrowSequences.Count; i++)
        {
            if(direction == arrowSequences[i].arrowSet[0])
            {
                arrowSequences[i].isActiveSet = true;
                return;
            }
        }
    }
}
