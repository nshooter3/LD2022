using UnityEngine;
using UnityEngine.EventSystems;

// Taken from https://answers.unity.com/questions/1197380/make-standalone-input-module-ignore-mouse-input.html
public class DisableMouseInputModule : StandaloneInputModule
{
    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
            {
                usedEvent |= SendMoveEventToSelectedObject();
            }

            if (!usedEvent)
            {
                SendSubmitEventToSelectedObject();
            }
        }
    }
}
