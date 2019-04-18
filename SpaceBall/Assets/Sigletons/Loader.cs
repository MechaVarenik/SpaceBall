using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    public GameObject hUDManager;

    public GameObject inputManager;

    void Awake() {
        if(HUDManager.instance == null) Instantiate(hUDManager);

        if(InputManager.instance == null) Instantiate(inputManager);

        if(GameManager.instance == null) Instantiate(gameManager);
    }

}