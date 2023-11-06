using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Lightning Preset", menuName ="Scriptables/Lightning Preset",order =1)]
public class LightningPreset : ScriptableObject
{
    public Gradient _ambientcolor;
    public Gradient _directionalcolor;
    public Gradient _fogcolor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
