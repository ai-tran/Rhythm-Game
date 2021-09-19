using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCounter : MonoBehaviour
{

    public BeatCount[] beatCounters;

    /// <param name="beatHitMarker">Which beat to set the marker</param>
    /// <param name="beatCount">how many beat counts to spawn</param>
    public void Init(int beatHitMarker, int beatCount)
    {
        beatCounters = transform.GetComponentsInChildren<BeatCount>();
        beatCounters[beatHitMarker].SetHitMarker();
    }

    public void SpawnBeatCount(int beatCount)
    {
        for(int i = 0; i < beatCount; i++)
        {

        }
    }
}
