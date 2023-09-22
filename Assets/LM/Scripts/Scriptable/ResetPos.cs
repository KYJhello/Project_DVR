using LM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetPos", menuName = "Data/ResetPos")]
public class ResetPos : ScriptableObject
{
    public Vector3 PlayerInBoatPos { get { return FindObjectOfType<BoatMover>().transform.localPosition + new Vector3(0, 1, 4); } }
    public Vector3 PlayerInRestaurantPos { get { return Vector3.zero; } }
    public Vector3 GunPos;
    public Vector3 HelmetPos;
}
