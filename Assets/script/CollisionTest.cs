using Unity.VisualScripting;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        print(v);
        this.transform.position += v * this.transform.forward * Time.deltaTime;
        this.transform.rotation = Quaternion.AngleAxis(h, this.transform.up)
            * this.transform.rotation;

        if (Input.GetMouseButtonDown(0))
        {

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            go.transform.position = this.transform.position +
                                   this.transform.forward +
                                   this.transform.up * 0.3f;
            go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            go.transform.rotation = this.transform.rotation;

            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.AddForce(go.transform.forward * 5, ForceMode.VelocityChange);




            Move bulletMove = go.AddComponent<Move>();
            bulletMove.Speed = 20f;

            go.AddComponent<Bullet>();
            Destroy(go, 3.0f);
        }
    }
}

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            return;
        }

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
