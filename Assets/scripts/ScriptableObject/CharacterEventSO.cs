using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<charater> OnEventRaised;
    
    public void RaiseEvent(charater charater)
    {
        OnEventRaised?.Invoke(charater);
    }
}
