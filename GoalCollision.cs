using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private MazeGenerator mazeGen;
    private GameManager gameManager;
    private AudioSource audioSource;
    private void Start()
    {
        mazeGen = FindObjectOfType<MazeGenerator>();
        gameManager = FindObjectOfType<GameManager>();  
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player reached the goal");
            audioSource.Play();
            mazeGen.Initialize();
            gameManager.UpdateScoreText();
        }
    }
}
