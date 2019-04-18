using UnityEngine;

public class InputManager : _SigletonBehaviour<InputManager>{

    public Touch currentTouch = default;
    public Vector2 prevPosition = new Vector2();
    public Vector2 currentPosition = new Vector2();

    public static Vector3 GetMoveDirection() {

        Vector3 direct = Vector3.zero;

        if(!Application.isMobilePlatform) {
            direct = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        }
        else {
            if(Input.touchCount > 0) {

                Vector2 screenPoint = (instance.currentPosition - instance.prevPosition);
                if (screenPoint.magnitude > 1.5f)
                direct = new Vector3(screenPoint.x, 0, screenPoint.y).normalized;

                if(Input.touchCount > 1)
                    direct.y = 1;
            }

        }
        return direct;
    }

    public static bool isControllActivated() {

        bool flActive = false;

        if(!Application.isMobilePlatform) {
            flActive = Input.anyKeyDown;
        }
        else {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                flActive = true;
            }
        }

        return flActive;
    }

    public void Update() {
        if(Application.isMobilePlatform && Input.touchCount > 0) {

            instance.prevPosition = instance.currentPosition;
            instance.currentPosition = Input.GetTouch(0).position;
        }
       
    }

}