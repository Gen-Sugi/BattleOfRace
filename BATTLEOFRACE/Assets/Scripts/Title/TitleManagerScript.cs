using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject editCanvas;
    [SerializeField] private GameObject loginCanvas;

    [SerializeField] private AudioClip select;

    public void Update()
    {

    }

    public void OnClickToEdit()
    {
        editCanvas.SetActive(true);
        titleCanvas.SetActive(false);        
    }

    public void OnClickToLogin()
    {
        loginCanvas.SetActive(true);
        titleCanvas.SetActive(false);        
    }

    public void OnClickGuestPlay()
    {
        SceneManager.LoadScene("MainScene");
    }
}
