using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bollet : MonoBehaviour
{
    public int damage;
    public GameObject cannonFireEffect; 

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� ParticleSystem ������Ʈ�� ���� ��
        if (collision.gameObject.GetComponent<ParticleSystem>() == null)
        {
            PlayFireEffect();
            // ������Ʈ �ı�
            Destroy(gameObject);
            //
        }
    }
    private void Update()
    {
        // ������Ʈ�� y��ǥ���� -5 ���Ϸ� ������ ��
        if (transform.position.y <= -5)
        {
            // ������Ʈ �ı�
            Destroy(gameObject);
            //Destroy(cannonFireEffect);
        }
    }
    void PlayFireEffect()
    {
        // CannonFire ����Ʈ�� �����ϴ��� Ȯ��
        if (cannonFireEffect != null)
        {
            // ��ƼŬ �ý����� ��ġ�� ���� ���� ������Ʈ�� ��ġ�� ����
            cannonFireEffect.transform.position = transform.position;
            GameObject effectInstance = Instantiate(cannonFireEffect, transform.position, Quaternion.identity);
            // ��ƼŬ �ý��� ������Ʈ�� ������
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // ��ƼŬ �ý����� �̹� ��� ���̶�� ����
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // ��ƼŬ �ý��� ���
                ps.Play();

                // ��ƼŬ �ý��� ������Ʈ�� 5�� �ڿ� �ı�
                Destroy(effectInstance, 3f);
            }
        }
    }
}
