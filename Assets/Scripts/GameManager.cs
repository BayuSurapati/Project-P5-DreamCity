using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;

    public InputManager inputManager;

    public UIController uiController;

    public StructureManager structureManager;
    // Start is called before the first frame update
    void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
        
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += structureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += roadManager.PlaceRoad;
        inputManager.onMouseHold += HandleMouseClick;
        inputManager.onMouseUp += roadManager.FinishPlacingRoad;
    }

    private void ClearInputActions()
    {
        inputManager.onMouseClick = null;
        inputManager.onMouseHold = null;
        inputManager.onMouseUp = null;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        roadManager.PlaceRoad(position);
    }

    void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
