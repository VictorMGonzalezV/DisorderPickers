using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlackerUnit : Unit
{
    private ResourcePile m_CurrentPile = null;
    [Tooltip("Set multiplier <1, Slackers decrease resource productivity")]
    public float ProductivityMultiplier = 0.5f;
    public float RunAwaySpeedMultiplier = 4.0f;
    public Color SlackerColor;
    private Building[] ResourcePiles = null;
    private float StartingSpeed = 0;

    private void Start()
    {
        //Manually set the color to the Slacker color using the inherited SetColor method
        SetColor(SlackerColor);
        //Find all ResourcePiles in the scene and store them in an array. ResourcePile is derived from Building
        ResourcePiles = FindObjectsOfType<ResourcePile>();
        //Cache the starting speed so it can be reset after the speed boost. Speed is inherited from Unit
        StartingSpeed = Speed;
        //Select a target for the Slacker to start moving towards it
        GoTo(SelectTargetPile());
       
    }

    private Building SelectTargetPile()
    {
        //The do-while implementation ensures every random target is different from the previous one. This improved the flow a lot,
        //with only around 3 targets randomly selecting the same target happened too often.
        Building previousTarget = m_Target;
        Building currentTarget = null;
        do
        {
            //Randomly select a ResourcePile and assign it as target
            int index = Random.Range(0, ResourcePiles.Length);
            currentTarget = ResourcePiles[index];
        } while (previousTarget == currentTarget);


        m_Target = currentTarget;
        return currentTarget;
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.GetComponent<ProductivityUnit>())
        {
            //Debug.Log("Work sucks huh huh");
            StartCoroutine(RunAway());
        }
        
    }

    //Implementing the running away behavior as a coroutine allows me to add a temporary speed boost without using the Update method
    private IEnumerator RunAway()
    {
        Speed *= RunAwaySpeedMultiplier;
        //SelectTargetPile();
        GoTo(SelectTargetPile());
        yield return new WaitForSeconds(4.0f);
        Speed = StartingSpeed;
    }
    //Slacker Units decrease the productivity of nearby resource piles
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

    //Overriding the base GetName() function to display "Slacker"
    public override string GetName()
    {
        return "Slacker";
    }

    public override string GetData()
    {
        return "Decreseases productivity of nearby resources.\nDoes not obey commands";
    }
}
