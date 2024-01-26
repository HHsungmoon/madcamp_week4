using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CannonShot : MonoBehaviour
{
    public Transform firePoint; // 발사 지점
    public GameObject bulletPrefab; // 포탄 프리팹
    public GameObject cannonFireEffect; // CannonFire 파티클 이펙트 오브젝트
    public float launchForce = 4000f; // 포탄 발사력

    public int direction;
    private bool canShoot = true;
    

    private void Start()
    {
        Transform grandparent = transform.parent?.parent;
        // grandparent가 null이 아니고, 그 이름을 확인합니다.
        if (grandparent != null)
        {
            if (grandparent.name == "cannonright")
            {
                direction = 1;
            }
            else if (grandparent.name == "cannonleft")
            {
                direction = 2;
            }
            else if (grandparent.name == "cannonfront")
            {
                direction = 3;
            }
        }
    }
    public void Update()
    {
        Transform grandGrandParent = transform.parent?.parent?.parent?.parent?.parent;
        int group=0;
        JP_Hakic_group tmp = grandGrandParent.GetComponent<JP_Hakic_group>();
        if(tmp != null)
        {
            group = tmp.get_JS_group();
        }
        
        if (Input.GetKeyDown(KeyCode.Period) && direction==1 && canShoot && group==1) // 오른쪽 일제사격
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
        if (Input.GetKeyDown(KeyCode.Comma) && direction==2 && canShoot && group == 1) // 왼쪽 일제사격
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
        if (Input.GetKeyDown(KeyCode.Space) && direction == 3 && canShoot && group == 1) // 전면 일제사격
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
    }

    void DecreaseGrandGrandParentCurrentMP()
    {
        // 현재 오브젝트에서 4단계 위의 부모 오브젝트에 접근
        Transform grandGrandParent = transform.parent?.parent?.parent?.parent?.parent;

        if (grandGrandParent != null)
        {
            // grandGrandParent에서 ShipStatus 컴포넌트를 찾음
            Ship_Status shipStatus = grandGrandParent.GetComponent<Ship_Status>();

            if (shipStatus != null)
            {
                // currentMP 변수를 찾아 그 값을 -1 함
                shipStatus.currentMP -= 1;
                Debug.Log("-1 mp");
            }
        }
    }
    IEnumerator ShootWithDelay()
    {
        float delay = Random.Range(0f, 1f);
        canShoot = false;
        // 딜레이만큼 대기
        yield return new WaitForSeconds(delay);

        // 딜레이 후에 Shoot 함수 호출
        TriggerShoot();

        // 발사 후 20초 동안 재장전을 기다림
        
        yield return new WaitForSeconds(10f);
        canShoot = true;
    }

    // Shoot 함수
    public void TriggerShoot()
    {
        // 포탄 프리팹을 발사 지점에서 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 발사 방향에 약간의 랜덤 회전을 추가
        Vector3 randomRotation = new Vector3(Random.Range(-3f, -3), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        Quaternion rotation = Quaternion.Euler(randomRotation) * bullet.transform.rotation;

        // 포탄에 발사력 적용
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 발사력에 랜덤값 추가
            float randomLaunchForce = launchForce + Random.Range(-200f, 200f);
            rb.AddForce(rotation * Vector3.forward * randomLaunchForce);
        }
        PlayFireEffect();
    }

    // ShootWithDelay 코루틴을 시작하는 메소드 예시
    public void Shoot()
    {
        if( canShoot == true)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    void PlayFireEffect()
    {
        // CannonFire 이펙트가 존재하는지 확인
        if (cannonFireEffect != null)
        {
            // 파티클 시스템 컴포넌트를 가져옴
            ParticleSystem ps = cannonFireEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // 파티클 시스템이 이미 재생 중이라면 정지
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // 파티클 시스템 재생
                ps.Play();
            }
        }
    }
}