using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YerdeMiyiz : MonoBehaviour
{
    public LayerMask layer; // zemin layer'ý
    public bool yerde_miyiz;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!AstronotHareket.isGameStarted)
        {
            return;
        }
        //parametreler
        //ýþýn nerende baþlayacak, ne tarafa doðru gidecek, ýþýn vektörünün büyüklüðü, hangi katmana temas ettiðinde çalýþacak
        RaycastHit2D carpis = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layer);

        if(carpis.collider != null)
        {
            //ýþýn bir yere çarptýysa yani zemine temas ediyorsak
            yerde_miyiz = true;

        }
        else
        {
            yerde_miyiz = false;
        }
        //ziplama
        if(Input.GetKeyDown(KeyCode.Space) && yerde_miyiz == true)
        {
            rb.velocity += new Vector2(0, 18f);
        }
    }
}
