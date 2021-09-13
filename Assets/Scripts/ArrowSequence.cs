using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    //temp move this in the future, what arrow prefabs to spawn in set
    public GameObject arrowPrefab;
    //list of directions to spawn arrows, this is set in the MoveSetClass
    public List<Direction> arrowSet = new();
    //list of arrow in this sequence
    public List<Arrow> arrows = new();

    public void Init(List<Direction> directions)
    {
        //Instantiate and sets up arrow directions
        foreach (Direction direction in directions)
        {
            var temp = Instantiate(arrowPrefab, transform);
            temp.GetComponent<Arrow>().Init(direction);
            arrows.Add(temp.GetComponent<Arrow>());
        }
    }

    //Reset all arrows to not pressed
    public void ResetSequence()
    {
        arrows.ForEach(arrow => arrow.IsPressed = false);
    }

    //Check if all arrows in sequence are pressed
    public bool IsSetComplete()
    {
        return arrows.All(arrow => arrow.IsPressed);
    }
}
