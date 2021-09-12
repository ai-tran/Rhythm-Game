using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    public ArrowSequence arrowContainer;

    //Will have to expand this to save more info on player input
    public List<GameManager.Direction> playerInput;

    List<GameManager.Direction> temp = new List<GameManager.Direction>();
    // Start is called before the first frame update
    void Start()
    {
        //temp testing spawning arrow 
        //todo randomly generate arrows
        temp.Add(GameManager.Direction.Down);
        temp.Add(GameManager.Direction.Up);
        temp.Add(GameManager.Direction.Left);
        temp.Add(GameManager.Direction.Right);

        arrowContainer.Init(temp);
    }

    // Update is called once per frame
    void Update()
    {
        //If arrow set is complete no longer need to store player input, might be temp eventually we want to record all player input
        if (arrowContainer.IsSetComplete())
        {
            playerInput.Clear();
        }else
        {
            //this is bad need to redo probably
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerInput.Add(GameManager.Direction.Up);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerInput.Add(GameManager.Direction.Left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerInput.Add(GameManager.Direction.Right);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerInput.Add(GameManager.Direction.Down);
            }
        }

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
