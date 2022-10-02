using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupMayhem : Status
{
    public List<GameObject> popUps;
    public GameObject popUp;
    public float minTime;
    public float maxTime;
    public bool hasLastPopup;

    // Update is called once per frame

    private void Start()
    {
        var newTime = Random.Range(minTime, maxTime);
        StartCoroutine(PopIn(newTime));
    }

    IEnumerator PopIn(float popInTime)
    {
        yield return new WaitForSeconds(popInTime);
        var popUpInstance = Instantiate(popUp, new Vector3(Random.Range(-500, 500), Random.Range(-300, 200), 0), Quaternion.identity);
        popUpInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        popUps.Add(popUpInstance);
        var newTime = Random.Range(minTime,maxTime);
        StartCoroutine(PopIn(newTime));
    }

    void RemoveLastPopUp()
    {
        if (popUps.Count > 0)
        {
            hasLastPopup = true;
            var popUp = popUps[popUps.Count - 1];
            if (!popUp.GetComponent<PopUpBehavior>().isPoppingIn)
            {
                popUps.Remove(popUp);
                Destroy(popUp);
                Debug.Log("Pop Up destroyed.");
                hasLastPopup = false;
            }
        }
    }

    private void Update()
    {
        if (!hasLastPopup)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                RemoveLastPopUp();
            }
        }
    }

}
