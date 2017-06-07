using UnityEngine;
using System.Collections;

//このクラスでは、パネル自身が持っている情報を取得するためのものです
public class Panel : MonoBehaviour
{
    [SerializeField]
    public bool Button = false;

    [HideInInspector]
    public PanelManager.PanelMode Mode;

    [HideInInspector]
    public bool Clear = false;

    void OnTriggerEnter(Collider other)
    {
        if (!Clear)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Button)
                {
                    //パネルの入力をする
                    PanelInputSystem.PanelChange(this.gameObject, false);                 
                }
                else if (!Button)
                {
                    //パネルの入力をする
                    PanelInputSystem.PanelChange(this.gameObject, true);
                }
            }
        }            
    }
}
