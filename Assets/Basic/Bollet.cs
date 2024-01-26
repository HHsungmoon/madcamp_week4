using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bollet : MonoBehaviour
{
    public int damage;
    public GameObject cannonFireEffect; 

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트에 ParticleSystem 컴포넌트가 없을 때
        if (collision.gameObject.GetComponent<ParticleSystem>() == null)
        {
            PlayFireEffect();
            // 오브젝트 파괴
            Destroy(gameObject);
            //
        }
    }
    private void Update()
    {
        // 오브젝트의 y좌표값이 -5 이하로 떨어질 때
        if (transform.position.y <= -5)
        {
            // 오브젝트 파괴
            Destroy(gameObject);
            //Destroy(cannonFireEffect);
        }
    }
    void PlayFireEffect()
    {
        // CannonFire 이펙트가 존재하는지 확인
        if (cannonFireEffect != null)
        {
            // 파티클 시스템의 위치를 현재 게임 오브젝트의 위치로 설정
            cannonFireEffect.transform.position = transform.position;
            GameObject effectInstance = Instantiate(cannonFireEffect, transform.position, Quaternion.identity);
            // 파티클 시스템 컴포넌트를 가져옴
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // 파티클 시스템이 이미 재생 중이라면 정지
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // 파티클 시스템 재생
                ps.Play();

                // 파티클 시스템 오브젝트를 5초 뒤에 파괴
                Destroy(effectInstance, 3f);
            }
        }
    }
}
