using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupShuffler : BattleUIInterference
{
    [SerializeField]
    private List<ShuffleCup> cups;

    private Vector3[] positions;

    private ShuffleCup swapCup1, swapCup2;
    private Vector3 swapCup1StartPos, swapCup1EndPos;
    private Vector3 swapCup2StartPos, swapCup2EndPos;

    private float introTimer;
    private float introWaitTimer;
    private float swapTime, swapTimer;
    private float waitTime, waitTimer;
    private float doneShufflingTimer = 0.25f;

    private int numSwaps;

    public bool doneShuffling { get; private set; }

    private List<BattleAction> actions;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    protected override void OnInterferenceStart()
    {
        transform.parent = BattleUI.instance.transform;
        gameObject.SetActive(true);
        actions = BattleUI.instance.actions;
        positions = new Vector3[cups.Count];
        for (int i = 0; i < cups.Count; i++)
        {
            positions[i] = cups[i].transform.position;
            cups[i].Init(actions[i], this);
        }
        introTimer = 1f;
        numSwaps = Random.Range(4, 5);
    }

    public override bool OverrideActionSelection()
    {
        return true;
    }

    public override void OnActionSelectionEnd()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!doneShuffling)
        {
            if (introTimer > 0f)
            {
                introTimer -= Time.deltaTime;
                if (introTimer <= 0f)
                {
                    cups.ForEach(cup => cup.SetCovered(true));
                    introWaitTimer = 0.75f;
                }
            }
            else if (introWaitTimer > 0f)
            {
                introWaitTimer -= Time.deltaTime;
            }
            else if (numSwaps > 0)
            {
                if (swapTimer > 0f)
                {
                    swapTimer -= Time.deltaTime;
                    swapCup1.transform.position = Vector3.Lerp(swapCup1StartPos, swapCup1EndPos, 1f - (swapTimer / swapTime));
                    swapCup2.transform.position = Vector3.Lerp(swapCup2StartPos, swapCup2EndPos, 1f - (swapTimer / swapTime));
                }
                else if (waitTimer > 0f)
                {
                    waitTimer -= Time.deltaTime;
                }
                else
                {
                    numSwaps--;
                    SetRandomSwapCups();
                }
            }
            else if (doneShufflingTimer > 0f)
            {
                doneShufflingTimer -= Time.deltaTime;
            }
            else
            {
                doneShuffling = true;
                ShuffleCup leftCup = null;
                float cupX = Mathf.Infinity;
                foreach (ShuffleCup cup in cups)
                {
                    if (cup.transform.position.x < cupX)
                    {
                        leftCup = cup;
                        cupX = cup.transform.position.x;
                    }
                }
                BattleUI.instance.SetSelectedGameObject(leftCup.Button.gameObject);
            }
        }
    }

    public void PickCup(int selection)
    {
        StartCoroutine(LiftCup(selection));
    }

    public IEnumerator LiftCup(int selection)
    {
        yield return new WaitForSeconds(1);
        BattleUI.instance.ChooseAction(selection);
    }

    private void SetRandomSwapCups()
    {
        int cupSwap1 = Random.Range(0, 3);
        int cupSwap2 = Random.Range(0, 3);
        while (cupSwap1 == cupSwap2)
        {
            cupSwap2 = Random.Range(0, 3);
        }
        swapCup1 = cups[cupSwap1];
        swapCup2 = cups[cupSwap2];
        swapCup1StartPos = positions[cupSwap1];
        swapCup1EndPos = positions[cupSwap2];
        swapCup2StartPos = positions[cupSwap2];
        swapCup2EndPos = positions[cupSwap1];

        swapTime = Random.Range(0.25f, 0.35f);
        swapTimer = swapTime;

        waitTime = Random.Range(0.05f, 0.1f);
        waitTimer = waitTime;

        ShuffleCup tempCup = cups[cupSwap1];
        cups[cupSwap1] = cups[cupSwap2];
        cups[cupSwap2] = tempCup;
    }
}
