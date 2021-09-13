using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    public ArrowSequence arrowContainer;

    //Will have to expand this to save more info on player input
    public List<Direction> playerInput;

    List<Direction> temp = new();

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

    private void HandleInput()
    {
        //If arrow set is complete no longer need to store player input, might be temp eventually we want to record all player input
        if (arrowContainer.IsSetComplete())
        {
            playerInput.Clear();
            return;
        }

        //this is bad need to redo probably
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerInput.Add(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerInput.Add(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerInput.Add(Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerInput.Add(Direction.Down);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

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
