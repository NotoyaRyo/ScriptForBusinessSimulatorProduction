using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static MoneyManager;

public class GameTimeManager : MonoBehaviour
{
    public MoneyManager moneyManager; // MoneyManagerのインスタンスを参照するための変数

    public TextMeshProUGUI clockText;

    public int hour = 0; // 時間
    protected float progSeconds = 0; // 時間の進行度（秒）
    public bool isBusinessTime = true; // 現在営業中かどうかのフラグ

    int day = 1; // 1日目からスタート

    void Start()
    {
        hour = 9; // 初期時間を9時に設定
        progSeconds = 0; // 時間の進行度を初期化
        isBusinessTime = true; // 初期状態は営業中

        UpdateUI();
    }

    void Update()
    {
        progSeconds += Time.deltaTime;
        if (progSeconds >= 2f) // 2秒経過
        {
            if ((9 <= hour) && (hour <= 16))
            {
                isBusinessTime = true; // 営業中
                hour++; // 時間を1時間進める
            }
            if(hour == 17)
            {
                isBusinessTime = false; // 営業終了
                moneyManager.resultPanel.SetActive(true); // 結果パネルを表示
                moneyManager.OnEnable();
            }
            
            progSeconds = 0; // 時間の進行度をリセット
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // UIの更新処理をここに記述
        clockText.text = hour.ToString("D2") + ":00"; // 時間を2桁表示
    }

}
