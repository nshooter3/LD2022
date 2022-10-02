using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUpGenerator : MonoBehaviour
{
    public List<GameObject> popUps;
    public GameObject popUp;
    public float minTime;
    public float maxTime;

    private bool spawnPopups = false;

    public static PopUpGenerator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PopIn(float popInTime)
    {
        yield return new WaitForSeconds(popInTime);
        var popUpInstance = Instantiate(popUp, new Vector3(Random.Range(-500, 500), Random.Range(-300, 200), 0), Quaternion.identity);
        popUpInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.POP_UP_OPEN);
        popUps.Add(popUpInstance);
        var newTime = Random.Range(minTime, maxTime);
        StartCoroutine(PopIn(newTime));
    }

    void RemoveLastPopUp()
    {
        if (popUps.Count > 0)
        {
            var popUp = popUps[popUps.Count - 1];
            popUps.Remove(popUp);
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.POP_UP_CLOSE);
            Destroy(popUp);
        }
    }

    private void RemoveAllPopUps()
    {
        for(int i= 0;i<popUps.Count;i++)
        {
            Destroy(popUps[i]);
        }
        popUps.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleSpawnPopups(!spawnPopups);
        }
    }

    public void ToggleSpawnPopups(bool spawnPopups)
    {
        this.spawnPopups = spawnPopups;
        if (spawnPopups)
        {
            var newTime = Random.Range(minTime, maxTime);
            StartCoroutine(PopIn(newTime));
        }
        else
        {
            RemoveAllPopUps();
            StopAllCoroutines();
        }
    }

    public bool IsBlockingInput()
    {
        return popUps.Count > 0;
    }

    public void InterceptInput()
    {
        RemoveLastPopUp();
    }
}
