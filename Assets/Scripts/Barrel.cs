using UnityEngine;

public class Barrel : MonoBehaviour
{
    float translation;
    float speed = -0.5f;
    private Vector2 bottomBorder;

    void Start()
    {
        bottomBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
    }

    void Update()
    {
        translation = speed * Time.deltaTime;
        transform.Translate(0, translation, 0);

        if (transform.position.y < bottomBorder.y)
        {
            Score.SetCharges(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Submarine")
        {
            Score.SetCharges(1);
            Destroy(gameObject);
        }
    }
}
