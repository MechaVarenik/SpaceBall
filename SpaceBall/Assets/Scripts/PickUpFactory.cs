using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFactory : MonoBehaviour {

    public GameObject pickUpPrefab;
    public Transform pointA;
    public Transform pointB;

    public float creationDelayMin = 0.5f;
    public float creationDelayMax = 2f;

    public List<GameObject> PickUpList = new List<GameObject>();

    private bool isGameRunning;

    void Start() {
        GameManager.instance.StartGameEvent += OnGameStart;
        GameManager.instance.StopGameEvent += OnGameStop;
    }


    public void CreateItem(Vector3 pos) {
        GameObject item = Instantiate(pickUpPrefab);
        item.GetComponent<PickUpController>().pickUpFactory = gameObject;

        PickUpList.Add(item);

        item.transform.position = pos;
        GameManager.instance.SetCubesCounter(PickUpList.Count);
        
    }

    public void DestroyNotify(GameObject item) {
        PickUpList.Remove(item);
        GameManager.instance.SetCubesCounter(PickUpList.Count);
    }

    private IEnumerator CreationProcess() {
        for(;;) {

            if(!isGameRunning) break;
            Vector3 pos = new Vector3(0, 0.5f, 0);
            Vector3 a = pointA.position;
            Vector3 b = pointB.position;

            pos.x = Random.Range(a.x < b.x ? a.x : b.x, a.x > b.x ? a.x : b.x);
            pos.z = Random.Range(a.z < b.z ? a.z : b.z, a.z > b.z ? a.z : b.z);

            CreateItem(pos);
            yield return new WaitForSeconds(Random.Range(creationDelayMin, creationDelayMax));
        }
        yield return null;
    }

    private void OnGameStart() {
        isGameRunning = true;
        GameManager.instance.SetCubesCounter(0);
        StartCoroutine(CreationProcess());
    }

    private void OnGameStop() {
        isGameRunning = false;
        StopCoroutine(CreationProcess());
        foreach(GameObject item in PickUpList.ToArray()) {
            if (item != null) item.GetComponent<PickUpController>().DestroyItem();
        }

    }

}
