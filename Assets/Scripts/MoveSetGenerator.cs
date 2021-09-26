using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MoveSetGenerator : MonoBehaviour
{
    //amount of moves, amount of arrow per move, list of directions
    readonly List<ArrowSequence> arrowSequences = new List<ArrowSequence>();

    [Header("Move Set Assets")]
    public GameObject arrowPrefab;
    public GameObject arrowSequencePrefab;
    public float verticalPadding = 0;

    public bool IsAnySequenceComplete
    {
        get
        {
            if (arrowSequences != null)
            {
                return arrowSequences.Any(d => d.IsSequenceComplete);
            }
            return false;
        }
    }
    public int sequenceIndex { get; set; } = -1;

    private void OnEnable()
    {
        EventManager.OnArrowKeyPress += ProcessArrowSequence;
    }

    private void OnDisable()
    {
        EventManager.OnArrowKeyPress -= ProcessArrowSequence;
    }

    public MoveSetGenerator GenerateMoveSet(int sequenceCount, int arrowCount)
    {
        foreach (ArrowSequence child in arrowSequences)
        {
            Destroy(child.gameObject);
        }
        arrowSequences.Clear();
        sequenceIndex = -1;

        for (int i = 0; i < sequenceCount; i++)
        {
            Vector3 position = new Vector3(0, i * verticalPadding + transform.position.y, 0);
            var seq = Instantiate(arrowSequencePrefab,position,Quaternion.identity,transform).GetComponent<ArrowSequence>();
            seq.Init(arrowPrefab, RandomDirectionsList(arrowCount));
            arrowSequences.Add(seq);
        }

        return this;
    }
    public MoveSetGenerator OnComplete(Action action)
    {
        action.Invoke();
        return this;
    }

    /// <summary>
    /// Get random list of directions
    /// </summary>
    /// <param name="listSize">how many items in list</param>
    /// <returns></returns>
    List<Direction> RandomDirectionsList(int listSize)
    {
        List<Direction> dir = new List<Direction>(listSize);
        for (int i = 0; i < listSize; i++)
        {
            // make 3 a variable
            Direction randDir = (Direction)UnityEngine.Random.Range(0, 3);
            dir.Add(randDir);
        }
        return dir;
    }
    private void ProcessArrowSequence(Direction direction)
    {
        bool isNoneActive = arrowSequences.All(a => !a.IsActiveMoveSet);

        if (isNoneActive)
        {
            sequenceIndex = -1;
            for(int i = 0; i < arrowSequences.Count; i++)
            {
                arrowSequences[i].ResetSequence();
                arrowSequences[i].IsActiveMoveSet = true;
            }
        }

        sequenceIndex++;
    }

}
