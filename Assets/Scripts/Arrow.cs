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
    public GameManager.Direction direction;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SetBackground(bool isComplete)
    {
        if (isComplete)
        {
            background.sprite = pressedBg;
        }
        else
        {
            background.sprite = defaultBg;
        }
    }

    public void Init(GameManager.Direction direction)
    {
        background.sprite = defaultBg;

        switch (direction)
        {
            case GameManager.Direction.Down:
                arrow.transform.Rotate(0, 0, 90);
                break;
            case GameManager.Direction.Up:
                arrow.transform.Rotate(0, 0, -90);
                break;
            case GameManager.Direction.Left:
                arrow.transform.Rotate(0, 0, 0);
                break;
            case GameManager.Direction.Right:
                arrow.transform.Rotate(0, 0, 180);
                break;
        }
    }
}
