using System.Collections.Generic;
using UnityEngine;

public class CountdownTrigger : InteractableOption {

    [SerializeField] private GameObject succesTarget;
    [SerializeField] private Material indicator;
    [SerializeField] private float counter = 5;
    [SerializeField] private List<CountdownTrigger> targets;
    public bool starter = false;

    private bool isCounting = false;
    private float timer;

    private void Update()
    {
        if (isCounting && timer < 1)
        {
            if (CheckTargets())
            {
                isCounting = false;
                TriggerEvent();
                return;
            }
            indicator.color = Color.Lerp(Color.white, Color.black, timer);
            timer += Time.deltaTime/counter;

        } else if(isCounting && timer >= 1)
        {
            isCounting = false;
            starter = false;
            isSatisfied = false;
            foreach(CountdownTrigger target in targets)
            {
                target.isSatisfied = false;
            }
            indicator.color = Color.white;
        }
    }

    private void TriggerEvent()
    {
        print(1);
    }

    private bool CheckTargets()
    {
        int counter = 0;
        foreach(CountdownTrigger target in targets)
        {
            if (target.isSatisfied)
            {
                counter++;
            }
        }
        return counter == targets.Count;
    }

    public override void InteractWithPlayer()
    {
        isSatisfied = true;
        foreach(CountdownTrigger target in targets)
        {
            if (target.starter)
            {
                return;
            }
        }

        starter = true;
        isCounting = true;
        timer = 0;
    }
}
