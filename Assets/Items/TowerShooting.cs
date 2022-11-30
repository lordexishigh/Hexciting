using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    public Transform shootingPoint;

    public List<GameObject> enemies = new List<GameObject>();

    private GameObject bullet;

    private float timer = 0;
    private float maxTimer = 0.5f;

    void Start()
    {
        bullet = Resources.Load<GameObject>("Bullet");
        shootingPoint = gameObject.transform.GetChild(0);
    }

    void Update()
    {
		timer -= Time.deltaTime;
        Aim();
    }

    private void Aim()
    {
        int index = 0; 
        int count = enemies.Count;

        while (index < count)
        {
            if (!enemies[index].activeSelf)
            {
                enemies.RemoveAt(index);
                count--;
                continue;
            }

            //Vector3 direction1 = transform.position - transform.parent.position;
            //Vector3 direction2 = hit.point - transform.parent.position;

            //transform.RotateAround(transform.parent.position, Vector3.up, /*-1 * Vector3.SignedAngle(direction2, direction1, Vector3.up)*/);

            RaycastHit hit;

            if (!Physics.Raycast(shootingPoint.position, enemies[index].transform.position - shootingPoint.position, out hit, 15, Physics.AllLayers, QueryTriggerInteraction.Ignore)) { index++; continue; }

            if (hit.transform.gameObject.tag == "Enemy")
            {
                Vector3 pos = enemies[index].transform.position;
                pos.y = transform.position.y;
                transform.LookAt(pos, Vector3.up);

                if (timer < 0)
                {
                    shootBullet(hit.transform);
                }
                return;
            }
            index++;
        }

    }

    private void shootBullet(Transform target)
    {
        Instantiate(bullet, shootingPoint.position, transform.rotation).GetComponent<BulletScript>().SetDestination(target);

        timer = maxTimer;
        return;        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            enemies.Add(col.gameObject);
        
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            enemies.Remove(col.gameObject);
    }
}
