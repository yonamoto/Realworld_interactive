using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shomen : MonoBehaviour
{
    private Vector3 magnetic;
    private ParticleSystem.MainModule ps_main;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {

        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps_main = ps.main;
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio_source.volume = Mathf.Min( ps_main.startSize.constant, 40) / 40f;

    }
}
