using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

    [SerializeField] private GameObject player;
    private Vector3 offset = new Vector3(0f,5f,-10f);

    // Use this for initialization
    void Start () {
        //offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }


}
