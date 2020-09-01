using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] TriggerAction;

    private bool[] TriggerActionChecker;

    // Start is called before the first frame update
    void Start()
    {
        TriggerActionChecker = new bool[TriggerAction.Length];
        for (int i = 0; i < TriggerActionChecker.Length; i++)
        {
            TriggerActionChecker[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
