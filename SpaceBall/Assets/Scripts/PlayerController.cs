using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 4;
    public float jumpForce = 200;

    private Rigidbody body;
    private SphereCollider touch;

    void Start() {

        body = GetComponent<Rigidbody>();
        touch = GetComponent<SphereCollider>();

    }


    private void FixedUpdate() {

        if(!GameManager.instance.isRunning) return;

        Vector3 movDir = InputManager.GetMoveDirection();

        float moveX = movDir.x;
        float moveZ = movDir.z;
        float moveY = movDir.y;

        Vector3 velocity = new Vector3(moveX, 0, moveZ) * movementSpeed;
        if(body.velocity.y == 0) {
            velocity.x *= 1.1f;
            velocity.y = moveY * jumpForce;
            velocity.z *= 1.1f;
        }
           
        body.AddForce(velocity);

        Vector3 vec = body.velocity;
        vec.x = Mathf.Clamp(body.velocity.x, -8f, 8f);
        vec.z = Mathf.Clamp(body.velocity.z, -8f, 8f);
        body.velocity = vec;
    }

    public void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "tg_PickUp") {
            other.gameObject.GetComponent<PickUpController>().PickUp();                   
        }

        if(other.gameObject.tag == "tg_static_wall") {
            Debug.Log("Points colliding: ");
            Debug.Log("First normal of the point that collide: ");


        }
    }

    public void SetPosition(Vector3 pos) {
        body.MovePosition(pos);
        body.velocity = Vector3.zero;
    }


}
