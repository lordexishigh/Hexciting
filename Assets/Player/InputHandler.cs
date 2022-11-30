using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private EventSystem eventSystem;
    private PlayerMovement playerMovement;
    private TowerSpawn towerSpawn;

    private bool isTouching = false;
    private float touchTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        playerMovement = GetComponent<PlayerMovement>();
        towerSpawn = GetComponent<TowerSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            EvaluateMobileInputs();
        }
        else if (Application.isEditor)
        {
            EvaluatePCInputs();
        }

    }

    private void EvaluateMobileInputs()
	{
        if (eventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { return; }

        RaycastHit RayHit;

        if (!PlayerMovement.CameraToMouseRay(Input.mousePosition, out RayHit)) return;

        if (Input.touches[0].phase == TouchPhase.Began)
		{
            isTouching = true;
        }
        else if (Input.touches[0].phase == TouchPhase.Stationary && isTouching)
		{
            touchTimer += Time.deltaTime;
        }

        if ((Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended || touchTimer > 0.3f) && isTouching) 
        {
            if (touchTimer > 0.3f)
            {
                towerSpawn.SpawnTower(RayHit);
            }
			else
			{
                playerMovement.MoveToInput(RayHit);
            }

            isTouching = false;
            touchTimer = 0; 
        }
    }

    private void EvaluatePCInputs()
	{
		if (eventSystem.IsPointerOverGameObject()) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit RayHit;
          
            if (PlayerMovement.CameraToMouseRay(Input.mousePosition, out RayHit))
            {
                playerMovement.MoveToInput(RayHit);
            }
        }
        else if (Input.GetMouseButtonDown(1))
		{
            RaycastHit RayHit;

            if (PlayerMovement.CameraToMouseRay(Input.mousePosition, out RayHit))
            {
                towerSpawn.SpawnTower(RayHit);
            }
        }
    }
}
