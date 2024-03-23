using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnTembokPlacement;
    public Button placeRoadButton, placeHouseButton, placeSpecialButton, placeTembokButton;

    public string MainMenu;

    public Color outlineColor;
    List<Button> buttonList;

    // Start is called before the first frame update
    private void Start()
    {
        buttonList = new List<Button>
        {
            placeRoadButton, placeHouseButton, placeSpecialButton, placeTembokButton
        };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        });

        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();
        });

        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();
        });

        placeTembokButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeTembokButton);
            OnTembokPlacement?.Invoke();
        });
    }


    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (var button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
