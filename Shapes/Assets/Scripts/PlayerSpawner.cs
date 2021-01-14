﻿using UnityEngine;

public class PlayerSpawner
{
    private const int maxPlayers = 4;

    private readonly Vector3 _topLeftSpawn;
    private readonly Vector3 _topRightSpawn;
    private readonly Vector3 _bottomLeftSpawn;
    private readonly Vector3 _bottomRightSpawn;
    
    public PlayerSpawner(IConstants constants)
    {
        float spawnWidth = constants.BoundaryWidth / 2.0f;
        float spawnHeight = constants.BoundaryHeight / 2.0f;

        _topLeftSpawn = new Vector3(-spawnWidth, spawnHeight, 0.0f);
        _topRightSpawn = new Vector3(spawnWidth, spawnHeight, 0.0f);
        _bottomLeftSpawn = new Vector3(-spawnWidth, -spawnHeight, 0.0f);
        _bottomRightSpawn = new Vector3(spawnWidth, -spawnHeight, 0.0f);
    }

    public void SetSpawnPosition(Rigidbody rb, int playerID = 0)
    {
        int RNG = playerID == 0 ? Random.Range(1, maxPlayers + 1) : playerID;
        switch (RNG)
        {
            case 1:
                rb.MovePosition(_topLeftSpawn);
                break;
            case 2:
                rb.MovePosition(_topRightSpawn);
                break;
            case 3:
                rb.MovePosition(_bottomLeftSpawn);
                break;
            case 4:
                rb.MovePosition(_bottomRightSpawn);
                break;
        }
        rb.velocity = Vector3.zero;
    }
}
