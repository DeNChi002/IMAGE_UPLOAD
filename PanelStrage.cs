using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//パネルのシステムに関するすべてのStatic情報を管理するものです
public class PanelStrage : MonoBehaviour
{
    //生成したパネルのGameObject
    //public static List<GameObject> Panel = new List<GameObject>();

    //生成するときの整列種類
    //public static PanelManager.InsType[] InsType;

    //パネルのパズルモード
    public static PanelManager.PanelMode[] PanelMode;

    //パネル数
    public static int P_Size;

    //解答数
    public static int Answer;

    //ステージ全体の問題数(正解か不正解か見る)
    public static bool[] QuestionAnswerds;

    //正解した問題の配列用変数
    public static int AnswerIndex = 0;

    //パネル生成用の座標
    public static Vector3 InsPosition = Vector3.zero;

    //現在の問題のパズルのモード
    public static PanelManager.PanelMode ThisMode;

    //パネルに関するStatic情報を全てリセットする
    public static void PanelInfomationReset()
    {
        P_Size = 0;
        Answer = 0;
    }
}
