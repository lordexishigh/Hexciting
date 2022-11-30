using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
	[SerializeField]
    private GameObject[] towerPrefabs;

    private int towerIndex = 3;
    public int TowerIndex { get { return towerIndex; } set { towerIndex = value; } }

    private int[] towerAmounts;

    private PlayerMovement playerMovement;

    private Vector3 towerPosition;
    private Vector3 towerAdjacentPosition;

    private BridgeScript bridgeScript;

    // Start is called before the first frame update
    void Start()
    {
        //towerPrefabs = Resources.Load<GameObject>("Tower");
        playerMovement = GetComponent<PlayerMovement>();
        towerAmounts = new int[towerPrefabs.Length];

        for (int i = 0; i < towerAmounts.Length; i++)
        {
            towerAmounts[i] = 5;
        }
    }

    void Update()
    {
        if(transform.position == towerAdjacentPosition && towerAdjacentPosition != Vector3.zero)
		{
            InstantiateTower(towerPosition);
        }
    }

    public void SpawnTower(RaycastHit RayHit)
	{
        HexScript hex = playerMovement.hexScript;

        if (towerPosition != Vector3.zero)
        {
            resetTowerPositions();
        }
        if (hex)
        {
            if(!hex.GetOccupied() && hex.getUsable())
                hex.SetMaterial(false);
            hex = null;
        }

        GameObject obj = RayHit.transform.gameObject;

        switch (obj.tag)
        {
            case "Hex":
                hex = obj.GetComponent<HexScript>();
                if (!hex.getUsable()) { return; }
                hex.SetMaterial(true);
                break;

            case "Tower":
                hex = obj.GetComponent<TowerScript>().getBaseHex();
                break;

            case "Bridge":
                if(towerIndex != 3) { return; }
                bridgeScript = obj.GetComponent<BridgeScript>();
                if(bridgeScript.getEnabled()) { bridgeScript = null; return; }
                hex = obj.GetComponent<HexScript>();
                break;

            default:
                return;
        }

        if (!hex.hasSpace()) { return; }

        towerPosition = obj.transform.position + Vector3.up * obj.transform.localScale.y * 1.95f;
        
        Vector3 point = hex.getAdjacentPosition(transform.position);

        towerAdjacentPosition = new Vector3(point.x, transform.position.y, point.z);

        playerMovement.hexScript = hex;

        playerMovement.SetDestination(towerAdjacentPosition);
    }

    private void InstantiateTower(Vector3 point)
	{
        if (towerIndex < 3)
        {
            if (getTowerAmount(towerIndex) < 1) { resetTowerPositions(); return; }

            
            playerMovement.hexScript.SetOccupied();
            playerMovement.hexScript.NumberOfTowers = 1;
            Instantiate(towerPrefabs[towerIndex], point, Quaternion.identity);
            towerPosition = towerAdjacentPosition = Vector3.zero;
        }
        else if (bridgeScript)
		{
            bridgeScript.EnableBridge();
            bridgeScript = null;
		}
        setItemAmount(towerIndex, -1);
        playerMovement.hexScript = null;
    }

    public void resetTowerPositions()
	{
        towerAdjacentPosition = towerPosition = Vector3.zero;
    }

    public int getTowerAmount(int index)
    {
        return towerAmounts[index];
    }

    public void setItemAmount(int index, int amount = 1)
    {
        towerAmounts[index] += amount;
        UI.instance.setItemAmountText(index, towerAmounts[index]);
    }
}
