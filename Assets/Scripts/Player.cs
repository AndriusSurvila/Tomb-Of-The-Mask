using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 input;
    public int moveSpeed = 20;
    AudioSystem audioSystem;
    public GameObject landParticles;
    public bool hasLanded;
    public string nextLevelName;

    private void Awake()
    {
        audioSystem = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSystem>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Mathf.Abs(newInput.x) > 0 && Mathf.Abs(newInput.y) > 0)
        {
            newInput.y = 0;
        }

        if (rb.velocity.magnitude < 0.1f && !hasLanded && newInput != input)
        {
            Instantiate(landParticles, transform.position, Quaternion.identity);
            hasLanded = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            audioSystem.PLaySounds(audioSystem.move);
        }

        if (newInput != Vector2.zero && rb.velocity.magnitude < 0.1f)
        {
            input = newInput;
            transform.up = -input;
            hasLanded = false;
        }

        rb.velocity = input * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Star"))
        {
            audioSystem.PLaySounds(audioSystem.star);
        }
        else if (other.gameObject.name.Contains("Coin"))
        {
            audioSystem.PLaySounds(audioSystem.coin);
        }
        else if (other.gameObject.name.Contains("Final"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        
        Destroy(other.gameObject);
    }
}