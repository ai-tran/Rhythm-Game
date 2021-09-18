using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    //list of arrow in this sequence
    public List<Arrow> arrows = new List<Arrow>();

    public bool IsActiveSet { get; set; } = true;

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
            var arrow = Instantiate(arrowPrefab, transform).GetComponent<Arrow>();
            arrow.Init(direction);
            arrows.Add(arrow);
        }
    }

    /// <summary>
    /// Check if player arrow key direction matches with set
    /// </summary>
    /// <param name="direction"></param>
    private void ProcessArrowKey(Direction direction)
    {
        if (IsActiveSet && !IsSequenceComplete())
        {
            if (arrows[sequenceIndex].direction == direction)
            {
                arrows[sequenceIndex].IsPressed = true;
                return;
            }
            IsActiveSet = false;
            ResetSequence();
        }
    }

    /// <summary>
    /// Clears state of move set used when user incorrectly presses wrong input
    /// </summary>
    public void ResetSequence()
    {
        arrows.ForEach(arrow => arrow.IsPressed = false);
        IsActiveSet = false;
        sequenceIndex = 0;
    }

    public bool IsSequenceComplete()
    {
        return arrows.All(arrow => arrow.IsPressed);
    }
}
