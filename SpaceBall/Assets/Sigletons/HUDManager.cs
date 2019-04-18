using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : _SigletonBehaviour<HUDManager> {

    Dictionary<string, Text> labelDict = new Dictionary<string, Text>();

    public void SetLabel(string labelName, Text label) {
        labelDict.Add(labelName, label);
    }

    public Text GetLabel(string labelName, Text label) {
        return labelDict[labelName];
    }

    public void SetText(string labelName, string message) {
        if(labelName != null) labelDict[labelName].text = message;
    }

    public void Hidden(string labelName, bool state) {
        Color clr = labelDict[labelName].color;
        labelDict[labelName].color = new Color(clr.r, clr.g, clr.b, (state ? 0 : 1));
    }
}