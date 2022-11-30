using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    private HexScript hex;

    private Collider col;

    private bool isEnabled;

	[SerializeField]
    private GameObject visualBridge;

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = false;
        hex = GetComponent<HexScript>();
        col = GetComponent<Collider>();
    }

    public void EnableBridge()
	{
        col.isTrigger = !col.isTrigger;
        visualBridge.SetActive(!visualBridge.activeSelf);
        isEnabled = !isEnabled;
        NavigationBaker.instance.BuildNavMesh();
    }

    public bool getEnabled()
	{
        return isEnabled;
	}
}
