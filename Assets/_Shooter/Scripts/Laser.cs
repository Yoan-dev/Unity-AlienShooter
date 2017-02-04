using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour {

    public UnityEngine.UI.Button restart;
    public UnityEngine.UI.Button quit;

    public GameObject laserParticles;
    public GameObject spiderParticles;
    public UnityEngine.UI.Text textScore;
    private bool desactivate = false;

    private int score = 0;

    void Start ()
    {
        restart.onClick.AddListener(() => Restart());
        quit.onClick.AddListener(() => Quit());
    }
	
	void Update ()
    {
		if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray(transform.parent.position, transform.parent.forward);
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
            Spider spider = hit.collider.gameObject.GetComponent<Spider>();
            if (!spider.IsDead())
            {
                StartCoroutine(Particles(spiderParticles, hit.point));
                spider.Kill();
                if (!desactivate) score++;
                textScore.text = "Score: " + score;
            }
        }
        else if (hit.collider.name == "Tower")
        {
            hit.collider.gameObject.transform.parent.GetComponent<Structure>().Hit(0.25f);
        }
        else if (hit.collider.name == "restart")
        {
            Restart();
        }
        else if (hit.collider.name == "restart")
        {
            Quit();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene("Shooter");
    }

    private void Quit()
    {
        Application.Quit();
    }

    IEnumerator Particles(GameObject particles, Vector3 position)
    {
        GameObject instance = Instantiate(particles, position, Quaternion.identity);
        instance.transform.eulerAngles = new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(3.0f);
        Destroy(instance);
        yield return null;
    }

    public void Desactivate()
    {
        desactivate = true;
    }
}
