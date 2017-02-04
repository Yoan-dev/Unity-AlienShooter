using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, PausedObject
{
    private GameObject structure;
    private new Animation animation;
    private bool pause = false;
    private bool dead = false;

    void Start ()
    {
        structure = GameObject.Find("Structure");
        animation = GetComponent<Animation>();
	}
	
	void Update ()
    {
        if (pause || dead) return;
        transform.LookAt(structure.transform);
        transform.position = Vector3.MoveTowards(transform.position, structure.transform.position, 0.0005f);
        if (!animation.isPlaying)
        {
            animation.Play("Walk");
        }
    }

    public void Kill()
    {
        dead = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().enabled = false;
        animation.Play("Death");
        StartCoroutine(Death());
    }

    public bool IsDead()
    {
        return dead;
    }

    IEnumerator Death()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pause || dead) return;
        if (collision.gameObject.name == "Structure")
        {
            animation.Play("Attack");
        }
    }

    public void PauseOnOff(bool onOff)
    {
        pause = onOff;
    }
}
