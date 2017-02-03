using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, PausedObject
{
    public GameObject spider;
    private int cooldown = 200;
    private int inc = 0;
    private float range = 0.25f;
    private bool pause = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (pause) return;
        if (inc > cooldown)
        {
            inc = 0;
            if (cooldown > 0) cooldown--;
            float x, y;
            if (FlipACoin())
            {
                x = (FlipACoin() ? 1 : -1) * range;
                y = Random.Range(-100, 101) / (float)100 * range;
            }
            else
            {
                x = Random.Range(-100, 101) / (float)100 * range;
                y = (FlipACoin() ? 1 : -1) * range;
            }
            Instantiate(spider, new Vector3(x, 0, y), Quaternion.identity, transform);
        }
        inc++;
	}

    private bool FlipACoin()
    {
        return Random.Range(0, 2) == 0;
    }

    public void PauseOnOff(bool onOff)
    {
        pause = onOff;
    }
}
