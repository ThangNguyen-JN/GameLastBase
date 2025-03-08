using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "QuestSystemData", menuName = "Quest System/Quest Data")]
public class QuestSystemData : ScriptableObject
{
    public List<QuestMain> questMain = new List<QuestMain>();
}




