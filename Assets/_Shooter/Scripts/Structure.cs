using UnityEngine;

public class Structure : MonoBehaviour {

    public GameObject head;
    public GameObject explosion;
    public GameObject reticule;
    public GameObject gameOver;
    public GameObject buttons;
    public Spawner spawner;
    public Laser laser;
    public UnityEngine.UI.Text hpText;
    public Transform ui;
    public Transform cam;
    private float hp = 100.0f;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        ui.rotation = Quaternion.LookRotation(ui.position - cam.position);
    }

    public void Hit(float damage)
    {
        if (hp < 0) return;
        hp -= damage;
        if (hp >= 0) hpText.text = ((int)hp) + "";
        if (hp < 0)
        {
            reticule.SetActive(false);
            gameOver.SetActive(true);
            explosion.SetActive(true);
            head.SetActive(false);
            buttons.SetActive(true);
            spawner.Desactivate();
            laser.Desactivate();
        }
    }
}
