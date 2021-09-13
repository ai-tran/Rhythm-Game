using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public Image arrow;
    public Image background;

    //abstract this out when for skins
    public Sprite pressedBg;
    public Sprite defaultBg;

    public int damage;
    public Direction direction;

    private bool isPressed;
    public bool IsPressed
    {
        get { return isPressed; }
        set 
        { 
            isPressed = value;
            SetBackground(value);
        }
    }

    private void SetBackground(bool isComplete)
    {
        background.sprite = isComplete
            ? pressedBg
            : defaultBg;
    }

    public void Init(Direction direction)
    {
        background.sprite = defaultBg;

        switch (direction)
        {
            case Direction.Down:
                arrow.transform.Rotate(0, 0, 90);
                break;
            case Direction.Up:
                arrow.transform.Rotate(0, 0, -90);
                break;
            case Direction.Left:
                arrow.transform.Rotate(0, 0, 0);
                break;
            case Direction.Right:
                arrow.transform.Rotate(0, 0, 180);
                break;
        }
    }
}
