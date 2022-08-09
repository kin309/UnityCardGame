using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class DeckScriptable : ScriptableObject
{
    public List<GameObject> list = new List<GameObject>();
}
