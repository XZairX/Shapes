﻿using System.Collections;
using System.Linq;
using UnityEngine;

public class CubeEmitter : MonoBehaviour
{
    private readonly WaitForSeconds _emitterDelay = new WaitForSeconds(1.0f);

    private GameObject _cube;
    private GameObject[] _cubeEmitters;
    private GameController _gameController;
    private EmitterProperties _emitterProperties;

    private void Start()
    {
        _cube = GameObject.FindGameObjectWithTag("Cube");
        _cubeEmitters = GameObject.FindGameObjectsWithTag("CubeEmitter")
            .OrderBy(g => g.name).ToArray();

        _gameController = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();

        StartCoroutine(EmitCubes());
    }

    private IEnumerator EmitCubes()
    {
        while (_gameController.IsRunning)
        {
            int RNG = Random.Range(0, _cubeEmitters.Length);
            _cubeEmitters[RNG].GetComponent<Renderer>().material.color = Color.blue;
            _emitterProperties = _cubeEmitters[RNG].GetComponent<EmitterProperties>();

            yield return _emitterDelay;

            if (_gameController.IsRunning)
            {
                CubeHandler cube = Instantiate(
                    _cube, _cubeEmitters[RNG].transform.position, Quaternion.identity)
                    .GetComponent<CubeHandler>();

                yield return null;

                cube.SetDirection(
                    _emitterProperties.GetXDirection(), _emitterProperties.GetYDirection());

                _cubeEmitters[RNG].GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
}
