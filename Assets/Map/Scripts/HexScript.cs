using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexScript : MonoBehaviour
{
    private Material highlightedMaterial;
    private Material normalMaterial;
    private MeshRenderer meshRenderer;

    [SerializeField]
    private bool occupied = false;

    [SerializeField]
    private bool usable = true;

    public List<Vector3> adjacentHexPositions = new List<Vector3>();
    public List<HexScript> adjacentHexScript = new List<HexScript>();

    private int maxTowers = 4;
    private int numberOfTowers;

    public int NumberOfTowers { get { return numberOfTowers; } set { numberOfTowers += value; } }

    private static Vector3[] directions = { Vector3.right, Vector3.forward - Vector3.right, Vector3.forward - Vector3.left, Vector3.left, Vector3.back - Vector3.right, Vector3.back - Vector3.left };

    // Start is called before the first frame update
    void Start()
    {
        numberOfTowers = 0;
        setAdjacentPositions();
        if (!usable) { return; }
        meshRenderer = GetComponent<MeshRenderer>();
        highlightedMaterial = Resources.Load<Material>("Materials/HighlightedMaterial");
        normalMaterial = Resources.Load<Material>("Materials/NormalMaterial");
    }

    public void SetMaterial(bool highlight = true)
    {
        if (!usable) { return; }

        if (highlight)
        {
            meshRenderer.material = highlightedMaterial;
        }
        else if (!occupied)
        {
            meshRenderer.material = normalMaterial;
        }
    }

    public void SetOccupied(bool occupiedState = true)
    {
        if (!usable) { return; }
        occupied = occupiedState;
        SetMaterial(occupiedState);
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public bool getUsable()
    {
        return usable;
    }

    private void setAdjacentPositions()
    {
        foreach (Vector3 dir in directions)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, 0.75f)) continue;

            if (hit.transform.gameObject.tag == "Hex" || hit.transform.gameObject.tag == "Bridge")
            {
                adjacentHexPositions.Add(hit.transform.position);
                adjacentHexScript.Add(hit.transform.gameObject.GetComponent<HexScript>());
            }
        }
    }

    public Vector3 getAdjacentPosition(Vector3 pos)
    {
        int index = 0;
        Vector3 closestPos = Vector3.zero;

        int count = adjacentHexScript.Count;

        do
        {
            closestPos = adjacentHexPositions[index];
            index++;
        } while (adjacentHexScript[index - 1].GetOccupied() && index < count);

        float sqrMagn = (closestPos - pos).sqrMagnitude;

        for (int i = index; i < count; i++)
        {
            if (adjacentHexScript[i].GetOccupied()) { continue; }

            float currentMagn = (adjacentHexPositions[i] - pos).sqrMagnitude;

            if (currentMagn < sqrMagn)
            {
                sqrMagn = currentMagn;
                closestPos = adjacentHexPositions[i];
            }
        }
        return closestPos;
    }

    public bool hasSpace()
    {
        if (numberOfTowers >= maxTowers)
        {
            return false;
        }
        return true;
    }
}
