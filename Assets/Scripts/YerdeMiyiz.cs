using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YerdeMiyiz : MonoBehaviour
{
    public LayerMask layer; // zemin layer'�
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
        //���n nerende ba�layacak, ne tarafa do�ru gidecek, ���n vekt�r�n�n b�y�kl���, hangi katmana temas etti�inde �al��acak
        RaycastHit2D carpis = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layer);

        if(carpis.collider != null)
        {
            //���n bir yere �arpt�ysa yani zemine temas ediyorsak
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
