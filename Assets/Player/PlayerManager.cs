using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float gold;

    public float Gold
	{
        get { return gold; }
        set
		{
            gold += value;
            UI.instance.setGoldText(gold);
        }
	}

    private int health;

    public int Health
	{
        get { return health; }
        set 
        { 
            health += value;
            UI.instance.setHealthText(health);
        }
	}

    public static PlayerManager instance;

    void Awake()
	{
        health = 100;
        gold = 200;

        if (instance == null)
        {
            instance = this;
        }
    }
}
