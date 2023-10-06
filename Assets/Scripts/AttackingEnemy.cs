using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : MonoBehaviour
{
    [SerializeField] public float health;

    [HideInInspector] public bool turn = false;
    [HideInInspector] public float incomingDamage;

    void Start()
    {
        incomingDamage = 1;
    }

    void Update()
    {
        
    }
}
