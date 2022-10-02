using System.Collections.Generic;
using UnityEngine;

public class CupShuffler : MonoBehaviour
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
    private float doneShufflingTimer = 0.5f;

    private int numSwaps;

    private bool startShuffle = false;
    private bool doneShuffling = false;

    private List<BattleAction> actions;

    public void Init(List<BattleAction> actions)
    {
        this.actions = actions;
        positions = new Vector3[cups.Count];
        for (int i = 0; i < cups.Count; i++)
        {
            positions[i] = cups[i].transform.position;
            cups[i].Init(actions[i]);
        }
        introTimer = 3f;
        numSwaps = Random.Range(8, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if (startShuffle)
        {
            if (!doneShuffling)
            {
                if (introTimer > 0f)
                {
                    introTimer -= Time.deltaTime;
                    if (introTimer <= 0f)
                    {
                        cups.ForEach(cup => cup.SetCovered(true));
                        introWaitTimer = 1f;
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
                }
            }
        }
    }

    public BattleAction PickCup(int selection)
    {
        cups[selection].SetCovered(false);
        return cups[selection].battleAction;
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

        swapTime = Random.Range(0.35f, 0.45f);
        swapTimer = swapTime;

        waitTime = Random.Range(0.15f, 0.1f);
        waitTimer = waitTime;

        ShuffleCup tempCup = cups[cupSwap1];
        cups[cupSwap1] = cups[cupSwap2];
        cups[cupSwap2] = tempCup;
    }
}
