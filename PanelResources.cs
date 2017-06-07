using UnityEngine;
using System.Collections;

//パネルのリソースを管理する
public class PanelResources : MonoBehaviour
{
    [Header("青色のMaterial")]
    [SerializeField]
    Material Blue;

    [Header("緑色のMaterial")]
    [SerializeField]
    Material Green;

    [Header("ねずみ色のMaterial")]
    [SerializeField]
    Material Invalied;

    [Header("チュートリアル用のパネル")]
    [SerializeField]
    GameObject[] TutorialPanels;

    [Header("Easyステージ用のパネル")]
    [SerializeField]
    GameObject[] EasyPanels;

    [Header("Normalステージ用のパネル")]
    [SerializeField]
    GameObject[] NormalPanels;

    [Header("Hardステージ用のパネル")]
    [SerializeField]
    GameObject[] HardPanels;

    public static Material M_Blue, M_Green, M_Invalied;

    public static GameObject[] Panel_Tutorial, Panel_Easy, Panel_Normal, Panel_Hard;

	void Awake ()
    {
        M_Blue = Blue;
        M_Green = Green;
        M_Invalied = Invalied;

        Panel_Tutorial = TutorialPanels;
        Panel_Easy = EasyPanels;
        Panel_Normal = NormalPanels;
        Panel_Hard = HardPanels;
	}
}
