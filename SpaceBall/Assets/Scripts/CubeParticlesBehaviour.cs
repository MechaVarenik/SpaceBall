using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticlesBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.main.startLifetime.constantMax);
    }
}
