using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelInfomation : MonoBehaviour
{
    [Header("パネルモード")]
    [SerializeField]
    public PanelManager.PanelMode Mode;

    [Header("パネル")]
    [SerializeField]
    List<GameObject> Panels = new List<GameObject>();

    public static GameObject StartPoint;

    public static GameObject EndPoint;

    //入力されたオブジェクトを格納するリスト
    public static List<GameObject> InputObject = new List<GameObject>();

    //パネルを全て持っておくリスト
    public static List<GameObject> ChangePanel = new List<GameObject>();

    //入れたくないポイント配列
    //public static int[] NotInsPoint;

    //パネル９枚
    //int[] NinePanels = { 1, 3, 5, 7 };

    //private void SetInsPoint(int count)
    //{
    //    switch (count)
    //    {
    //        //パネルが９枚の場合
    //        case 9:
    //            NotInsPoint = new int[NinePanels.Length];
    //            for(int i = 0; i < NotInsPoint.Length; i++)
    //            {
    //                NotInsPoint[i] = NinePanels[i];
    //            }
    //            break;
    //    }
    //}

    public void SetPanelInfomation()
    {
        //リストを初期化
        InputObject.Clear();
        ChangePanel.Clear();

        //クリアフラグを初期化する
        PanelInputSystem.StartFlg = false;

        //パネルの枚数を取得
        for (int i = 0; i < transform.childCount; i++)
        {
            Panels.Add(transform.GetChild(i).gameObject);

            Panels[i].GetComponent<Panel>().Mode = Mode;

            //パネルの数に応じて、開始地点と終了地点を生成する
            //SetInsPoint(Panels.Count);

            //if (Mode == PanelManager.PanelMode.One_Stroke)
            //{
            //    int S_random = 0;
            //    int E_random = 0;

            //    foreach (int Point in NotInsPoint)
            //    {
            //        S_random = Random.Range(0, transform.childCount + 1);

            //        if (Point != S_random)
            //        {
            //            Panels[S_random].AddComponent<Panel_StrokePoint>();
            //            Panels[S_random].GetComponent<Panel_StrokePoint>().PointType = Panel_StrokePoint.StrokePoint.Start;
            //            break;
            //        }
            //    }

            //    foreach (int Point in NotInsPoint)
            //    {
            //        E_random = Random.Range(0, transform.childCount + 1);

            //        if (Point != E_random && E_random != S_random)
            //        {
            //            Panels[E_random].AddComponent<Panel_StrokePoint>();
            //            Panels[E_random].GetComponent<Panel_StrokePoint>().PointType = Panel_StrokePoint.StrokePoint.End;
            //            break;
            //        }
            //    }

            //}         

            //Panel_StrokePoint(開始地点と終了地点)を持っているパネルの場合
            if(Panels[i].GetComponent<Panel_StrokePoint>() != null)
            {
                //PointTypeを調べる
                switch (Panels[i].GetComponent<Panel_StrokePoint>().PointType)
                {
                    //開始地点の場合
                    case Panel_StrokePoint.StrokePoint.Start:

                        //開始地点となるオブジェクトを取得
                        StartPoint = Panels[i];

                        break;

                    //終了地点の場合
                    case Panel_StrokePoint.StrokePoint.End:

                        //終了地点となるオブジェクトを取得
                        EndPoint = Panels[i];

                        break;
                }
            }

            ChangePanel = Panels;
        }

        //パネルの数を格納する
        PanelStrage.P_Size = Panels.Count;

    }
}