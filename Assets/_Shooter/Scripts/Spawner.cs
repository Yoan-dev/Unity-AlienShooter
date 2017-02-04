using UnityEngine;

public class Spawner : MonoBehaviour, PausedObject
{
    public GameObject spider;
    private int cooldown = 100;
    private int inc = 0;
    private float range = 0.25f;
    private bool pause = false;
    private bool desactivate = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (pause || desactivate) return;
        if (inc > cooldown && transform.childCount <= 50)
        {
            inc = 0;
            if (cooldown > 1) cooldown--;
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

    public void Desactivate()
    {
        desactivate = true;
    }
}
