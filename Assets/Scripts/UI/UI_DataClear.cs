using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DataClear : MonoBehaviour
{
    public void OnClickDataClear()
    {
        PlayerPrefs.DeleteAll();
    }
}
