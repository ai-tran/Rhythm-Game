using DG.Tweening;
using RhythmTool;
using System.Collections.Generic;
using UnityEngine;

public class BeatSlider : MonoBehaviour
{
    //public float speed = 0.5f;

    //temp so I can visualize beats
    public Transform pic;

    public RhythmAnalyzer analyzer;
    public AudioSource audioSource;
    public RhythmData rhythmData;

    private float prevTime = 0;
    public List<Beat> beats;

    //temp change this to get children from parent
    public List<RectTransform> beatCounter;
    private int beatCounterIndex = 0;

    void Awake()
    {

        //get list of all beat markups in track
        beats = new List<Beat>();

        //Find a track with Beats.
        Track<Beat> track = rhythmData.GetTrack<Beat>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = audioSource.time;

        if(beatCounterIndex >= beatCounter.Count)
        {
            beatCounterIndex = 0;
        }

        //Clear the list.
        beats.Clear();
        //Find all beats for the part of the song that is currently playing.
        rhythmData.GetFeatures<Beat>(beats, prevTime, time);
        //Do something with the Beats here.
        foreach (Beat beat in beats)
        {
            beatCounter[beatCounterIndex].GetComponent<RectTransform>().DOScale(1.3f, 0.2f);
            
            if (beatCounterIndex != 0)
            {
                int previousIndex = beatCounterIndex - 1;
                beatCounter[previousIndex].GetComponent<RectTransform>().DOScale(1, 0.2f);
            }
            else
            {
                int lastIndex = beatCounter.Count -1;
                beatCounter[lastIndex].GetComponent<RectTransform>().DOScale(1, 0.2f);
            }

            beatCounterIndex++;
        }
        //Keep track of the previous playback time of the AudioSource.
        prevTime = time;

        if (Input.GetKeyDown(KeyCode.Space)){
            //temp move this from being hardcoded
            if(beatCounterIndex == 9)
            {
                print("Hit");
            }
            else
            {
                print("Miss");
            }
        }
    }

    public void OnBeat()
    {
        print("On beat");
        pic.transform.DOPunchScale(Vector3.up, 0.2f, 10, 1);
    }
}
