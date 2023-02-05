using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Components



    #endregion

    #region Inspector

    [SerializeField]
    GameObject winScreen;

    #endregion

    #region Variables

    List<ObjectiveObj> objectives;

    #endregion

    private void Awake()
    {
        objectives = FindObjectsOfType<ObjectiveObj>().ToList();
    }

    public void RemoveObjective(ObjectiveObj objective)
    {
        objectives.Remove(objective);
        Destroy(objective.gameObject);

        if (objectives.Count <= 0)
        {
            Instantiate(winScreen, transform.Find("Canvas"));
            GameObject.Find("CM vcam1").SetActive(false);
        }
    }
}
