using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour {

    public GameObject laserParticles;
    public GameObject spiderParticles;
    public GameObject laserRay;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        laserRay.SetActive(false);
		if (Input.GetMouseButton(0))
        {
            Ray rayAim = new Ray(transform.parent.position, transform.parent.forward);
            //Debug.DrawRay(transform.parent.position, transform.parent.forward, Color.green);
            RaycastHit hitAim;
            if (Physics.Raycast(rayAim, out hitAim, 100.0f))
            {
                transform.LookAt(hitAim.point);
                Ray rayLaser = new Ray(transform.position, transform.forward);
                //Debug.DrawRay(transform.position, transform.forward, Color.red);
                laserRay.SetActive(true);
                RaycastHit hitLaser;
                if (Physics.Raycast(rayLaser, out hitLaser, 100.0f))
                {
                    Hit(hitLaser);
                }
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
        GameObject instance = Instantiate(particles, position, Quaternion.identity);
        instance.transform.eulerAngles = new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(1.0f);
        Destroy(instance);
        yield return null;
    }
}
