using UnityEngine;
using UnityEngine.Events;

public class ButtonActivator : MonoBehaviour
{

    public enum PulsationType { CONTINUOUS, DISCRETE }
    public PulsationType pulsation = PulsationType.DISCRETE;

    [Header("Evento al pulsar (o evento mientras está pulsado)")]
    public UnityEvent activationEvent;
    [Header("Evento al despulsar (o evento mientras no esté pulsado)")]
    public UnityEvent deactivationEvent;

    //private ActivableAnimation pushable;
    private bool lastCheck;

    // Use this for initialization
    // void Start()
    // {
    //     pushable = transform.Find("Pushable").GetComponent<ActivableAnimation>();
    //     lastCheck = pushable.IsInActivatedPosition();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (pulsation == PulsationType.CONTINUOUS)
    //     {
    //         if (pushable.IsInActivatedPosition())
    //             activationEvent.Invoke();
    //         else
    //             deactivationEvent.Invoke();
    //     }
    //     else if (pulsation == PulsationType.DISCRETE)
    //     {
    //         if (lastCheck != pushable.IsInActivatedPosition())
    //         {
    //             lastCheck = pushable.IsInActivatedPosition();
    //             if (pushable.IsInActivatedPosition())
    //                 activationEvent.Invoke();
    //             else
    //                 deactivationEvent.Invoke();
    //         }
    //     }
    // }
}
