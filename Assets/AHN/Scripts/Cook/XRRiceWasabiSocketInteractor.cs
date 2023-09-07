using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    /// <summary>
    /// 밥에 와사비를 붙일 Socket.
    /// + Material이 아예 없었으면 좋겠음.
    /// </summary>
    public class XRRiceWasabiSocketInteractor : XRSocketInteractor
    {
        protected override void CreateDefaultHoverMaterials()
        {
            base.CreateDefaultHoverMaterials();
            interactableHoverMeshMaterial = null;
            interactableCantHoverMeshMaterial = null;
        }
    }
}