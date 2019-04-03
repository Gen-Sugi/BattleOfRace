using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour {

    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject loginCanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickToPlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickReturn()
    {
        titleCanvas.SetActive(true);
        loginCanvas.SetActive(false);
    }
}
