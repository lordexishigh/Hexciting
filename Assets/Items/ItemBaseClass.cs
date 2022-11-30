using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseClass : MonoBehaviour
{
    private HexScript baseHex;

    // Start is called before the first frame update
    void Start()
    {
        int layer_mask = LayerMask.GetMask("Hex");

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20, layer_mask))
        {
            baseHex = hit.transform.gameObject.GetComponent<HexScript>();
        }
    }

    public HexScript getBaseHex()
    {
        return baseHex;
    }
}
