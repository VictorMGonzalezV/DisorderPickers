using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ResourceItem", menuName = "Tutorial/Resource Item")]

//Inherit from ScriptableObject when you need scriptable objects to handle data. In order to make them work use the CreateAssetMenu
//that defines the options you have when right-clicking in the editor to create new instances. This script only defines the template.
public class ResourceItem : ScriptableObject
{
    public string Name;
    public string Id;
    public Sprite Icon;
}
