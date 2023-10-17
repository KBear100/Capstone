using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFightAI : MonoBehaviour
{
    [SerializeField] string member;

    bool turn = false;

    void Start()
    {
        turn = false;
    }

    void Update()
    {
        if (turn)
        {

        }
    }
}
