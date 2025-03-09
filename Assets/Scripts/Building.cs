using UnityEngine;

public class Building
{
    public string BuildingName { get; private set; }
    public string ResourceType { get; private set; }
    public int ResourceAmount { get; private set; }
    public float GenerationTime { get; private set; } 

    public Building(string buildingName, string resourceType, int resourceAmount, float generationTime)
    {
        BuildingName   = buildingName;
        ResourceType   = resourceType;
        ResourceAmount = resourceAmount;
        GenerationTime = generationTime;
    }
}
