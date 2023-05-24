using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrderManager : Building
{
    //I'm deriving the OrderManager from Building to have access to the preexisting logic for storing resources and the UI integration
    public ResourceItem[] resourcesToCollect;
    [Tooltip("Match the size and order with the resources to collect")]
    //I'm manually initializing the array with size[3] because the number of possible resources will remain as 3
    private int[] targetAmounts=new int[3];
    //UI Panels can't be accessed as such from script, so I need a GameObject reference
    public GameObject OrderCompletePanel;
    public GameObject GameOverPanel;
    //Target time to pick the current order before the game is over
    [Tooltip("Input the time in total seconds, not minutes")]
    public float TimeLeft=120.0f;
    //Reference to the timer display text
    public Text CountdownText;
    private bool IsTimerOn = false;
    //Reference to the order number text
    public Text OrderNumberText;
    //Here I can use a static instance like with the Base,this makes it easier for the Base to connect with the OrderManager
    public static OrderManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //Start the order countdown
        IsTimerOn = true;
        //Generate a random order number and update the UI text
        OrderNumberText.text = GenerateOrderNumber();
    }
    
    private void Update()
    {
        //Decrease the time left to pick the order
        if(IsTimerOn)
        {
            if(TimeLeft>0.0f)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer();
            }
            else
            {
                //Debug.Log("Time is UP");
                StartCoroutine(GameOver());
                IsTimerOn = false;
            }
        }
    }
    private void UpdateTimer()
    {
        float minutes = Mathf.FloorToInt(TimeLeft / 60);
        float seconds = Mathf.FloorToInt(TimeLeft % 60);

        CountdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private IEnumerator GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        //This allows the coroutine to continue while the game is paused
        yield return new WaitForSecondsRealtime(3.0f);
        SceneManager.LoadScene(0);
    }

    //Display winning message and reload the scene so players can pick a new order
    private IEnumerator PlayerWin()
    {
        OrderCompletePanel.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }

    private string GenerateOrderNumber()
    {
        //Order numbers in warehouses aren't random digits, they follow an internal logic, so I'm creating one based on the current date.
        System.DateTime date = System.DateTime.Now;
        return "Time to pick Order #" + date.ToString("MM-dd") +"."+ Random.Range(0, 10000);
    }

    //Because of how the original project is set up this was the best way I could find to make order generation work
    private void GenerateNewAmounts()
    {
        for(int i=0;i<3;i++)
        {
            //Ideally the min and max values of the resources to collect should be exposed for designers, but for a small 2-minute loop this feels enough
            targetAmounts[i] = Random.Range(4, 11);
        }
    }

    private void Awake()
    {
        //Initialize instance of OrderManager
        Instance = this;
        //Generate the amount of resources to collect
        GenerateNewAmounts();
        //Assign the amounts to the resource entries
        int i = 0;
        foreach (ResourceItem resource in resourcesToCollect)
        {
           
           m_Inventory.Add(new InventoryEntry()
            {
                Count = targetAmounts[i],
                ResourceId = resource.Id
                
            });
            i++;
        }

    }

    public void CheckOrder()
    {
        int i = 0;
        int targetReached = 0;
        //The public Inventory alias allows direct access to the current amount of a resource in the Base instance
        foreach (InventoryEntry resource in Base.Instance.Inventory)
        {
            if (resource.Count >= targetAmounts[i])
            {
                targetReached++;
            } 
            i++;    
        }
        if (targetReached==targetAmounts.Length)
        {
            //Debug.Log("Order is of complete");
           // OrderCompletePanel.SetActive(true);
            StartCoroutine(PlayerWin());
        }
  


    }


}
