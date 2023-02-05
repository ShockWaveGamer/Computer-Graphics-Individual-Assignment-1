using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingObject : MonoBehaviour
{
    #region Components



    #endregion

    #region Inspector

    [SerializeField]
    Direction direction;

    [SerializeField]
    float distance, speed;

    #endregion

    #region Variables

    Vector3 startPos;

    #endregion

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float sin = Mathf.Sin(Time.time * speed);

        switch (direction)
        {
            case Direction.X:
                transform.position = startPos + Vector3.right * sin * distance;
                break;
            case Direction.Y:
                transform.position = startPos + Vector3.up * sin * distance;
                break;
            case Direction.Z:
                transform.position = startPos + Vector3.forward * sin * distance;
                break;
            default:
                break;
        }
    }
}

public enum Direction
{
    X,
    Y,
    Z
}
