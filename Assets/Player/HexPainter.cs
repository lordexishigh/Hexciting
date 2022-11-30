using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPainter : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Hex")
		{
			HexScript hex = col.gameObject.GetComponent<HexScript>();
			if (hex.getUsable())
				hex.SetMaterial(true);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Hex")
		{
			HexScript hex = col.gameObject.GetComponent<HexScript>();
			if(hex.getUsable())
				hex.SetMaterial(false);
		}
	}
}
