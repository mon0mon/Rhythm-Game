using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    private bool Enable = true;

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }
    
    private void Update()
    {
        if (Enable)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    public void SetEnable(bool check)
    {
        Enable = check;
    }

    public bool GetEnable()
    {
        return Enable;
    }
}
