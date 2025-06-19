using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    
    [Header("Reference")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange;
    
    // Components
    private Rigidbody2D rb;
    private Animator anim;
    
    // Direction
    private Vector2 lastMoveDirection = Vector2.down;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
}
