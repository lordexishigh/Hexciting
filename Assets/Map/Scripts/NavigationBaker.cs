using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    public static NavigationBaker instance;

    void Awake()
    {
        instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
        BuildNavMesh();
    }

    public void BuildNavMesh()
	{
		navMeshSurface.AddData();
		navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
		//navMeshSurface.BuildNavMesh();
	}
}
