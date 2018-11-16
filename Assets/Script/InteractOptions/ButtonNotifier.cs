using System.Collections.Generic;
using UnityEngine;

public class ButtonNotifier : InteractableOption {

    [SerializeField] private List<ButtonSelector> buttons;

    public override void InteractWithPlayer()
    {
        foreach(ButtonSelector button in buttons)
        {
            if (button.GetBool())
            {
                button.TriggerTarget();
                button.InteractWithPlayer();
            }
        }
    }
}
