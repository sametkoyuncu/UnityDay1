using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public Rigidbody2D bulletRB2D;
    public Rigidbody2D bulletConnectionPointRB2D;
    bool isClick = false;
    public float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            //bulletRB2D.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(mousePos, bulletConnectionPointRB2D.position)>maxDistance)
            {
                bulletRB2D.position = bulletConnectionPointRB2D.position + (mousePos - bulletConnectionPointRB2D.position).normalized * maxDistance;
            }
            else
            {
                bulletRB2D.position = mousePos;
            }
        }
    }

    void OnMouseDown()
    {
        isClick = true;
        bulletRB2D.isKinematic = true;
    }

    void OnMouseUp()
    {
        isClick = false;
        bulletRB2D.isKinematic = false;
        Fire();
    }

    void Fire()
    {
        Destroy(GetComponent<SpringJoint2D>(), 0.05f);
    }
}
