using UnityEngine;

public interface IUpgradable
{
    string upgradeName { get; }
    string upgradeDescription { get; }
    void  Apply();
}

public class upgradeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
