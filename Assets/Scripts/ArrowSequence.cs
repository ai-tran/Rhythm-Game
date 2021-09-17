using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    public delegate bool SetCompleteAction();
    public static event SetCompleteAction OnSetComplete;

    //list of arrow in this sequence
    public List<Arrow> arrows = new List<Arrow>();

    public bool isActiveSet = true;
    //How far are we in move set
    public int sequenceIndex = 0;

    public void OnEnable()
    {
        EventManager.OnArrowKeyPress += ProcessArrowKey;
    }

    public void OnDisable()
    {
        EventManager.OnArrowKeyPress -= ProcessArrowKey;
    }

    public void Init(GameObject arrowPrefab, List<Direction> directions)
    {
        //Instantiate and sets up arrow directions
        foreach (Direction direction in directions)
        {
            var temp = Instantiate(arrowPrefab, transform);
            temp.GetComponent<Arrow>().Init(direction);
            arrows.Add(temp.GetComponent<Arrow>());
        }
    }

    /// <summary>
    /// Check if player arrow key direction matches with set
    /// </summary>
    /// <param name="direction"></param>
    void ProcessArrowKey(Direction direction)
    {
        if (isActiveSet)
        {
            if (arrows[sequenceIndex].direction == direction)
            {
                arrows[sequenceIndex].IsPressed = true;
                sequenceIndex++;
                return;
            }
            ResetSequence();
        }
    }


    /// <summary>
    /// Clears state of move set used when user incorrectly presses wrong input
    /// </summary>
    public void ResetSequence()
    {
        arrows.ForEach(arrow => arrow.IsPressed = false);
        isActiveSet = false;
        sequenceIndex = 0;
    }

    public bool IsSequenceComplete()
    {
        return arrows.All(arrow => arrow.IsPressed);
    }
}
