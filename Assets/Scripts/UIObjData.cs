using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UIData")]
public class UIObjData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public string activateText;
    public string upgradeText;
    public string descriptionText;
}
