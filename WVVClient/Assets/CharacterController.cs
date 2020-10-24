using System.Collections;
using System.Collections.Generic;
using Live2D.Cubism.Framework;
using Unity.Mathematics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Live2D.Cubism.Framework.CubismEyeBlinkController eyeController;
    public Live2D.Cubism.Core.CubismParameter lSmileParam;
    public Live2D.Cubism.Core.CubismParameter rSmileParam;
    public float eyeOpening;

    void LateUpdate()
    {
        eyeController.EyeOpening = Mathf.Lerp(eyeController.EyeOpening, eyeOpening, Time.deltaTime * 5);

        lSmileParam.BlendToValue(CubismParameterBlendMode.Override, 1.0f - eyeController.EyeOpening);
        rSmileParam.BlendToValue(CubismParameterBlendMode.Override, 1.0f - eyeController.EyeOpening);
    }
}
