using System.Collections.Generic;
using UnityEngine;

public class PressKeyTutorial : TutorialSection
{
    [SerializeField] private List<KeyCode> keys = new List<KeyCode>();

    public override bool EndCheck()
    {
        return keys.Count == 0;
    }

    public override void Tick()
    {
        if (!Input.anyKey) return;

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i]))
                keys.RemoveAt(i--);
        }
    }
}
