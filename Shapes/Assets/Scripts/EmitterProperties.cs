﻿using UnityEngine;

public class EmitterProperties
{
    private const float _negativeOne = -1.0f;
    private const float _positiveOne = 1.0f;
    private const float _collisionScale = 2.0f;

    private readonly float _boundaryOffset;

    private readonly IConstants _constants;    

    public EmitterProperties(IConstants constants, GameObject cube)
    {
        _constants = constants;
        _boundaryOffset = cube.GetComponent<Collider>().bounds.size.x * _collisionScale;
    }

    public Vector3 SetPosition(float x, float y)
    {
        return new Vector3(SetXPosition(x), SetYPosition(y), 0.0f);
    }

    private float SetXPosition(float x)
    {
        if (x < 0.0f)
        {
            x = -_constants.BoundaryWidth + _boundaryOffset;
        }
        else if (x > 0.0f)
        {
            x = _constants.BoundaryWidth - _boundaryOffset;
        }
        else
        {
            x = 0.0f;
        }
        return x;
    }

    private float SetYPosition(float y)
    {
        if (y < 0.0f)
        {
            y = -_constants.BoundaryHeight + _boundaryOffset;
        }
        else if (y > 0.0f)
        {
            y = _constants.BoundaryHeight - _boundaryOffset;
        }
        else
        {
            y = 0.0f;
        }
        return y;
    }

    public float GetXDirection(float x)
    {
        if (x < 0.0f)
        {
            x = _positiveOne;
        }
        else if (x > 0.0f)
        {
            x = _negativeOne;
        }
        else
        {
            x = Random.Range(_negativeOne, _positiveOne);
        }
        return x;
    }

    public float GetYDirection(float y)
    {
        if (y < 0.0f)
        {
            y = _positiveOne;
        }
        else if (y > 0.0f)
        {
            y = _negativeOne;
        }
        else
        {
            y = Random.Range(_negativeOne, _positiveOne);
        }
        return y;
    }
}
