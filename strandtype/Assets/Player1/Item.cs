using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Item", menuName = "Item" )]
public class Item : ScriptableObject
{
    public string name;
    public string description;
    public Sprite artwork;
    


    public UnityEvent TheFunction;

    public void CallFunction()
    {
        TheFunction.Invoke();
    }





}
