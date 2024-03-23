using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TembokManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Vector3Int> temporaryPlacementPosition = new List<Vector3Int>();
    public List<Vector3Int> tembokPositionToCheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

    //public GameObject roadStraight;
    public RoadFixer tembokFixer;

    // Start is called before the first frame update
    void Start()
    {
        tembokFixer = GetComponent<RoadFixer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceTembok(Vector3Int Position)
    {
        if (placementManager.CheckIfPositionInBound(Position) == false)
        {
            return;
        }
        if (placementManager.CheckIfPositionIsFree(Position) == false)
        {
            return;
        }
        if (placementMode == false)
        {
            temporaryPlacementPosition.Clear();
            tembokPositionToCheck.Clear();

            placementMode = true;
            startPosition = Position;

            temporaryPlacementPosition.Add(Position);
            placementManager.PlaceTemporaryStructure(Position, tembokFixer.roadStraight, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPosition.Clear();

            foreach (var positionToFix in tembokPositionToCheck)
            {
                tembokFixer.FixRoadAtPosition(placementManager, positionToFix);
            }

            tembokPositionToCheck.Clear();
            temporaryPlacementPosition = placementManager.GetPathBetween(startPosition, Position);

            foreach (var temporaryPosition in temporaryPlacementPosition)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                {
                    continue;
                }
                placementManager.PlaceTemporaryStructure(temporaryPosition, tembokFixer.roadStraight, CellType.Road);
            }
        }
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPosition)
        {
            tembokFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            var neighbors = placementManager.GetNeigborOfTypesFor(temporaryPosition, CellType.Road);

            foreach (var roadPosition in neighbors)
            {
                if (tembokPositionToCheck.Contains(roadPosition) == false)
                {
                    tembokPositionToCheck.Add(roadPosition);
                }
            }
        }
        foreach (var positionToFix in tembokPositionToCheck)
        {
            tembokFixer.FixRoadAtPosition(placementManager, positionToFix);
        }
    }

    public void FinishPlacingRoad()
    {
        placementMode = false;
        placementManager.AddTemporaryStructuresToStructureDictionary();
        if (temporaryPlacementPosition.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();

        }
        temporaryPlacementPosition.Clear();
        startPosition = Vector3Int.zero;
    }
}
