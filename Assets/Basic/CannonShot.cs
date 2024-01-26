using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CannonShot : MonoBehaviour
{
    public Transform firePoint; // �߻� ����
    public GameObject bulletPrefab; // ��ź ������
    public GameObject cannonFireEffect; // CannonFire ��ƼŬ ����Ʈ ������Ʈ
    public float launchForce = 4000f; // ��ź �߻��

    public int direction;
    private bool canShoot = true;
    

    private void Start()
    {
        Transform grandparent = transform.parent?.parent;
        // grandparent�� null�� �ƴϰ�, �� �̸��� Ȯ���մϴ�.
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
        
        if (Input.GetKeyDown(KeyCode.Period) && direction==1 && canShoot && group==1) // ������ �������
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
        if (Input.GetKeyDown(KeyCode.Comma) && direction==2 && canShoot && group == 1) // ���� �������
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
        if (Input.GetKeyDown(KeyCode.Space) && direction == 3 && canShoot && group == 1) // ���� �������
        {
            DecreaseGrandGrandParentCurrentMP();
            Shoot();
            PlayFireEffect();
        }
    }

    void DecreaseGrandGrandParentCurrentMP()
    {
        // ���� ������Ʈ���� 4�ܰ� ���� �θ� ������Ʈ�� ����
        Transform grandGrandParent = transform.parent?.parent?.parent?.parent?.parent;

        if (grandGrandParent != null)
        {
            // grandGrandParent���� ShipStatus ������Ʈ�� ã��
            Ship_Status shipStatus = grandGrandParent.GetComponent<Ship_Status>();

            if (shipStatus != null)
            {
                // currentMP ������ ã�� �� ���� -1 ��
                shipStatus.currentMP -= 1;
                Debug.Log("-1 mp");
            }
        }
    }
    IEnumerator ShootWithDelay()
    {
        float delay = Random.Range(0f, 1f);
        canShoot = false;
        // �����̸�ŭ ���
        yield return new WaitForSeconds(delay);

        // ������ �Ŀ� Shoot �Լ� ȣ��
        TriggerShoot();

        // �߻� �� 20�� ���� �������� ��ٸ�
        
        yield return new WaitForSeconds(10f);
        canShoot = true;
    }

    // Shoot �Լ�
    public void TriggerShoot()
    {
        // ��ź �������� �߻� �������� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �߻� ���⿡ �ణ�� ���� ȸ���� �߰�
        Vector3 randomRotation = new Vector3(Random.Range(-3f, -3), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        Quaternion rotation = Quaternion.Euler(randomRotation) * bullet.transform.rotation;

        // ��ź�� �߻�� ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �߻�¿� ������ �߰�
            float randomLaunchForce = launchForce + Random.Range(-200f, 200f);
            rb.AddForce(rotation * Vector3.forward * randomLaunchForce);
        }
        PlayFireEffect();
    }

    // ShootWithDelay �ڷ�ƾ�� �����ϴ� �޼ҵ� ����
    public void Shoot()
    {
        if( canShoot == true)
        {
            StartCoroutine(ShootWithDelay());
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