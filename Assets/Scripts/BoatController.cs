using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private GameObject barrel = default;

    [SerializeField]
    private AudioClip splashSound = default;

    [SerializeField]
    private GameObject bomb = default;    // Explosion player

    private Vector2 leftBorder;
    private Vector2 rightBorder;
    private Vector2 spriteHalfSize;
    private Renderer rend;
    private float speed = 4.0f;
    AudioSource audioPlayer;

    float elapsedTime;
    BoxCollider2D mCollider;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        // Border stuff
        rend = GetComponent<SpriteRenderer>();
        leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightBorder = Camera.main.ViewportToWorldPoint(Vector3.one);
        spriteHalfSize = rend.bounds.extents;
        mCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float translate = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(translate, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Score.MyCharges() > 0) {
            Instantiate(barrel, transform.position, Quaternion.identity);
            audioPlayer.PlayOneShot(splashSound, 1f);
            Score.SetCharges(-1);
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1f)
        {
            elapsedTime = elapsedTime % 1;
            UpdateBoatPos();    // Update the boat position once a second for the sub aim
        }
    }

    void UpdateBoatPos()
    {
        Score.SetBoatPos(transform.position);
    }

    private void LateUpdate()
    {
        // Keep the player boat inside the visible screen
        float spriteLeft = transform.position.x - spriteHalfSize.x;
        float spriteRight = transform.position.x + spriteHalfSize.x;

        Vector3 clampedPosition = transform.position;

        if (spriteLeft < leftBorder.x)
            clampedPosition.x = leftBorder.x + spriteHalfSize.x;
        else if (spriteRight > rightBorder.x)
            clampedPosition.x = rightBorder.x - spriteHalfSize.x;

        transform.position = clampedPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bomb" || collision.tag == "Torpedo")
        {
            GameObject ExPlayer = Instantiate(bomb, transform.position, Quaternion.identity);
            Invoke("GoToMenu", 2);  // Go back to menu after 2 seconds
            Destroy(ExPlayer, 2.5f);
            rend.enabled = false;   // Make player invisible 
            mCollider.enabled = false; // Turn of collider
        }
    }

    void GoToMenu()
    {
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
