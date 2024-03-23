using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Credits, Main, Select, Controls;
    public string City, Kingdom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kota()
    {
        SceneManager.LoadScene(City);
    }

    public void Kerajaan()
    {
        SceneManager.LoadScene(Kingdom);
    }

    public void OpenCredits()
    {
        Credits.SetActive(true);
        Main.SetActive(false);
    }

    public void BackFromCredits()
    {
        Credits.SetActive(false);
        Main.SetActive(true);
    }

    public void GoPlay()
    {
        Main.SetActive(false);
        Select.SetActive(true);
    }

    public void BackFromSelect()
    {
        Main.SetActive(true);
        Select.SetActive(false);
    }

    public void GoToControls()
    {
        Controls.SetActive(true);
        Main.SetActive(false);
    }

    public void BackFromControls() 
    {
        Controls.SetActive(false);
        Main.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
