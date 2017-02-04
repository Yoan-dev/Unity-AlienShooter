using System.Collections;
using UnityEngine;

public class Spider : MonoBehaviour, PausedObject
{
    private GameObject structure;
    private new Animation animation;
    private bool pause = false;
    private bool dead = false;
    private bool attacking = false;

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
        yield return new WaitForSeconds(1.958f);
        Destroy(gameObject);
        yield return null;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (pause || dead) return;
        if (collision.gameObject.name == "Structure")
        {
            animation.Play("Attack");
            if (!attacking)
            {
                attacking = true;
                StartCoroutine(Attack(collision.gameObject.GetComponent<Structure>()));
            }
        }
    }

    IEnumerator Attack(Structure structure)
    {
        yield return new WaitForSeconds(1.0f);
        if (!dead) structure.Hit(1.0f);
        yield return new WaitForSeconds(0.458f);
        attacking = false;
        yield return null;
    }

    public void PauseOnOff(bool onOff)
    {
        pause = onOff;
    }
}
