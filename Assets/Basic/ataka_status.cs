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

    // fireDestroy 파티클 시스템에 대한 참조
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
            // "float"라는 이름의 자식 오브젝트 찾기
            Transform floatChild = transform.Find("float");
            if (floatChild != null) // 오브젝트가 존재하는 경우
            {
                // Buoyant Object 스크립트 찾기
                var buoyantScript = floatChild.GetComponent<WaterSystem.BuoyantObject>(); // BuoyantObject 대신 실제 스크립트의 정확한 이름 사용
                if (buoyantScript != null)
                {
                    buoyantScript.enabled = false; // 스크립트 비활성화
                }
            }

            // Canvas 자식 오브젝트 찾기 및 비활성화
            Transform canvasChild = transform.Find("Canvas");
            if (canvasChild != null) // Canvas 오브젝트가 존재하는 경우
            {
                canvasChild.gameObject.SetActive(false); // 비활성화
            }

            // 파티클 활성화
            fireDestroy1.Play();
            fireDestroy2.Play();
            fireDestroy3.Play();

            // y 값 감소 로직 실행
            StartCoroutine(SinkShip());
        }
    }

    IEnumerator SinkShip()
    {
        float duration = 5f; // 지속 시간
        float endPositionY = transform.position.y - 5; // 최종 y 위치
        float elapsedTime = 0; // 경과 시간

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        while (elapsedTime < duration)
        {
            // 초당 y 위치 감소
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, endPositionY, (elapsedTime / duration)), transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 설정
        transform.position = new Vector3(transform.position.x, endPositionY, transform.position.z);

        // 여기서 추가적인 로직 실행 가능 (예: 오브젝트 파괴)
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
