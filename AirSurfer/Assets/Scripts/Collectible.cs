using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    //this class can be extended to contain all types of collectible, for now we have just cake
    public enum CollectibleType 
    {
        None,
        Bomb
    }

    [SerializeField] private CollectibleType collectibleType = CollectibleType.Bomb;


    public void GiveCollectible() 
    {
        switch (collectibleType) 
        {
            case CollectibleType.Bomb:
                LevelManager.Instance.PlayCollectibleParticle(transform);
                Destroy(gameObject);
                break;
        }
    }

    public CollectibleType GetCollectibleType() 
    {
        return collectibleType;
    }
}