using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//パネルの基本システムを制御するものです
public class PanelInputSystem : MonoBehaviour
{
    //開始地点のパネルを踏んだかどうか判定するための変数
    public static bool StartFlg = false;

    //現在踏んでいるパネルの座標
    public static Vector3 ThisPosition;

    //領域を判別するための座標
    public static Vector3 Left, Right, Up, Down;

    //パネルを踏んだら点灯し、再度踏んだら消灯する機能
    public static void PanelChange(GameObject ObjectM , bool flg)
    {
        switch (PanelStrage.ThisMode)
        {    
//-----------------------[普通]--------------------------------------------------------------------        
            case PanelManager.PanelMode.Normal:

                //パネルが点灯していない場合
                if (flg)
                {
                    SEMaster.SESound(SEMaster.SoundType.PanelOn);

                    //踏んだパネルの情報を与える
                    PanelInfomation.InputObject.Add(ObjectM);

                    //Materialを変更する
                    ObjectM.GetComponent<MeshRenderer>().material = PanelResources.M_Blue;

                    //解答を保存する
                    AnswerSystem.AnswerPanel(flg);

                    //パネルを踏んだ判定をする
                    ObjectM.GetComponent<Panel>().Button = true;
                }
                else
                {
                    SEMaster.SESound(SEMaster.SoundType.PanelOff);

                    //パネルの情報を削除する
                    PanelInfomation.InputObject.Remove(ObjectM);

                    //Materialを変更する
                    ObjectM.GetComponent<MeshRenderer>().material = PanelResources.M_Invalied;

                    //解答を保存する
                    AnswerSystem.AnswerPanel(flg);

                    //パネルを踏んでいない判定をする
                    ObjectM.GetComponent<Panel>().Button = false;
                }              
                break;

//>--------------------------------[一筆書き]-------------------------------------------<
            case PanelManager.PanelMode.One_Stroke:

                //パネルが点灯してなく、開始地点のパネルの場合
                if (!StartFlg && flg && PanelInfomation.StartPoint == ObjectM)
                {
                    SEMaster.SESound(SEMaster.SoundType.PanelOn);

                    //踏んだパネルの情報を与える
                    PanelInfomation.InputObject.Add(ObjectM);

                    //踏んだパネルの座標を取得
                    ThisPosition = ObjectM.transform.localPosition;

                    //解答を保存する
                    AnswerSystem.AnswerPanel(flg);

                    //開始地点を踏んだとみなす
                    StartFlg = true;

                    //パネルを踏んだ判定をする
                    ObjectM.GetComponent<Panel>().Button = true;

                    //周りのパネルの色を変更する
                    ChangeAreaPanelColor();
                }
                //開始地点を踏むまでは機能しない
                else if (StartFlg)
                {
                    //パネルが点灯してなく、開始地点以外で、左右上下のパネルの場合
                    if (flg && ObjectM != PanelInfomation.StartPoint && (ObjectM.transform.localPosition == Right || ObjectM.transform.localPosition == Left || ObjectM.transform.localPosition == Up || ObjectM.transform.localPosition == Down))
                    {
                        SEMaster.SESound(SEMaster.SoundType.PanelOn);

                        //踏んだパネルの情報を与える
                        PanelInfomation.InputObject.Add(ObjectM);

                        //踏んだパネルの座標を取得
                        ThisPosition = ObjectM.transform.localPosition;

                        //解答を保存する
                        AnswerSystem.AnswerPanel(flg);

                        //パネルを踏んだ判定をする
                        ObjectM.GetComponent<Panel>().Button = true;

                        //周りのパネルの色を変更する
                        ChangeAreaPanelColor();

                    }                    
                    //踏んだパネルが前に踏んだパネルの場合(開始地点は領域変更しないため、最低1つはパネルが点灯している)
                    else if(PanelInfomation.InputObject.Count != 1)
                    {
                        SEMaster.SESound(SEMaster.SoundType.PanelOff);

                        //領域変更がされるかどうかのフラグ
                        bool AreaChangeFlg = false;

                        //踏んでいたパネル内を検索
                        for(int i = 0; i < PanelInfomation.InputObject.Count; i++)
                        {
                            //前に踏んだパネルの場合
                            if(ObjectM == PanelInfomation.InputObject[i])
                            {
                                //領域変更フラグを立てる
                                AreaChangeFlg = true;

                                Debug.Log("前に踏んだよ");
                                break;
                            }
                        }

                        //前に踏んだパネルの場合は、領域を変更する
                        if (AreaChangeFlg)
                        {
                            //パネルを消すまでの場所を計算する
                            int count = PanelInfomation.InputObject.IndexOf(ObjectM) + 1;

                            Debug.Log("消すまでの場所[" + count + "] : 全体の大きさ[" + PanelInfomation.InputObject.Count + "]");

                            //前に踏んだパネルの分、パネルを消す
                            while (count < PanelInfomation.InputObject.Count)
                            {
                                //前に踏んだパネルの踏んでいた判定をFalseにする
                                PanelInfomation.InputObject[PanelInfomation.InputObject.Count - 1].GetComponent<Panel>().Button = false;

                                //Listから前に踏んだパネルの情報を削除する
                                PanelInfomation.InputObject.RemoveAt(PanelInfomation.InputObject.Count - 1);

                                //正答数を減少させる
                                PanelStrage.Answer--;

                                //パネルを消すまでの場所に到達した場合
                                if (count > PanelInfomation.InputObject.Count)
                                {
                                    Debug.Log("全体の大きさ[" + PanelInfomation.InputObject.Count + "]");

                                    break;
                                }
                            }

                            //現在踏んでいる場所を更新する
                            ThisPosition = ObjectM.transform.localPosition;

                            //領域のパネルのMaterialを変更する
                            ChangeAreaPanelColor();
                        }
                        else
                        {
                            //パネルを踏んでいない判定をする
                            ObjectM.GetComponent<Panel>().Button = false;
                        }
                    }
                }
                else
                {
                    //Debug.Log("パネル踏めない");

                    //パネルを踏んでいない判定をする
                    ObjectM.GetComponent<Panel>().Button = false;
                }

                break;
        }
    }

    //領域のパネルのMaterialを変更する
    public static void ChangeAreaPanelColor()
    {
        //上下左右のパネルを取得する
        Left = new Vector3(ThisPosition.x - 1, ThisPosition.y, ThisPosition.z);
        Right = new Vector3(ThisPosition.x + 1, ThisPosition.y, ThisPosition.z);
        Up = new Vector3(ThisPosition.x, ThisPosition.y, ThisPosition.z + 1);
        Down = new Vector3(ThisPosition.x, ThisPosition.y, ThisPosition.z - 1);

        //上下左右のパネルの色を変更する
        for (int i = 0; i < PanelInfomation.ChangePanel.Count; i++)
        {
            //パネルを踏んでいる判定がない場合
            if (!PanelInfomation.ChangePanel[i].GetComponent<Panel>().Button)
            {
                //現在踏んでいるパネルの左の場合
                if (Left == PanelInfomation.ChangePanel[i].transform.localPosition)
                {
                    //Material変更
                    MaterialChange(PanelInfomation.ChangePanel[i], true);                   
                }
                //現在踏んでいるパネルの右の場合
                else if (Right == PanelInfomation.ChangePanel[i].transform.localPosition)
                {
                    //Material変更
                    MaterialChange(PanelInfomation.ChangePanel[i], true);                  
                }
                //現在踏んでいるパネルの上の場合
                else if (Up == PanelInfomation.ChangePanel[i].transform.localPosition)
                {
                    //Material変更
                    MaterialChange(PanelInfomation.ChangePanel[i], true);                   
                }
                //現在踏んでいるパネルの下の場合
                else if (Down == PanelInfomation.ChangePanel[i].transform.localPosition)
                {
                    //Material変更
                    MaterialChange(PanelInfomation.ChangePanel[i], true);                    
                }
                //領域の範囲外の場合
                else
                {
                    //Material変更
                    MaterialChange(PanelInfomation.ChangePanel[i], false);
                }
            }
            //踏んでいるパネルの場合は青色のMaterialに変更しておく
            else
            {
                PanelInfomation.ChangePanel[i].GetComponent<MeshRenderer>().material = PanelResources.M_Blue;
            }
        }
    }

    //パネルのMaterialを変更するメソッド
    public static void MaterialChange(GameObject M_Source, bool flg)
    {
        //領域のパネルの場合
        if (flg)
        {
            //緑色のMaterialに変更する
            M_Source.GetComponent<MeshRenderer>().material = PanelResources.M_Green;
        }
        //領域の範囲外の場合
        else
        {
            //点灯していないMaterialに変更する
            M_Source.GetComponent<MeshRenderer>().material = PanelResources.M_Invalied;
        }        
    }
}