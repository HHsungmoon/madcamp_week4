using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Status : MonoBehaviour
{
    public int maxHP = 150;
    public int currentHP;

    public HP_bar hpBar;

    public int maxMP = 100;
    public int currentMP;

    public mp_bar mpBar;

       
    public ParticleSystem fireDestroy;
    void Start()
    {
        currentHP = maxHP;
        hpBar.SetMaxHP(maxHP);

        currentMP = maxMP;
        mpBar.SetMaxMP(maxMP);
    }

    void Update()
    {
        Transform childTransform = transform.Find("Myship(Clone)");
        if (childTransform != null)
        {
            //Debug.Log("myship find!!!!");
            ActivateJSControlUI(true);
        }
        else
        {
            ActivateJSControlUI(false);
        }
        mpBar.SetMP(currentMP);
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpBar.SetHP(currentHP);
        if (currentHP <= 0)
        {
            fireDestroy.Play();

            ObjectSelector objectSelector = GetComponent<ObjectSelector>();
            if (objectSelector != null)
            {
                objectSelector.HandleSink(gameObject);
            }
            StartCoroutine(SinkShip());
        }
    }

    IEnumerator SinkShip()
    {
        float duration = 5f; 
        float endPositionY = transform.position.y - 5; 
        float elapsedTime = 0; 

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
        while (elapsedTime < duration)
        {
  
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, endPositionY, (elapsedTime / duration)), transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        transform.position = new Vector3(transform.position.x, endPositionY, transform.position.z);

    
        Destroy(gameObject);
    }

    void UsePotan(int bullet)
    {
        currentMP -= bullet;
        mpBar.SetMP(currentMP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bollet bullet = collision.gameObject.GetComponent<Bollet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "JP_sakibune")
        {
            TakeDamage(10);
        }
        if (collision.gameObject.tag == "JP_atake")
        {
            TakeDamage(10);
        }
    }

    public void ActivateJSControlUI(bool type)
    {
        Transform floatChild = transform.Find("float");
        if (floatChild != null)
        {
       
            Transform turtleShip1Child = floatChild.Find("Turtle_ship_1");
            Transform panokShipChild = floatChild.Find("Panok_ship_(Panokseon)____");
            if (turtleShip1Child != null)
            {
       
                Transform jsControlUIChild = turtleShip1Child.Find("JS_control_UI");
                if (jsControlUIChild != null)
                {
                    jsControlUIChild.gameObject.SetActive(type);
                }
            }
            if (panokShipChild != null)
            {
               
                Transform jsControlUIChild = panokShipChild.Find("JS_control_UI");
                if (jsControlUIChild != null)
                {
                    jsControlUIChild.gameObject.SetActive(type);
                }
            }
        }
    }
}