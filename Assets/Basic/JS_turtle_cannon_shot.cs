using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JS_turtle_cannon_shot : MonoBehaviour
{
    public Transform firePoint; // 발사 지점
    public GameObject bulletPrefab; // 포탄 프리팹
    public GameObject cannonFireEffect; // CannonFire 파티클 이펙트 오브젝트
    public float launchForce = 2500f; // 포탄 발사력

    public GameObject[] cannon_front;
    public GameObject[] cannon_right;
    public GameObject[] cannon_left;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 발사
        {
            Shoot(1);
            PlayFireEffect();
        }
    }
    void Shoot(int index)
    {
        if (index >= 0 && index < cannon_front.Length)
        {
            // cannon_front[index]에 대한 발사 로직 구현
        }

        // 포탄 프리팹을 발사 지점에서 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 발사 방향에 약간의 랜덤 회전을 추가
        Vector3 randomRotation = new Vector3(Random.Range(-10f, 0), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Quaternion rotation = Quaternion.Euler(randomRotation) * bullet.transform.rotation;

        // 포탄에 발사력 적용
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 발사력에 랜덤값 추가
            float randomLaunchForce = launchForce + Random.Range(-200f, 200f);
            rb.AddForce(rotation * Vector3.forward * randomLaunchForce);
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
