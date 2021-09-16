using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    public ArrowSequence arrowContainer;

    //Will have to expand this to save more info on player input
    public List<Direction> playerInput;

    List<Direction> temp = new List<Direction>();

    // Start is called before the first frame update
    void Start()
    {
        //temp testing spawning arrow 
        //todo randomly generate arrows
        temp.Add(Direction.Down);
        temp.Add(Direction.Up);
        temp.Add(Direction.Left);
        temp.Add(Direction.Right);

        arrowContainer.Init(temp);
    }

    // Update is called once per frame
    void Update()
    {
        //check if our player input matches with the current set
        //if we reset it so the player can start over
        for(int i = 0; i < playerInput.Count; i++)
        {
            if(playerInput[i] != temp[i])
            {
                arrowContainer.ResetSequence();
                playerInput.Clear();
            }
            else
            {
                arrowContainer.arrows[i].IsPressed = true;
            }
        }
    }


}
