using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2;
    //Productivity Units increase the productivity of nearby resource piles
    protected override void BuildingInRange()
    {
        if (m_CurrentPile == null)
        {
            //as Type is shorthand for checking if the type of m_Target is a ResourcePile
            ResourcePile pile = m_Target as ResourcePile;

            if (pile != null)
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
            }
        }
    }

    void ResetProductivity()
    {
        if (m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
        }
    }

    public override void GoTo(Building target)
    {
        ResetProductivity(); 
        base.GoTo(target); 
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }

    //Overriding the base GetName() function to display "Manager"
    public override string GetName()
    {
        return "Manager";
    }

    public override string GetData()
    {
        return "Place next to resource piles to increase productivity.\nCan chase Slackers away";
    }

}

