using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    float translation;
    int speed;
    readonly int bombSpeed = 1;
    readonly int torpedoSpeed = 7;
    SpriteRenderer rend;

    private void Start()
    {
        // Deciding the speed based on object tag since it is same script for bomb and torpedo
        rend = GetComponent<SpriteRenderer>();
        switch (rend.tag)
        {
            case "bomb":
                speed = bombSpeed;
                break;
            case "Torpedo":
                speed = torpedoSpeed;
                break;
        }
    }

    void Update()
    {
        // Bullet movement
        translation = 1 * speed * Time.deltaTime;
        transform.Translate(0, translation, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "sky":
                Destroy(gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                break;
        }
    }
}
