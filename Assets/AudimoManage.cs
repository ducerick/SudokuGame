using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudimoManage : MonoBehaviour
{
    public AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Audio.Play();
    }

    private void OnDisable()
    {
        Audio.Pause();
    }
}
