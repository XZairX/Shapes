﻿using System.Collections;
using System.Linq;
using UnityEngine;

public class CubeEmitter : MonoBehaviour
{
    private GameObject cube;
    private GameObject[] cubeEmitters;
    private GameController gameController;
    private EmitterProperties emitterProperties;

    private void Start()
    {
        cube = GameObject.FindGameObjectWithTag("Cube");
        cubeEmitters = GameObject.FindGameObjectsWithTag("CubeEmitter").OrderBy(gameObject => gameObject.name).ToArray();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        foreach (GameObject cubeEmitter in cubeEmitters)
        {
            cubeEmitter.transform.position = cubeEmitter.GetComponent<EmitterProperties>().SetPosition();
        }

        StartCoroutine(EmitCubes());
    }

    private IEnumerator EmitCubes()
    {
        while (gameController.IsRunning())
        {
            int RNG = Random.Range(0, cubeEmitters.Length);
            cubeEmitters[RNG].GetComponent<Renderer>().material.color = Color.blue;
            emitterProperties = cubeEmitters[RNG].GetComponent<EmitterProperties>();

            yield return new WaitForSeconds(1);

            if (gameController.IsRunning())
            {
                CubeHandler cubeObject = Instantiate(cube, cubeEmitters[RNG].transform.position, Quaternion.identity).GetComponent<CubeHandler>();
                cubeObject.SetDirection(emitterProperties.GetXDirection(), emitterProperties.GetYDirection());

                cubeEmitters[RNG].GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
}
