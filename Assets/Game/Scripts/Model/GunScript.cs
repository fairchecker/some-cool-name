using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    int buletsCount = 5;
    [SerializeField]
    float reloadTime = 0.5f;
    [SerializeField]
    Transform shootPos; // Точка старта
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
            // Задаём позицию пули в точке старта
            tmp.transform.position = shootPos.position;
            // Вычисляем направление от позиции объекта (пушки) к точке старта
            Vector3 direction = (shootPos.position - transform.position).normalized;
            // Устанавливаем локальное направление пули (ось Z)
            tmp.transform.forward = direction;

            // Добавляем скрипт пули и устанавливаем скорость
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
        // Перемещаем пулю вперёд относительно её локальной оси Z
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("bullet"))
        {
            if (collision.transform.CompareTag("enemy"))
            {
                // Обработка попадания во врага
            }
            Destroy(gameObject);
        }
    }
}
