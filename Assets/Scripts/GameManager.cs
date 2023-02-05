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
            StartCoroutine(Win());
        }

        IEnumerator Win()
        {
            Debug.Log("Win");
            
            while (Time.timeScale > 0)
            {
                Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime, 0, Mathf.Infinity);
                yield return new WaitForEndOfFrame();
            }

            Instantiate(winScreen, transform.Find("Canvas"));
            GameObject.Find("CM vcam1").SetActive(false);

            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
    }
}
