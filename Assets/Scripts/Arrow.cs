using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public Image arrow;
    public Image background;

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

    private Skin currentSkin;
    private void Awake()
    {
        currentSkin = GameManager.Instance.currentSkin;
    }

    private void SetBackground(bool isComplete)
    {
        background.sprite = isComplete
            ? currentSkin.arrowPressedBg
            : currentSkin.arrowDefaultBg;

        if (isComplete)
        {
            transform.DOPunchScale(transform.localScale * 0.4f, 0.2f, 6, 1);
        }
    }

    public void Init(Direction direction)
    {
        background.sprite = currentSkin.arrowDefaultBg;
        this.direction = direction;
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

    private void OnDestroy()
    {
        DOTween.Clear();
    }
}
