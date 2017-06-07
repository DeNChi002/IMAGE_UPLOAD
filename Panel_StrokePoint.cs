using UnityEngine;
using System.Collections;

public class Panel_StrokePoint : MonoBehaviour
{
    [Header("開始地点と終了地点")]
    [SerializeField]
    public StrokePoint PointType;

    public enum StrokePoint
    {
        Start,
        End
    }
}
