using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class DialogObject : ScriptableObject
{
    [TextArea] public string[] dialog;
}
