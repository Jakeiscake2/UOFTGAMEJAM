using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    public int levelNum;
    public int TotalQueueSize;
    public GameObject[] DistributionStations;
    
    [System.Serializable]
    public class Distribution
    {
        public GameObject? depositStation;
        public ColorClass? color;
        public Distribution(GameObject? gameObject, ColorClass? distributedColor)
        {
            depositStation = gameObject;
            color = distributedColor;
        }
    }

    [SerializeField] private List<Distribution> Distributions;
    private int DistributedIndex;
    
    GameObject getRandomDeposit(){
        if(DistributionStations.Length == 0){
            Debug.LogError("No Distribution Center for Random");
        }
        return DistributionStations[Random.Range(0, DistributionStations.Length)];
    }

    ColorClass getRandomColor(){
        return (ColorClass)Random.Range(1, 8);
    }

    void GenerateQueue(){
        for (int i = 0; i < Distributions.Count; i++) 
        {
            if(Distributions[i].depositStation == null){
                Distributions[i].depositStation = getRandomDeposit();
            }
            if(Distributions[i].color == null){
                Distributions[i].color = getRandomColor();
            }
        }
    }

    void Awake()
    {
        if(Distributions.Count > TotalQueueSize){
            Debug.LogError("Distribution count is greater than QueueSize.");
        }
        else if(Distributions.Count < TotalQueueSize){
            Debug.Log("Adding distributions");
            while(Distributions.Count < TotalQueueSize){
                Distributions.Add(new Distribution(null,null));
            }
        }
        GenerateQueue();
        DistributedIndex = 0;
    }

    public void NextDeposit(){
        Distributions[DistributedIndex].depositStation.GetComponent<DepositStation>().setRequiredColor(ColorClass.Blank);
        DistributedIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(DistributedIndex < TotalQueueSize && DistributedIndex < Distributions.Count ){
            Distribution nextDistribution = Distributions[DistributedIndex];
            if(nextDistribution.color != null){
                nextDistribution.depositStation.GetComponent<DepositStation>().setRequiredColor(nextDistribution.color ?? ColorClass.Black);
            }
            else{
                Debug.LogError("Next Distributed Color is null.");
            }   
        }
        else{
            LevelManager.i.NextLevel();
        }
    }
}