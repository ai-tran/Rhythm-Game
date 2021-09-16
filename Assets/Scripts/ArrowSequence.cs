using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    public delegate bool SetCompleteAction();
    public static event SetCompleteAction InSetComplete;

    //list of directions to spawn arrows, this is set in the MoveSetClass
    public List<Direction> arrowSet = new List<Direction>();

    //list of arrow in this sequence
    private List<Arrow> arrows = new List<Arrow>();

    private bool isActiveSet = true;
    private int sequenceIndex = 0;

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

    void ProcessArrowKey(Direction direction)
    {

    }

    //Reset all arrows to not pressed
    public void ResetSequence()
    {
        arrows.ForEach(arrow => arrow.IsPressed = false);
    }

    public bool IsSequenceComplete()
    {
        return arrows.All(arrow => arrow.IsPressed);
    }
}
