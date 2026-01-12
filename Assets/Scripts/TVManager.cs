using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    public VideoPlayer video;
    public bool enter;
    bool isPlaying;


    // Update is called once per frame
    void Update()
    {
        if (enter)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                isPlaying = !isPlaying;
            }
        }
        if (isPlaying)
        {
            video.Play();
        }
        else
        {
            video.Pause();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        enter = true;
    }
    private void OnTriggerExit(Collider other)
    {
        enter = false;
    }
}
