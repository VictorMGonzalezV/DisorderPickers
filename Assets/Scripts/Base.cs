using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class Base : Building
{ 
    public static Base Instance { get; private set; }
    public ResourceItem[] targetResources;
   
    

    private void Awake()
    {
        Instance = this;
        //This code adds the target resources specified in the editor to the actual inventory in the Base object
        //This setup ensures designers can choose which resources will be tracked for an order
        //The game can already track how much of a resource is stocked on the base

        //Test out what Building.GetContent does, it may be useful for the order tracking
        //Building.GetItem is used to deduct from stock, so order tracking may be better served with a new method
        //Try overriding virtual function GetData to display the order information:returns a string so can't use icons, must change the UI instead
       
        foreach (ResourceItem resource in targetResources)
        {
            m_Inventory.Add(new InventoryEntry()
            {
                Count = 0,
                ResourceId = resource.Id
            }) ;
        }
    }
}
