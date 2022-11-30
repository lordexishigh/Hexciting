using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform destinationTrans;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (destinationTrans != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationTrans.position, 0.1f);
            if (transform.position == destinationTrans.position)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDestination(Transform trans)
    {
        destinationTrans = trans;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            col.gameObject.GetComponent<Enemies>().TakeDamage();
        }
        Destroy(gameObject);
    }
}
 