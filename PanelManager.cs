using UnityEngine;
using System.Collections;
using System.Collections.Generic;

# if UNITY_EDITOR
using UnityEditor;
#endif

//パネル謎解きの全体の管理をするもの
public class PanelManager : MonoBehaviour
{
    //総問題数
    [SerializeField]
    public int QuestionSize;

    //成否判定
    [SerializeField]
    public bool[] Questions;

    //ドアのオブジェクト
    [SerializeField]
    public GameObject[] Doors;

    //パネルの集合体
    [SerializeField]
    public GameObject[] PanelSet;

    //パネルを生成するポイント
    [SerializeField]
    public GameObject[] InsPoint;

    //パネルのモードを保存しておく配列
    [SerializeField]
    public PanelMode[] Mode;

    //パズルの難易度
    [SerializeField]
    public GameLevel Level;

    //問題番号
    [SerializeField]
    public int[] QuestionNumber;

    //問題のクリアフラグ
    public static bool ClearFlg = false;

    //ステージのクリアフラグ
    public static bool StageClearFlg = false;


    List<int> Before_random = new List<int>();

    public enum PanelMode
    {
        Normal,
        One_Stroke
    }

    public enum GameLevel
    {
        Tutorial,
        Easy,
        Normal,
        Hard
    }

    void Awake ()
    {
        PanelStrage.QuestionAnswerds = new bool[QuestionSize];

        PanelStrage.PanelMode = new PanelMode[QuestionSize];

        //難易度別にパネル集合体を決める
        switch (Level)
        {
            case GameLevel.Tutorial:

                //ステージの問題数からパネルの集合体を配列に格納
                for (int i = 0; i < QuestionSize; i++)
                {
                    //チュートリアルは決まった順番でパネル集合体を出す
                    GameObject Panels = Instantiate(PanelResources.Panel_Tutorial[i], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                    PanelSet[i] = Panels;                  
                }

                break;

            case GameLevel.Easy:

                Before_random.Clear();

                //ステージの問題数からパネルの集合体を配列に格納
                for (int i = 0; i < QuestionSize; i++)
                {
                    while (true)
                    {
                        //ランダムでパネルの集合体を生成する
                        int _random = Random.Range(0, PanelResources.Panel_Easy.Length);

                        bool RandomFlg = false;

                        //同じパネル集合体にならないようにする
                        for (int q = 0; q < Before_random.Count; q++)
                        {
                            if (Before_random[q] != _random)
                            {
                                RandomFlg = true;

                            }
                            else
                            {
                                RandomFlg = false;
                                Debug.Log("ダブッたのでやり直し");
                            }
                        }

                        //PanelSet配列に何も入っていない場合はパズルを格納する
                        if (PanelSet[0] == null)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Easy[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        //PanelSet配列に同じものが入っていない場合はパズルを格納する
                        else if (RandomFlg)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Easy[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        else
                        {
                            Debug.Log("問題番号[" + _random + "]はダブっています");
                        }
                    }
                }
                break;

            case GameLevel.Normal:

                Before_random.Clear();

                //ステージの問題数からパネルの集合体を配列に格納
                for (int i = 0; i < QuestionSize; i++)
                {
                    while (true)
                    {
                        //ランダムでパネルの集合体を生成する
                        int _random = Random.Range(0, PanelResources.Panel_Normal.Length);

                        bool RandomFlg = false;

                        //同じパネル集合体にならないようにする
                        for (int q = 0; q < Before_random.Count; q++)
                        {
                            if (Before_random[q] != _random)
                            {
                                RandomFlg = true;

                            }
                            else
                            {
                                RandomFlg = false;
                                Debug.Log("ダブッたのでやり直し");
                            }
                        }

                        //PanelSet配列に何も入っていない場合はパズルを格納する
                        if (PanelSet[0] == null)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Normal[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        //PanelSet配列に同じものが入っていない場合はパズルを格納する
                        else if (RandomFlg)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Normal[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        else
                        {
                            Debug.Log("問題番号[" + _random + "]はダブっています");
                        }
                    }
                }

                break;

            case GameLevel.Hard:

                Before_random.Clear();

                //ステージの問題数からパネルの集合体を配列に格納
                for (int i = 0; i < QuestionSize; i++)
                {
                    while (true)
                    {
                        //ランダムでパネルの集合体を生成する
                        int _random = Random.Range(0, PanelResources.Panel_Hard.Length);

                        bool RandomFlg = false;

                        //同じパネル集合体にならないようにする
                        for (int q = 0; q < Before_random.Count; q++)
                        {
                            if (Before_random[q] != _random)
                            {
                                RandomFlg = true;
                                
                            }
                            else
                            {
                                RandomFlg = false;
                                Debug.Log("ダブッたのでやり直し");
                            }
                        }

                        //PanelSet配列に何も入っていない場合はパズルを格納する
                        if (PanelSet[0] == null)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Hard[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        //PanelSet配列に同じものが入っていない場合はパズルを格納する
                        else if (RandomFlg)
                        {
                            GameObject Panels = Instantiate(PanelResources.Panel_Hard[_random], InsPoint[i].transform.position, Quaternion.identity) as GameObject;

                            PanelSet[i] = Panels;

                            Before_random.Add(_random);

                            Debug.Log("[" + PanelSet[i] + "] : 問題番号[" + _random + "]");

                            RandomFlg = false;

                            break;
                        }
                        else
                        {
                            Debug.Log("問題番号[" + _random + "]はダブっています");
                        }
                    }                                 
                }

                break;
        }
       
        //ステージの問題数を取得し、Listに格納
        for (int i = 0; i < QuestionSize; i++)
        {
            //初期は、クリアしていないのでFalseにする
            PanelStrage.QuestionAnswerds[i] = Questions[i];

            //パズルモードを保存する
            PanelStrage.PanelMode[i] = Mode[i];
        }

        PanelSet[PanelStrage.AnswerIndex].GetComponent<PanelInfomation>().SetPanelInfomation();

        SetInfomation();
    }

    void Update()
    {
        if (!StageClearFlg)
        {
            if (ClearFlg)
            {
                //次の問題を作成する---------------------------------------
                //クリア判定を取得
                SetQuestionAnswer();

                //ドアのアニメーションを実行
                Doors[PanelStrage.AnswerIndex].GetComponent<DoorAnimation>().DoorAnimationStart();

                //次の問題へ移る
                PanelStrage.AnswerIndex++;

                //問題配列が総問題数より小さい場合
                if (PanelStrage.AnswerIndex < QuestionSize)
                {
                    //情報を更新し、問題作成
                    SetInfomation();
                }
                else
                {
                    StageClearFlg = true;

                    //Debug.Log("ステージクリア");
                }

                ClearFlg = false;
            }
        }        
    }

    //情報を与えて、問題を生成する
    private void SetInfomation()
    {
        //パネルのStatic情報を全て初期化し、次の問題を作成する
        PanelStrage.PanelInfomationReset();

        //パズルモードを代入する
        PanelStrage.ThisMode = Mode[PanelStrage.AnswerIndex];

        PanelSet[PanelStrage.AnswerIndex].GetComponent<PanelInfomation>().Mode = PanelStrage.ThisMode;

        PanelSet[PanelStrage.AnswerIndex].GetComponent<PanelInfomation>().SetPanelInfomation();
    }

    //正解を保存しておく
    private void SetQuestionAnswer()
    {
        //問題をクリアしたことにより、Trueに変更する
        PanelStrage.QuestionAnswerds[PanelStrage.AnswerIndex] = true;       

        //配列にTrueを代入
        Questions[PanelStrage.AnswerIndex] = true;
        
        //パネルを動作させなくする
        for(int i = 0; i < PanelInfomation.InputObject.Count; i++)
        {
            PanelInfomation.InputObject[i].GetComponent<Panel>().Clear = true;
        }

        //踏んだパネルの情報をすべて削除する
        PanelInfomation.InputObject.Clear();             
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PanelManager))]
public class PanelEditor : Editor
{
    private bool foldout;

    private Vector2 scrollPosition = Vector2.zero;

    public override void OnInspectorGUI()
    {
        PanelManager Edit = target as PanelManager;

        foldout = EditorGUILayout.Foldout(foldout, "パネル情報");

        if (foldout)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();

            Edit.Level = (PanelManager.GameLevel)EditorGUILayout.EnumPopup("難易度", Edit.Level);

            Edit.QuestionSize = EditorGUILayout.IntField("総問題数", Edit.QuestionSize);

            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Debug.Log("変更しました");

                //配列の大きさを設定
                Edit.Questions = new bool[Edit.QuestionSize];
                Edit.Mode = new PanelManager.PanelMode[Edit.QuestionSize];
                Edit.Doors = new GameObject[Edit.QuestionSize];
                Edit.PanelSet = new GameObject[Edit.QuestionSize];
                Edit.InsPoint = new GameObject[Edit.QuestionSize];
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUI.skin.box, GUILayout.Height(300));

            EditorGUILayout.LabelField("問題の情報");

            EditorGUILayout.BeginVertical();

            //リスト表示
            for (int i = 0; i < Edit.QuestionSize; i++)
            {
                Edit.Questions[i] = EditorGUILayout.Toggle("問題番号[" + i.ToString() + "]", Edit.Questions[i]);
                Edit.Doors[i] = EditorGUILayout.ObjectField("ドア", Edit.Doors[i], typeof(GameObject), true) as GameObject;
                Edit.InsPoint[i] = EditorGUILayout.ObjectField("生成する部屋", Edit.InsPoint[i], typeof(GameObject), true) as GameObject;
                Edit.Mode[i] = (PanelManager.PanelMode)EditorGUILayout.EnumPopup("パズルモード", Edit.Mode[i]);

                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();
        } 
    }
}
#endif
