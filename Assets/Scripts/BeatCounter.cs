using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCounter : MonoBehaviour
{
    public BeatCount[] beatCounts { get; set; }
    private Skin CurrentSkin
    {
        get
        {
            return GameManager.Instance.currentSkin;
        }
    }

    /// <param name="beatHitMarker">Which beat to set the marker</param>
    /// <param name="beatCount">how many beat counts to spawn</param>
    public void Init(int beatHitMarker, int beatCount)
    {
        SpawnBeatCount(beatCount);
        beatCounts = transform.GetComponentsInChildren<BeatCount>();
        beatCounts[beatHitMarker - 1].SetHitMarker();
    }

    public void SpawnBeatCount(int beatCount)
    { 
        for(int i = 0; i < beatCount; i++)
        {
            Instantiate(CurrentSkin.beatCountPrefab,transform);
        }
    }
}
