using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] string m_horizontalAxisName = "Horizontal";
    [SerializeField] string m_verticalAxisName = "Vertical";
    [SerializeField] string m_jumpButtonName = "Jump";
    [SerializeField] string m_primaryButtonName = "Primary";
    [SerializeField] string m_secondaryButtonName = "Secondary";
    [SerializeField] string m_specialButtonName = "Special";
    [SerializeField] string m_pauseButtonName = "Pause";

    List<IInputListener> inputListeners = new List<IInputListener>();

    public string HorizontalAxisName { get { return m_horizontalAxisName; } }
    public string VerticalAxisName { get { return m_verticalAxisName; } }
    public string JumpButtonName { get { return m_jumpButtonName; } }
    public string PrimaryButtonName { get { return m_primaryButtonName; } }
    public string SecondaryButtonName { get { return m_secondaryButtonName; } }
    public string PauseButtonName { get { return m_pauseButtonName; } }

    PlayerInput currentFrameInput = PlayerInput.NullInput;

    private void Update()
    {
        Vector2 axis = Input.GetAxis(m_horizontalAxisName) * Vector2.right + Input.GetAxis(m_verticalAxisName) * Vector2.up;

        bool jump = Input.GetButton(m_jumpButtonName);
        bool jumpDown = Input.GetButtonDown(m_jumpButtonName);
        bool jumpUp = Input.GetButtonUp(m_jumpButtonName);

        bool primary = Input.GetButton(m_primaryButtonName);
        bool primaryDown = Input.GetButtonDown(m_primaryButtonName);
        bool primaryUp = Input.GetButtonUp(m_primaryButtonName);

        bool secondary = Input.GetButton(m_secondaryButtonName);
        bool secondaryDown = Input.GetButtonDown(m_secondaryButtonName);
        bool secondaryUp = Input.GetButtonUp(m_secondaryButtonName);

        bool special = Input.GetButton(m_specialButtonName);
        bool specialDown = Input.GetButtonDown(m_specialButtonName);
        bool specialUp = Input.GetButtonUp(m_specialButtonName);

        bool pauseDown = Input.GetButtonDown(m_pauseButtonName);

        PlayerInput.NewInput(out currentFrameInput, axis,
            jump, jumpDown, jumpUp,
            primary, primaryDown, primaryUp,
            secondary, secondaryDown, secondaryUp,
            special, specialDown, specialUp,
            pauseDown);

        // foreach (IInputReceiver receiver in inputReceivers)
        // {
        //     receiver.UpdateInput(currentFrameInput);
        // }

        for (int i = 0; i < inputListeners.Count; i++)
        {
            inputListeners[i].UpdateInput(currentFrameInput);
        }
    }

    public static void RegisterListener(IInputListener newListener)
    {
        if (newListener != null && !InputManager.Instance.inputListeners.Contains(newListener))
        {
            InputManager.Instance.inputListeners.Add(newListener);
            newListener.OnInputEnabled();
        }
        else if (newListener == null)
        {
            Debug.LogError("InputManager: IInputListener a registrar es NULL.");
        }
        else
        {
            Debug.LogError("InputManager: Has intentado registrar un IInputListener que ya estaba suscrito previamente.");
        }
    }

    public static void UnregisterListener(IInputListener listenerToRemove)
    {
        if (listenerToRemove != null && InputManager.Instance.inputListeners.Contains(listenerToRemove))
        {
            listenerToRemove.UpdateInput(PlayerInput.NullInput);
            listenerToRemove.OnInputDisabled();
            InputManager.Instance.inputListeners.Remove(listenerToRemove);
        }
        else if (listenerToRemove == null)
        {
            Debug.LogError("InputManager: IInputListener a que se pretende des-registrar es NULL.");
        }
        else
        {
            Debug.LogError("InputManager: Has intentado des-registrar un IInputListener que no estaba suscrito previamente.");
        }

    }

    public static bool IsRegister(IInputListener receiverToCheck)
    {
        return (Instance.inputListeners.Contains(receiverToCheck));
    }

    public static bool IsRegister(IInputListener[] receiversToCheck)
    {
        bool anyRegister = false;
        foreach (IInputListener receiver in receiversToCheck)
        {
            if (Instance.inputListeners.Contains(receiver))
            {
                anyRegister = true;
            }
        }
        return anyRegister;
    }

    // TO DO: RegisterListenerOnEvent(listener, gameEvent) --> necessity for parametrized events

}



