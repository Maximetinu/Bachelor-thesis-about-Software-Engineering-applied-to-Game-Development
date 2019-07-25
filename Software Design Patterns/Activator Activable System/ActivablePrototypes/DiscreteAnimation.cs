///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2017 GambusinoLabs - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script accionable para ejecutar una animacion. La animacion debe ser un estado de un animator y se activara seteando a true "Activate Rotation".
/// Es necesario que el GameObject tenga el animator asociado.
/// Si además, se le asocia un PuzzleController, se informará a este de las orientaciones de las columnas, para que active el puzzle pertinente.
/// </summary>
[RequireComponent(typeof(Animator))]
public class DiscreteAnimation : MonoBehaviour
{
    #region Fields.
    Animator myAnimator;
    string activationParam = "Repetitions";
    float initialSpeed;
    #endregion

    #region Properties.
    [Tooltip("Este script sirve para activar la animation del objeto mediante llamadas a los metodos TriggerAnimation usando un activable, como un boton. \n Si RepetitionsSpeedMultiplier esta activado, le pongas las repeticiones que le pongas, siempre tardara lo mismo. Por tanto, a mas repeticiones, mqs rapido hara la animacion. Esto sirve para coordinar distintos objetos. Es equivalente a llamar a TriggerAnimationAtFixedSpeed")]
    public bool RepetitionsSpeedMultiplier = true;

    [Tooltip("Multiplicador de velocidad de la animacion. Modificar aqui si no se quiere cambiar en el animator ni en la animation.")]
    public float AnimationSpeedFactor = 1.0f;

    [Space(30)]
    [Tooltip("Las orientaciones solo estaran bien interpretadas si la animacion es ciclica y coincide. Por ejemplo, si tenemos una columna cuadrada con una animacion que la rote 90 grados, PossibleOrientations = 4. Si la animacion no es ciclica, ignorar estas variables.")]
    [Header("Puzzles variables")]
    public int PossibleOrientations = 8;
    public int InitialOrientation = 0;
    [HideInInspector] // TO DO: make it read only
    public int CurrentOrientation = 0;


    [Space(15)]
    [Tooltip("Arrastrar aquí el Game Object vacío que contiene el PuzzleController, para informar de las posiciones.")]
    public ColumnOrientationsController PuzzleController;
    #endregion

    #region Methods.
    public void TriggerAnimation(int num_repetitions = 1)
    {
        if (myAnimator.GetInteger(activationParam) == 0)
            myAnimator.SetInteger(activationParam, num_repetitions);

        if (RepetitionsSpeedMultiplier)
            myAnimator.speed = initialSpeed * System.Math.Abs(num_repetitions);
    }

    public void TriggerAnimationAtFixedSpeed(int num_repetitions = 1)
    {
        if (myAnimator.GetInteger(activationParam) == 0)
            myAnimator.SetInteger(activationParam, num_repetitions);

        myAnimator.speed = initialSpeed * System.Math.Abs(num_repetitions);
    }

    [ContextMenu("Log current orientation")]
    void LogCurrentOrientation()
    {
        Debug.Log("DiscreteAnimation: current orientation = " + CurrentOrientation);
    }

    public void OnAnimationFinish()
    {
        if (myAnimator.GetInteger(activationParam) > 0)
            CurrentOrientation = (CurrentOrientation + 1) % PossibleOrientations;
        else
            CurrentOrientation = (PossibleOrientations + (CurrentOrientation - 1)) % PossibleOrientations;
        if (myAnimator.GetInteger(activationParam) == 0)
        {
            OnAllRepetitionsFinish();
        }
    }

    public void OnAllRepetitionsFinish()
    {
        if (PuzzleController != null)
            PuzzleController.ReportOrientation(gameObject.name, CurrentOrientation);
    }
    #endregion

    #region MonoBehaviour methods.

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed *= AnimationSpeedFactor;

        initialSpeed = myAnimator.speed;

        if (!Utils.ContainsParam(myAnimator, activationParam))
            Debug.LogException(new UnityEngine.MissingComponentException("Animator parameter int \"" + activationParam + "\" in Animator " + myAnimator.name + " not found."));
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (PossibleOrientations == 0)
                PossibleOrientations = 1;
            InitialOrientation %= PossibleOrientations;
            CurrentOrientation = (PossibleOrientations + InitialOrientation) % PossibleOrientations;
        }
    }
    #endregion
}
