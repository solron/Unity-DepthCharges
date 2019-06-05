//using System;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]    // Enemy
    private GameObject enemy = default;

    [SerializeField]
    private AudioClip sonarSound = default;

    AudioSource audioPlayer;

    float spawnTimer = 2.0f;
    Vector2 subPos;

    //Borders
    private Vector2 leftBorder;
    private Vector2 rightBorder;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightBorder = Camera.main.ViewportToWorldPoint(Vector3.one);
        Score.Reset();  // Reset score, kills, subslots, ammo
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = Random.Range(2f, 4f);
            SpawnSub();
        }
    }

    void SpawnSub()
    {
        bool slotFound = false;
        int counter = 0;

        int direction = Random.Range(0, 2);
        audioPlayer.PlayOneShot(sonarSound, 1f);

        switch (direction)
        {
            case 0:
                subPos.x = leftBorder.x - 2f;
                break;
            default:
                subPos.x = rightBorder.x + 2f;
                break;
        }

        while (!slotFound)  // find a free slot to spawn the sub
        {                   // only one sub for each depth 
            subPos.y = Random.Range(-4, 2);
            slotFound = Score.ReadSlot((int)subPos.y);
            counter++;
            if (counter > 6)
            {
                subPos.y = Random.Range(-4, 2);
                counter = 0;
                slotFound = true;
            }
        }

        Score.SetSlot((int)subPos.y, false);
        Instantiate(enemy, subPos, Quaternion.identity);
    }
}
