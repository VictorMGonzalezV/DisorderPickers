using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_ProductionSpeed = 0.5f;//this is a private backing field
    public float ProductionSpeed //notice the missing ;
    {
        //You can write custom getters/setters 
        get { return m_ProductionSpeed; }
        set { //You can also use the custom setter to add validation logic
            if(value<0.0f)
            {
              Debug.LogError("You can't set a negative production value-duh!");
            } else
                {
                  m_ProductionSpeed = value;
                }
              
            }
    }

    private float m_CurrentProduction = 0.0f;

    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        //$ denotes an interpolated string, with the part inside {} being the interpolated expression
        return $"Producing at the speed of {m_ProductionSpeed}/s";
        
    }
    
    
}
