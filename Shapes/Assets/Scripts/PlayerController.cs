﻿using UnityEngine;

public class PlayerController : MonoBehaviour, IGetAxisService
{
    private float _boundaryWrapDistance;
    private float _speed;
    private IConstants _constants;
    private IGameController _gameController;
    private IGetAxisService _getAxisService;
    private Rigidbody _rb;
    private PlayerSpawner _playerSpawner;

    private void Awake()
    {
        Collider collider = (BoxCollider)NullChecker.TryGet<BoxCollider>(gameObject);
        _boundaryWrapDistance = collider.bounds.size.x;

        _rb = (Rigidbody)NullChecker.TryGet<Rigidbody>(gameObject);
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        _rb.freezeRotation = true;
        _rb.useGravity = false;
    }

    private void Start()
    {
        _getAxisService = this;
        _constants = (IConstants)NullChecker
            .TryFind<Constants>(Tags.Constants, gameObject);

        _gameController = (IGameController)NullChecker
            .TryFind<GameController>(Tags.GameController, gameObject);

        _speed = _constants.HalfGameWidth;
        _playerSpawner = new PlayerSpawner(_constants);
        _playerSpawner.SetSpawnPosition(_rb);
    }
    
    public void RunTestingConstructor(IGetAxisService getAxisService, float speed)
    {
        _getAxisService = getAxisService;
        _speed = speed;
    }

    public float GetAxis(string axis) => Input.GetAxis(axis);

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(
            _getAxisService.GetAxis(UnityInput.Horizontal),
            _getAxisService.GetAxis(UnityInput.Vertical)) * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.CompareTag(Tags.BoundaryGame))
        {
            switch (other.gameObject.tag)
            {
                case nameof(Tags.BoundaryNorth):
                    _rb.MovePosition(new Vector3(
                        _rb.position.x,
                        other.transform.position.y - _boundaryWrapDistance,
                        _rb.position.z));
                    break;
                case nameof(Tags.BoundarySouth):
                    _rb.MovePosition(new Vector3(
                        _rb.position.x,
                        other.transform.position.y + _boundaryWrapDistance,
                        _rb.position.z));
                    break;
                case nameof(Tags.BoundaryEast):
                    _rb.MovePosition(new Vector3(
                        other.transform.position.x - _boundaryWrapDistance,
                        _rb.position.y,
                        _rb.position.z));
                    break;
                case nameof(Tags.BoundaryWest):
                    _rb.MovePosition(new Vector3(
                        other.transform.position.x + _boundaryWrapDistance,
                        _rb.position.y,
                        _rb.position.z));
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.Cube)
            || collision.gameObject.CompareTag(Tags.Sphere))
        {
            if (_gameController.IsInDebugMode)
            {
                RunDebugModeCommand();
            }
            else
            {
                _gameController.StopRunning();
                gameObject.SetActive(false);
            }
        }
    }

    //Convert to SetSpawnPosition() to private after removing Debug command
    private void RunDebugModeCommand()
    {
        _playerSpawner.SetSpawnPosition(_rb);
    }
}
