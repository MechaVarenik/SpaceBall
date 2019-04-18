using UnityEngine;

public class PickUpController : MonoBehaviour {

    public float minLifeTime = 5;
    public float maxLifeTime = 15;

    public GameObject particles = null;

    private float lifeTime;
    private bool isActive = true;

    public GameObject pickUpFactory = null;

    public void Start() {

        lifeTime = Random.Range(minLifeTime, maxLifeTime);
    }

    void Update() {

        if(isActive && (lifeTime -= Time.deltaTime) <= 0) DestroyItem(); 
        
    }

    public void PickUp() {
        if(isActive) {
            GameManager.instance.IncreaseScore();
            if(particles != null)
                Instantiate(particles).transform.position = transform.position;
            DestroyItem();
        }
    }

    public void DestroyItem() {
        if(pickUpFactory != null && pickUpFactory.GetComponent<PickUpFactory>() != null) {
            pickUpFactory.GetComponent<PickUpFactory>().DestroyNotify(gameObject);
        }
            isActive = false;
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    

}



