using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        
        controller = GetComponent<PlayerController>();
        if (controller == null) Debug.LogError("PlayerController not found");
    }
    
}
