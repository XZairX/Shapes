﻿using System;
using System.Collections;
using UnityEngine;

public class SphereEmitter : MonoBehaviour
{
    private readonly WaitForSeconds _emitterDelay = new WaitForSeconds(3.0f);

    private IGameController _gameController;
    private GameObject _sphere;

    private void Start()
    {
        try
        {
            _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }
        catch (NullReferenceException)
        {
            _gameController = gameObject.AddComponent<GameController>();
        }

        _sphere = GameObject.FindWithTag("Sphere");
        if (_sphere == null)
        {
            _sphere = new GameObject();
            _sphere.AddComponent<SphereHandler>();
        }

        StartCoroutine(EmitSpheres());
    }

    private IEnumerator EmitSpheres()
    {
        while (_gameController.IsRunning)
        {
            yield return _emitterDelay;

            if (_gameController.IsRunning)
            {
                SphereHandler sphere = Instantiate(
                    _sphere, Vector3.up, Quaternion.identity)
                    .GetComponent<SphereHandler>();

                yield return null;

                sphere.SetDirection();
            }
        }
    }
}
