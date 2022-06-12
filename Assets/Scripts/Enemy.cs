using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    [SerializeField] bool canShoot = false;
    [SerializeField] GameObject projectile;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume = 0.25f;

    [SerializeField] TMP_Text displayedText;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        UpdateTextbox();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        if (canShoot)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile,
            transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CheckWords();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
    void UpdateTextbox()
    {
        var reader = FindObjectOfType<CsvReader>();
        displayedText.text = reader.ReturnRandomEnglishWordFromList();
    }

    private void CheckWords()
    {
        var currentWord = FindObjectOfType<CurrentWord>();
        if (currentWord.currentPair.englishWord == displayedText.text)
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            currentWord.UpdateTextbox();
        }
        else
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().GetDamageOfWrongWord();
        }
    }
}
