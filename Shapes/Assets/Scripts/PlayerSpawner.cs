﻿using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private const int maxPlayers = 4;

    private Vector3 _topLeftSpawn;
    private Vector3 _topRightSpawn;
    private Vector3 _bottomLeftSpawn;
    private Vector3 _bottomRightSpawn;

    private Rigidbody _rb;
    private Constants _constants;
    
    private void Awake() => _rb = GetComponent<Rigidbody>();
    
    private void Start()
    {
        _constants = GameObject.FindGameObjectWithTag("Constants")
            .GetComponent<Constants>();

        float spawnWidth = _constants.BoundaryWidth / 2.0f;
        float spawnHeight = _constants.BoundaryHeight / 2.0f;

        _topLeftSpawn = new Vector3(-spawnWidth, spawnHeight, 0.0f);
        _topRightSpawn = new Vector3(spawnWidth, spawnHeight, 0.0f);
        _bottomLeftSpawn = new Vector3(-spawnWidth, -spawnHeight, 0.0f);
        _bottomRightSpawn = new Vector3(spawnWidth, -spawnHeight, 0.0f);

        SetSpawnPosition();
    }

    //Convert to private after removing Debug command from PlayerController.cs
    public void SetSpawnPosition()
    {
        // Currently randomised until multiplayer is implemented
        int RNG = Random.Range(1, maxPlayers + 1);
        switch (RNG)
        {
            case 1:
                _rb.MovePosition(_topLeftSpawn);
                break;
            case 2:
                _rb.MovePosition(_topRightSpawn);
                break;
            case 3:
                _rb.MovePosition(_bottomLeftSpawn);
                break;
            case 4:
                _rb.MovePosition(_bottomRightSpawn);
                break;
        }
        _rb.velocity = Vector3.zero;
    }
}
