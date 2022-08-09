using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPanel : MonoBehaviour
{
        public void ChangePanel(GameObject panel)
    {
        panel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
