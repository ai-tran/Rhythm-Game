using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    private MoveSetGenerator moveSetGenerator;
    //list of arrow in this sequence
    public List<Arrow> arrows = new List<Arrow>();

    public bool IsActiveMoveSet { get; set; } = true;
    
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
        moveSetGenerator = FindObjectOfType<MoveSetGenerator>();

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
        if (IsActiveMoveSet && !IsSequenceComplete())
        {
            if (arrows[moveSetGenerator.sequenceIndex].direction == direction)
            {
                arrows[moveSetGenerator.sequenceIndex].IsPressed = true;
                return;
            }
            ResetSequence();
            IsActiveMoveSet = false;
        }
    }

    /// <summary>
    /// Clears state of move set used when user incorrectly presses wrong input
    /// </summary>
    public void ResetSequence()
    {
        arrows.ForEach(arrow => arrow.IsPressed = false);
    }

    public bool IsSequenceComplete()
    {
        return arrows.All(arrow => arrow.IsPressed);
    }
}
