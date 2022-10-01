using UnityEngine;

public class FMODTestMusic : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter musicEvent;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Press 'S' to start music");
        Debug.LogWarning("Press 'A' to start player action");
        Debug.LogWarning("Press 'I' to start player idle");
        Debug.LogWarning("Press 'E' to end music because enemy was defeated.");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            musicEvent.Play();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.ENCOUNTER_CONTROLLER,
                (float)FMODEventsAndParameters.EncounterControllerValues.StartBattle);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.ENCOUNTER_CONTROLLER,
                (float)FMODEventsAndParameters.EncounterControllerValues.Action);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.ENCOUNTER_CONTROLLER,
                (float)FMODEventsAndParameters.EncounterControllerValues.Idle);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.ENCOUNTER_CONTROLLER,
                (float)FMODEventsAndParameters.EncounterControllerValues.EndBattle);
        }
    }
}
