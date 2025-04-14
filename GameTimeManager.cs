namespace MyGame
{
    using UnityEngine;
    using TMPro;
    using MyGame.value;

    [System.Serializable]
    public class GameTimeManager : MonoBehaviour
    {
        protected UIObjectValue uIObjectValue; // UIオブジェクトValue
        protected GameValue gameValue; // ゲーム内変数Value

        protected GameRuleManager gameRuleManager; // GameRuleManagerのインスタンス

        protected void Awake()
        {
        }

        protected void Start()
        {
            uIObjectValue = GameManager.Instance.UIObjValue;
            gameValue = GameManager.Instance.GmValue;
            gameRuleManager = GameManager.Instance.RuleManager;

            init();

            UpdateUI();
        }

        /**
         * 初期化処理
         */
        protected void init()
        {
            gameValue.hour = 9; // 初期時間を9時に設定
            gameValue.progSeconds = 0;
            gameValue.isBusinessTime = true; // 営業中
        }

        protected void Update()
        {
            // 
            gameValue.sellTimer += Time.deltaTime;
            if (gameValue.sellTimer >= gameValue.sellInterval)
            {
                gameValue.sellTimer = 0f;
                gameRuleManager.TrySellItem(); // 売れるか判定
            }

            // 時間の進行度を更新
            gameValue.progSeconds += Time.deltaTime;
            if (gameValue.progSeconds >= 10f) // 1秒経過
            {
                if ((9 <= gameValue.hour) && (gameValue.hour <= 16))
                {
                    gameValue.isBusinessTime = true; // 営業中
                    gameValue.hour++; // 時間を1時間進める
                }
                if (gameValue.hour == 17)
                {
                    gameValue.isBusinessTime = false; // 営業終了

                    if (gameValue.day == gameValue.nextNormaDay)
                    {
                        // ノルマ判定用の関数を呼ぶ
                        gameRuleManager.CheckNorma();
                    }

                    // 営業終了時の処理
                    uIObjectValue.SetResultPanelActive(true); // 結果パネルを表示
                }

                gameValue.progSeconds = 0; // 時間の進行度をリセット
            }

            UpdateUI();
        }

        protected void UpdateUI()
        {
            // UIの更新処理をここに記述
            uIObjectValue.clockText.text = gameValue.hour.ToString("D2") + ":00"; // 時間を2桁表示
        }

    }
}