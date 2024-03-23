using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Vector3Int> temporaryPlacementPosition = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

    //public GameObject roadStraight;
    public RoadFixer roadFixer;

    // Start is called before the first frame update
    void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceRoad(Vector3Int Position)
    {
        if (placementManager.CheckIfPositionInBound(Position)==false)
        {
            return;
        }
        if (placementManager.CheckIfPositionIsFree(Position) == false)
        {
            return;
        }
        if(placementMode == false)
        {
            temporaryPlacementPosition.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = Position;

            temporaryPlacementPosition.Add(Position);
            placementManager.PlaceTemporaryStructure(Position, roadFixer.roadStraight, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPosition.Clear();

            foreach (var  positionToFix in roadPositionsToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, positionToFix);
            }

            roadPositionsToRecheck.Clear();
            temporaryPlacementPosition = placementManager.GetPathBetween(startPosition, Position);

            foreach (var temporaryPosition in temporaryPlacementPosition)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                {
                    continue;
                }
                placementManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.roadStraight, CellType.Road);
            }
        }
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPosition)
        {
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            var neighbors = placementManager.GetNeigborOfTypesFor(temporaryPosition, CellType.Road);

            foreach (var roadPosition in neighbors)
            {
                if (roadPositionsToRecheck.Contains(roadPosition) == false)
                {
                    roadPositionsToRecheck.Add(roadPosition);
                }
            }
        }
        foreach (var positionToFix in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
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
