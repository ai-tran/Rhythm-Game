using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains and keeps track of Arrow in the sequence
/// </summary>
public class ArrowSequence : MonoBehaviour
{
    //temp move this in the future, what arrow prefabs to spawn in set
    public GameObject arrowPrefab;
    //list of directions to spawn arrows, this is set in the MoveSetClass
    public List<GameManager.Direction> arrowSet;
    //list of arrow in this sequence
    public List<Arrow> arrows;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(List<GameManager.Direction> directions)
    {
        //Instantiate and sets up arrow directions
        foreach (GameManager.Direction direction in directions)
        {
            var temp = Instantiate(arrowPrefab, this.gameObject.transform);
            temp.GetComponent<Arrow>().Init(direction);
            arrows.Add(temp.GetComponent<Arrow>());
        }
    }

    //Reset all arrows to not pressed
    public void ResetSequence()
    {
        foreach (Arrow arrow in arrows)
        {
            arrow.IsPressed = false;
        }
    }

    //Check if all arrows in sequence are pressed
    public bool IsSetComplete()
    {
        for(int i = 0; i < arrows.Count; i++)
        {
            if(arrows[i].IsPressed == false)
            {
                return false;
            }
        }
        return true;
    }
}
