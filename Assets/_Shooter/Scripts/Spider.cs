using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, PausedObject
{
    private GameObject structure;
    private bool pause = false;

    void Start ()
    {
        structure = GameObject.Find("Structure");
	}
	
	void Update ()
    {
        if (pause) return;
        transform.LookAt(structure.transform);
        transform.position = Vector3.MoveTowards(transform.position, structure.transform.position, 0.001f);
    }

    public void PauseOnOff(bool onOff)
    {
        pause = onOff;
    }
}
