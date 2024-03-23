using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeight[] housesPrefab, specialPrefab;
    public PlacementManager placementManager;

    private float[] housesWeight, specialWeight;
    // Start is called before the first frame update
    void Start()
    {
        housesWeight = housesPrefab.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeight = specialPrefab.Select(prefabStats => prefabStats.weight).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositonBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightIndex(housesWeight);
            placementManager.PlaceObjectOnTheMap(position, housesPrefab[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositonBeforePlacement(position))
        {
            int randomIndex = GetRandomWeightIndex(specialWeight);
            placementManager.PlaceObjectOnTheMap(position, specialPrefab[randomIndex].prefab, CellType.SpecialStructure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private int GetRandomWeightIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue<tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }

    private bool CheckPositonBeforePlacement(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("Posisi sudah kelewatan");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("Posisi sudah diambil");
            return false;
        }
        if (placementManager.GetNeigborOfTypesFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Posisi dipasang di dekat jalan");
            return false;
        }
        return true;
    }
}

[Serializable]
public struct StructurePrefabWeight
{
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}
