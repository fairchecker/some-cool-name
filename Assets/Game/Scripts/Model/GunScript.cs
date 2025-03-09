using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    int buletsCount = 5;
    [SerializeField]
    float reloadTime = 0.5f;
    [SerializeField]
    Transform shootPos; // ����� ������
    [SerializeField]
    Mesh bulletMesh;
    [SerializeField]
    Vector3 bulletSize = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    Material bulletMaterial;
    [SerializeField]
    float bulletSpeed = 10f;

    bool canShoot = true;

    public void Shoot()
    {
        if (!canShoot) return;
        for (int i = 0; i < buletsCount; i++)
        {
            GameObject tmp = new GameObject("Bullet");
            // ����� ������� ���� � ����� ������
            tmp.transform.position = shootPos.position;
            // ��������� ����������� �� ������� ������� (�����) � ����� ������
            Vector3 direction = (shootPos.position - transform.position).normalized;
            // ������������� ��������� ����������� ���� (��� Z)
            tmp.transform.forward = direction;

            // ��������� ������ ���� � ������������� ��������
            Bullet bulletComponent = tmp.AddComponent<Bullet>();
            bulletComponent.speed = bulletSpeed;

            tmp.AddComponent<BoxCollider>();
            tmp.AddComponent<Rigidbody>();
            tmp.AddComponent<MeshRenderer>().material = bulletMaterial;
            tmp.AddComponent<MeshFilter>().mesh = bulletMesh;
            tmp.transform.localScale = bulletSize;
            tmp.tag = "bullet";
        }
        canShoot = false;
        Invoke("Reload", reloadTime);
    }
    private void Reload()
    {
        canShoot = true;
    }
}

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void FixedUpdate()
    {
        // ���������� ���� ����� ������������ � ��������� ��� Z
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("bullet"))
        {
            if (collision.transform.CompareTag("enemy"))
            {
                // ��������� ��������� �� �����
            }
            Destroy(gameObject);
        }
    }
}
