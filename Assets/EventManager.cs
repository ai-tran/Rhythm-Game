using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void BeatHitAction();
    public static event BeatHitAction OnBeatHit;

    public delegate void ArrowKeyAction(Direction direction);
    public static event ArrowKeyAction OnArrowKeyPress;

    // Update is called once per frame
    void Update()
    {
        //maybe abstract input to player input manager
        //will also need to abstract keycode in the future for keyboard binding maybe?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBeatHit();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnArrowKeyPress(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnArrowKeyPress(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnArrowKeyPress(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnArrowKeyPress(Direction.Down);
        }
    }
}
