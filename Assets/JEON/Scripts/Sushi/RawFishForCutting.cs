using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3번째 분리완료
public class RawFishForCutting : MonoBehaviour
{
    private string fishTier;
    private string fishName;
    public string FishTier { get { return fishTier; } set {  fishTier = value; } }
    public string FishName { get { return fishName; } set { fishName = value; } }

    private void Awake()
    {
        Debug.Log($"fishTier = {fishTier}, fishName = {fishName}");
    }
}
