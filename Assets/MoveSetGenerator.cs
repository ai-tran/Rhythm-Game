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
    public GameObject arrowSequencePrefab;
    public Transform moveSetParent;

    //How many sequences to spawn
    public int sequenceCount;
    //arrow many arrows to increment by per sequence
    public int arrowIncrement;
    //How many arrows per set
    public int arrowCount;

    private void OnEnable()
    {
        EventManager.OnArrowKeyPress += ProcessArrowSequence;
    }

    private void OnDisable()
    {
        EventManager.OnArrowKeyPress -= ProcessArrowSequence;
    }

    private void Start()
    {
        InitMoveSet();
    }

    void InitMoveSet()
    {
        SpawnSequence();
    }

    void SpawnSequence()
    {
        for (int i = 0; i < sequenceCount; i++)
        {
            var seq = Instantiate(arrowSequencePrefab, moveSetParent).GetComponent<ArrowSequence>();
            seq.Init(arrowPrefab, RandomDirectionsList(arrowCount));
            arrowSequences.Add(seq);
        }
    }

    /// <summary>
    /// Get random list of directions
    /// </summary>
    /// <param name="listSize">how many items in list</param>
    /// <returns></returns>
    List<Direction>RandomDirectionsList(int listSize)
    {
        List<Direction> dir = new List<Direction>(listSize);
        for(int i = 0; i < listSize; i++)
        {
            Direction randDir = (Direction)Random.Range(0, 3);
            dir.Add(randDir);
        }
        return dir;
    }

    void ProcessArrowSequence(Direction direction)
    {
        for (int i = 0; i < arrowSequences.Count; i++)
        {
            if (direction == arrowSequences[i].arrows[0].direction)
            {
                arrowSequences[i].isActiveSet = true;
                return;
            }
        }
    }
}
