using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour {

    public GameObject laserParticles;
    public GameObject spiderParticles;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Hit(hit);
            }
        }
	}

    private void Hit(RaycastHit hit)
    {
        StartCoroutine(Particles(laserParticles, hit.point));
        if (hit.collider.name == "Spider(Clone)")
        {
            StartCoroutine(Particles(spiderParticles, hit.point));
            Destroy(hit.collider.gameObject);
        }
    }

    IEnumerator Particles(GameObject particles, Vector3 position)
    {
        GameObject instance = Instantiate(particles, position, Quaternion.identity, transform);
        instance.transform.eulerAngles = new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(1.0f);
        Destroy(instance);
        yield return null;
    }
}
