using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
	private Vector3 pos = Vector3.zero;

	void Update()
	{
		if (pos == transform.position)
		{
			UI.instance.toggleShop();
			pos = Vector3.zero;
		}
	}

	public void SetPos(Vector3 _pos)
	{
		_pos.y = transform.position.y;
		pos = _pos;
	}

	public Vector3 GetPos()
	{
		return pos;
	}
}
