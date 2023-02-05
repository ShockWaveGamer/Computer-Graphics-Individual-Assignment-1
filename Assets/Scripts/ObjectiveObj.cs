using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveObj : MonoBehaviour
{
    #region Components



    #endregion

    #region Inspector



    #endregion

    #region variables
    #endregion

    private void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, 0, Time.deltaTime * 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hi");
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().RemoveObjective(this);
        }
    }
}
