using UnityEngine;

public class SubController : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPlayer = default;

    [SerializeField]
    private GameObject bomb = default;

    [SerializeField]
    private GameObject torpedo = default;

    [SerializeField]
    private AudioClip torpedoLaunch = default;

    [SerializeField]
    private AudioClip torpedoWarning = default;

    readonly float speed = 2.0f;
    float translation;
    int direction;

    //Flip the sub
    SpriteRenderer rend;

    // Kill the sub if it cross the screen border
    private Vector2 leftBorder;
    private Vector2 rightBorder;
    private Vector2 spriteHalfSize;

    // Torpedo firing
    float bombCoordinates;
    bool willFireTorpedo = false;
    bool hasFired = false;
    AudioSource audioPlayer;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();

        // Decide if the sub will fire a torpedo
        int torpedoChance = Random.Range(0, 10);
        if (torpedoChance == 7)
        {
            willFireTorpedo = true;
            audioPlayer.PlayOneShot(torpedoWarning, 1f);
        }

        // Check which side of screen it was spawned
        if (transform.position.x > 0)   // Right to left = -1
            direction = -1;             // Left to right = 1
        else
        {
            direction = 1;
            rend.flipX = true;      // flip the sprite if the sub is spawned on left side
        }

        // Set up the screen border variables
        leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightBorder = Camera.main.ViewportToWorldPoint(Vector3.one);
        spriteHalfSize = rend.bounds.extents;

        // pick that position to drop the bomb or torpedo
        bombCoordinates = Random.Range(Score.GetBoatPos().x - 1f, Score.GetBoatPos().x + 1f);
    }

    void Update()
    {
        translation = direction * speed * Time.deltaTime;
        transform.Translate(translation, 0, 0);

        // Release the bomb
        if (direction == -1 && transform.position.x < bombCoordinates && !hasFired)
            BombRelease();
        else if (direction == 1 && transform.position.x > bombCoordinates && !hasFired)
            BombRelease();

        // Kill the sub outside of borders
        if (direction == -1 && (transform.position.x < leftBorder.x - spriteHalfSize.x))
            KillSub();
        else if (direction == 1 && (transform.position.x > rightBorder.x + spriteHalfSize.x))
            KillSub();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrel")
        {
            Score.AddScore((int)transform.position.y);
            GameObject ExPlayer = Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            Destroy(ExPlayer, 2);
            KillSub();
        }
    }

    private void BombRelease()
    {
        if (willFireTorpedo && !hasFired)
        {
            Instantiate(torpedo, transform.position, Quaternion.identity);
            audioPlayer.PlayOneShot(torpedoLaunch, 1f);
            hasFired = true;
        }
        else
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            hasFired = true;
        }
    }

    void KillSub()
    {
        Score.SetSlot((int)transform.position.y, true);
        Destroy(gameObject);
    }
}
