using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JS_turtle_cannon_shot : MonoBehaviour
{
    public Transform firePoint; // �߻� ����
    public GameObject bulletPrefab; // ��ź ������
    public GameObject cannonFireEffect; // CannonFire ��ƼŬ ����Ʈ ������Ʈ
    public float launchForce = 2500f; // ��ź �߻��

    public GameObject[] cannon_front;
    public GameObject[] cannon_right;
    public GameObject[] cannon_left;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٸ� ������ �߻�
        {
            Shoot(1);
            PlayFireEffect();
        }
    }
    void Shoot(int index)
    {
        if (index >= 0 && index < cannon_front.Length)
        {
            // cannon_front[index]�� ���� �߻� ���� ����
        }

        // ��ź �������� �߻� �������� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �߻� ���⿡ �ణ�� ���� ȸ���� �߰�
        Vector3 randomRotation = new Vector3(Random.Range(-10f, 0), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Quaternion rotation = Quaternion.Euler(randomRotation) * bullet.transform.rotation;

        // ��ź�� �߻�� ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �߻�¿� ������ �߰�
            float randomLaunchForce = launchForce + Random.Range(-200f, 200f);
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
