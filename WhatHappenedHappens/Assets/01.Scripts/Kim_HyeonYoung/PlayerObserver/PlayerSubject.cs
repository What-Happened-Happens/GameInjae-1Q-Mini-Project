using Script.Interface;
using Script.Interface.Assets.Script.PlayerObserver;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSubject : MonoBehaviour
{
    readonly List<IPlayerObserver> _observer = new(); 

    public void Attach(IPlayerObserver observer) => _observer.Add(observer); 
    public void Detach(IPlayerObserver observer) => _observer.Remove(observer); 

    public void Notify(in PlayerEvent Event)
    {
        foreach (var obs in _observer) obs.OnNotify(Event); // Call the observer and watch changes in the state
    }
}
