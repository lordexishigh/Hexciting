using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private static LayerMask layers;
    private static Camera _camera;

    private TowerSpawn towerSpawn;
    private NavMeshAgent agent;

    private HexScript hex;

    public HexScript hexScript { get { return hex; } set { hex = value; } }

    private ShopHandler shopHandler;

    // Start is called before the first frame update
    void Start()
    {
        layers = LayerMask.GetMask("Hex", "Tower");
        towerSpawn = GetComponent<TowerSpawn>();
        agent = GetComponent<NavMeshAgent>();
        shopHandler = GetComponent<ShopHandler>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public static bool CameraToMouseRay(Vector2 position, out RaycastHit RayHit)
	{
        Ray ray = _camera.ScreenPointToRay(position);

        return Physics.Raycast(ray, out RayHit, Mathf.Infinity, layers);
    }

    public void MoveToInput(RaycastHit RayHit)
    {
        towerSpawn.resetTowerPositions();
        if (hex)
		{
            if (!hex.GetOccupied() && hex.getUsable())
            {
                hex.SetMaterial(false);                
            }
			
            hex = null;
		}

        GameObject obj = RayHit.transform.gameObject;

		switch (obj.tag)
		{
            case "Hex":
                hex = obj.GetComponent<HexScript>(); break;

            case "Tower":
                hex = obj.GetComponent<TowerScript>().getBaseHex(); break;

            case "Shop":
                hex = obj.GetComponent<ShopScript>().getBaseHex(); break;

            case "Bridge":
                if(!obj.GetComponent<BridgeScript>().getEnabled()) { return; }
                hex = obj.GetComponent<HexScript>(); break;
            default:
                return;
        } 

        if (!hex.GetOccupied())
        {
            SetDestination(RayHit.transform.position);
        }
        else
        {
            SetDestination(hex.getAdjacentPosition(transform.position));

            if (obj.tag == "Shop")
                shopHandler.SetPos(agent.destination);
        }

        if (hex.getUsable())
            hex.SetMaterial(true);
    }

    //	if (hex.GetOccupied()) { return; }

    //    if(hex.getUsable())
    //        hex.SetMaterial(true);

    //    Vector3 point = obj.transform.position;

    //SetDestination(point);

    public void SetDestination(Vector3 point)
	{
        agent.destination = new Vector3(point.x, transform.position.y, point.z);
    }
}
