using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm = 96;
    //The number of seconds for each song beat
    public float secPerBeat;
    //Current song position, in seconds
    public float songPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    //How many seconds have passed since the song started
    public float dspSongTime;
    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public Slider slider;


    //The offset to the first beat of the song in seconds
    public float firstBeatOffset = 3.90f;


    public float prevBeat;
    public float nextBeat;
    public float beatindex = 1;
    float beatPerSlide = 4;
    public float beatPercent;
    public float interp;

    // Start is called before the first frame update

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        //Start the music
        musicSource.Play();

        prevBeat = secPerBeat;
        nextBeat = secPerBeat * beatPerSlide;
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        //print("song pos " + songPosition);
        //print("song pos in beats: " + songPositionInBeats);

        beatPercent = (songPosition - prevBeat) / (nextBeat - prevBeat);

        interp = Mathf.Lerp(0, 1, beatPercent);

        if(songPosition > nextBeat)
        {
            beatindex++;
            prevBeat = nextBeat;
            nextBeat = secPerBeat * (4 * beatindex);
        }

        slider.value = beatPercent;
    }
}
