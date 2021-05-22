using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AstronotHareket : MonoBehaviour
{
    // 2 //
    //
    //Vector3 noktaA = new Vector3(-4, 0, 0);
    //Vector3 noktaB = new Vector3(6, 0, 0);
    //float hiz = 1f;
    //float t;

    // 4 //
    //
    //public float itki = 5f; // 4.1 - Kuvvet
    //public float itki = 1000000f;
    //Rigidbody2D rb;

    // 5 //
    //
    public float hiz = 0.1f;
    public int crystalCount;
    public bool isOnPlanet = false;
    public int health = 100;

    // 7 //
    //
    public int stoneCount;

    // 11 //
    //
    public Animator myAnimator;

    // 12 //
    //
    public Text txtStone;
    public Text txtCrystal;

    // 13 //
    //
    public GameObject endGamePanel;
    public Text endGameStone;
    public Text endGameCrystal;

    // 14 //
    //
    public static bool isGameStarted = false;
    public GameObject startGamePanel;

    // 15 //
    //
    public AudioSource stoneCollectSound;

    // Kodu sadeleþtirme iþleri
    float yatay;
    //float dikey;

    // Start is called before the first frame update
    void Start()
    {
        // 12 //
        //
        txtCrystal.text = health.ToString();
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 1 // iki nokta arasý hareket
        //
        //Vector3 hedef = new Vector3(8, 0, 0);
        //float hiz = 10f;
        //transform.position = Vector3.MoveTowards(transform.position, hedef, Time.deltaTime * hiz);

        // 2 // volta
        //
        //t += Time.deltaTime * hiz;
        //transform.position = Vector3.Lerp(noktaA, noktaB, t);
        //if (t >= 1)
        //{
        //    var b = noktaB;
        //    var a = noktaA;

        //    noktaA = b;
        //    noktaB = a;
        //    t = 0;
        //}

        // 3 // belirli yönde hareket
        //
        //transform.Translate(3 * Time.deltaTime, 0, 0);

        // 4.1 - Kuvvet Eklemek //
        // kuvvette ivme olduðu için hýzlanarak gider
        //rb.AddForce(transform.right * itki);
        // 4.2 - Hýz Eklemek //
        // hýzda sabit gider
        //rb.velocity = transform.right * itki * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(!isGameStarted)
        {
            return;
        }
        // 5 // yatayda klavye ile hareket
        //
        yatay = Input.GetAxis("Horizontal");
        //Debug.Log(yatay);
        //transform.position += new Vector3(yatay, 0, 0);
        transform.position += new Vector3(yatay * hiz, 0, 0);

        //dikey = Input.GetAxis("Vertical");
        //transform.position += new Vector3(0, dikey * hiz, 0);

        ChangeDirection();

        // 11 // animasyon
        //
        bool isWalk = false;
        if((yatay > 0) || (yatay < 0))
        {
            isWalk = true;
        }

        if(yatay == 0)
        {
            isWalk = false;
        }
        //koddaki deðeri animator'e atma
        myAnimator.SetBool("IsWalk", isWalk);
    }

    void Awake()
    {
        // 4 //
        //
        //ilk çalýþtýðýnda rb^ye oyun içindeki rigitbody'i atýyor
        //update'de rb' kullanýyoruz
        //rb = GetComponent<Rigidbody2D>();
    }

    // 6 // çarpýþma
    // 
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Astronot Carpisti.");
    //}

    // 7 // taþ toplama
    //
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Stone")
    //    {
    //        stoneCount++;
    //        Debug.Log("Stone Toplandi.");
    //        Destroy(collision.gameObject);
    //    }
    //}
    // 8 // tuzak
    //
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Platform")
    //    {

    //        if (isOnPlanet==false)
    //        {
    //            Debug.Log("Astronot Gezegene Indi.");
    //        }
    //        isOnPlanet = true;
    //    }
    //    else if(collision.gameObject.tag == "Spike")
    //    {
    //        Debug.Log("Tuzaga yakalandin, oyun bitti.");
    //        Destroy(this.gameObject);
    //    }
    //}
    // 9 // can toplama
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crystal")
        {
            health += 5;
            if(health >= 100)
            {
                health = 100;
            }
            //12//
            txtCrystal.text = health.ToString();
            Debug.Log("+5 Can Toplandi. Mevcut Can Puani: " + health);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Stone")
        {
            stoneCount++;
            //12//
            txtStone.text = stoneCount.ToString();
            Debug.Log("Stone Toplandi.");
            Destroy(collision.gameObject);
            stoneCollectSound.Play();
        }

        if (collision.gameObject.tag == "Portal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    // 10 // tuzak can azaltma ve bitiþ
    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            health -= 15;
            
            
            if(health <= 0)
            {
                health = 0;
                //12//
                txtCrystal.text = health.ToString();

                Debug.Log("Tuzaga yakalandin, -15 Can. Kalan Can Puani: " + health);
                Debug.Log("Oyun Bitti.");

                Destroy(this.gameObject);

                endGamePanel.SetActive(true);
                endGameStone.text = stoneCount.ToString();
                endGameCrystal.text = crystalCount.ToString();

            }
            else
            {
                //12//
                txtCrystal.text = health.ToString();

                Debug.Log("Tuzaga yakalandin, -15 Can. Kalan Can Puani: " + health);
            }
        }
    }
    void ChangeDirection()
    {
        if (yatay > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (yatay < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

        //if (dikey > 0)
        //{
        //    float x = transform.localScale.x;
        //    transform.localScale = new Vector3(x, 1, 1);
        //}
        //else if (dikey < 0)
        //{
        //    transform.localScale = new Vector3(1, -1, 1);
        //}
    }

    public void GameStarted()
    {
        isGameStarted = true;
        startGamePanel.SetActive(false);
    }
}
