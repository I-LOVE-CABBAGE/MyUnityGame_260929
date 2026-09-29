using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    // 스포너가 알아서 채워줄 바닥(Plane) 정보
    public Collider groundCollider;

    private Rigidbody rb;
    private float changeTimer;
    private Vector3 randomDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PickNewDirection();
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0)
        {
            PickNewDirection();
            TryJump();
            changeTimer = Random.Range(1f, 3f);
        }

        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Plane 범위 안에서만 움직이도록 완벽 제한
        if (groundCollider != null)
        {
            // 직접 만든 Plane의 실제 상하좌우 끝 좌표를 가져옵니다.
            Bounds bounds = groundCollider.bounds;

            // 원기둥 두께(0.5f)만큼 여유를 둬서 바닥 모서리에 걸쳐서 떨어지는 것을 방지
            float clampedX = Mathf.Clamp(transform.position.x, bounds.min.x + 0.5f, bounds.max.x - 0.5f);
            float clampedZ = Mathf.Clamp(transform.position.z, bounds.min.z + 0.5f, bounds.max.z - 0.5f);

            // 만약 Plane 밖으로 삐져나가려고 했다면?
            if (transform.position.x != clampedX || transform.position.z != clampedZ)
            {
                // 강제로 Plane 안쪽으로 위치를 되돌리고 방향을 즉시 바꿉니다.
                transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
                PickNewDirection();
            }
        }
    }

    void PickNewDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, 0, randomZ).normalized;
    }

    void TryJump()
    {
        if (Random.value < 0.3f && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}