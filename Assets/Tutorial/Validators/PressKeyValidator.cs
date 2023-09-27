using System.Collections.Generic;
using UnityEngine;

public class PressKeyValidator : TutorialValidator
{
    [SerializeField] private List<KeyCode> keys = new List<KeyCode>();

    public override bool IsValid()
    {
        if (!Input.anyKey) return false;

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i]))
                keys.RemoveAt(i--);
        }

        return keys.Count == 0;
    }
}
