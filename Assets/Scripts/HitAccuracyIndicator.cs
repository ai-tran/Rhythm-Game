using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitAccuracyIndicator : MonoBehaviour
{
    public SpriteRenderer accuracySprite;
    public AudioSource soundFx;

    [Header("Punch Scale Settings")]
    public float scaleSize = 0.3f;
    public float duration = 0.2f;
    public int vibrato = 5;
    public float elasticty = 1f;

    private Skin currentSkin;

    public void Init(HitAccuracy accuracyType)
    {
        currentSkin = GameManager.Instance.currentSkin;

        switch (accuracyType)
        {
            case HitAccuracy.Perfect:
                accuracySprite.sprite = currentSkin.perfectText;
                soundFx.clip = currentSkin.perfectSfx;
                break;
            case HitAccuracy.Great:
                accuracySprite.sprite = currentSkin.greatText;
                soundFx.clip = currentSkin.perfectSfx;
                break;
            case HitAccuracy.Cool:
                accuracySprite.sprite = currentSkin.coolText;
                soundFx.clip = currentSkin.perfectSfx;
                break;
            case HitAccuracy.Miss:
                accuracySprite.sprite = currentSkin.missText;
                soundFx.clip = currentSkin.missSfx;
                break;
        }

        soundFx.Play();
        transform.DOPunchScale(new Vector3(scaleSize, scaleSize, scaleSize), duration, vibrato, elasticty);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
