using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCounter : MonoBehaviour
{
    public int beatHitMarker;
    // Start is called before the first frame update
    public BeatCount[] beatCounters;
    void Awake()
    {
        beatCounters = transform.GetComponentsInChildren<BeatCount>();
        beatCounters[beatHitMarker].SetHitMarker();
    }
}
