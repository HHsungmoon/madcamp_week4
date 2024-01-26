using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    public Transform firePoint; // �߻� ����
    public GameObject bulletPrefab; // ��ź ������
    public GameObject cannonFireEffect; // CannonFire ��ƼŬ ����Ʈ ������Ʈ
    public float launchForce = 1200f; // ��ź �߻��

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٸ� ������ �߻�
        {
            Shoot();
            PlayFireEffect();
        }
    }

    void Shoot()
    {
        // ��ź �������� �߻� �������� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �߻� ���⿡ �ణ�� ���� ȸ���� �߰�
        Vector3 randomRotation = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Quaternion rotation = Quaternion.Euler(randomRotation) * bullet.transform.rotation;

        // ��ź�� �߻�� ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �߻�¿� ������ �߰�
            float randomLaunchForce = launchForce + Random.Range(-100f, 100f);
            rb.AddForce(rotation * Vector3.forward * randomLaunchForce);
        }
    }

    void PlayFireEffect()
    {
        // CannonFire ����Ʈ�� �����ϴ��� Ȯ��
        if (cannonFireEffect != null)
        {
            // ��ƼŬ �ý��� ������Ʈ�� ������
            ParticleSystem ps = cannonFireEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // ��ƼŬ �ý����� �̹� ��� ���̶�� ����
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // ��ƼŬ �ý��� ���
                ps.Play();
            }
        }
    }
}
