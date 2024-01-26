using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ataka_status : MonoBehaviour
{
    public int maxHP = 150;
    public int currentHP;

    public HP_bar hpBar;

    public int maxMP = 100;
    public int currentMP;

    public mp_bar mpBar;

    // fireDestroy ��ƼŬ �ý��ۿ� ���� ����
    public ParticleSystem fireDestroy1;
    public ParticleSystem fireDestroy2;
    public ParticleSystem fireDestroy3;

    void Start()
    {
        currentHP = maxHP;
        hpBar.SetMaxHP(maxHP);

        currentMP = maxMP;
        mpBar.SetMaxMP(maxMP);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UsePotan(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpBar.SetHP(currentHP);
        if (currentHP <= 0)
        {
            // "float"��� �̸��� �ڽ� ������Ʈ ã��
            Transform floatChild = transform.Find("float");
            if (floatChild != null) // ������Ʈ�� �����ϴ� ���
            {
                // Buoyant Object ��ũ��Ʈ ã��
                var buoyantScript = floatChild.GetComponent<WaterSystem.BuoyantObject>(); // BuoyantObject ��� ���� ��ũ��Ʈ�� ��Ȯ�� �̸� ���
                if (buoyantScript != null)
                {
                    buoyantScript.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
                }
            }

            // Canvas �ڽ� ������Ʈ ã�� �� ��Ȱ��ȭ
            Transform canvasChild = transform.Find("Canvas");
            if (canvasChild != null) // Canvas ������Ʈ�� �����ϴ� ���
            {
                canvasChild.gameObject.SetActive(false); // ��Ȱ��ȭ
            }

            // ��ƼŬ Ȱ��ȭ
            fireDestroy1.Play();
            fireDestroy2.Play();
            fireDestroy3.Play();

            // y �� ���� ���� ����
            StartCoroutine(SinkShip());
        }
    }

    IEnumerator SinkShip()
    {
        float duration = 5f; // ���� �ð�
        float endPositionY = transform.position.y - 5; // ���� y ��ġ
        float elapsedTime = 0; // ��� �ð�

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        while (elapsedTime < duration)
        {
            // �ʴ� y ��ġ ����
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, endPositionY, (elapsedTime / duration)), transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ ����
        transform.position = new Vector3(transform.position.x, endPositionY, transform.position.z);

        // ���⼭ �߰����� ���� ���� ���� (��: ������Ʈ �ı�)
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
        if (collision.gameObject.tag == "JS_ship")
        {
           TakeDamage(40);
        }
    }
}
