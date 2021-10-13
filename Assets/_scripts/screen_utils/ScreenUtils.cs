using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtils : MonoBehaviour
{
    public static ScreenUtils Instance
    {
        get
        {
            return _instance;
        }
    }

    static ScreenUtils _instance;

    private float screenWidth;
    private float screenHeighth;

    private Vector2 centerPoint;

    public Vector2 CenterPoint { get => centerPoint; private set { centerPoint = value; } }
    public float UpperBound { get => centerPoint.y * 2; }
    public float LowerBound { get => -centerPoint.y * 2; }
    public float LeftBound { get => -centerPoint.x * 2; }
    public float RightBound { get => centerPoint.y * 2; }


    private void OnAwake()
    {
        this.screenHeighth = Screen.height;
        this.screenWidth = Screen.width;

        this.centerPoint = new Vector2(this.screenWidth / 2, this.screenHeighth / 2);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        OnAwake();
    }



}
