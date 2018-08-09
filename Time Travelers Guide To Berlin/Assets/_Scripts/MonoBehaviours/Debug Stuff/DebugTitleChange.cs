using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugTitleChange : MonoBehaviour {

    public string displayOn;
    public string displayOff;

    TextMeshProUGUI title;


    private void Start()
    {
        title = GetComponent<TextMeshProUGUI>();

        ChangeTitle(false);

    }

    public void ChangeTitle(bool toggleOn){

        if (toggleOn)
        {
            title.text = displayOn;
            title.color = Color.green;
        } else
        {
            title.text = displayOff;
            title.color = Color.red;
        }


    }

}
