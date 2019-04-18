using UnityEngine;
using UnityEngine.UI;

public class HUDItemLabel : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        HUDManager.instance.SetLabel(gameObject.name, GetComponent<Text>());
    }

}
