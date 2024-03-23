using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModel : MonoBehaviour
{
    float yHeight = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        yHeight = structure.transform.position.y;

    }
    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var Structure = Instantiate(model, transform);
        Structure.transform.localPosition = new Vector3(0, yHeight, 0);
        Structure.transform.localRotation = rotation;
    }
}
