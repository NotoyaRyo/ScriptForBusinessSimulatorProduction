namespace MyGame
{
    using UnityEngine;
    using TMPro;
    using MyGame.value;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private UIObjectValue uiObjectValue; // UIオブジェクトvalue
        [SerializeField] private GameValue gameValue; // ゲーム内変数Value
        [SerializeField] private GameTimeManager gameTimeManager; // TimeManagerのインスタンス
        [SerializeField] private GameRuleManager gameRuleManager; // GameRuleManagerのインスタンス

        public UIObjectValue UIObjValue => uiObjectValue;
        public GameValue GmValue => gameValue;

        public GameTimeManager TimeManager => gameTimeManager;
        public GameRuleManager RuleManager => gameRuleManager;

        protected void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // 重複対策
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーンまたぎたいなら
            }

            init();
        }

        public void Start()
        {
            uiObjectValue.resultPanel.SetActive(false); // 結果パネルを非表示にする
            SelectApple(); // 初期選択をりんごにする
            UpdateUI();
        }

        /**
         * UI更新処理
         */
        public void UpdateUI()
        {
            uiObjectValue.daytext.text = $"{gameValue.day}日目"; // 日付を更新
            uiObjectValue.moneyText.text = $"{gameValue.money}円";
            uiObjectValue.appleText.text = $"{gameValue.appleCount}個";
            uiObjectValue.orangeText.text = $"{gameValue.orangeCount}個";
            uiObjectValue.resultMoneyText.text = $"{gameValue.money}円";
            uiObjectValue.resultSelesText.text = $"{gameValue.salesAmountOfTheDay}円";
            uiObjectValue.normaText.text = $"{gameValue.currentNorma}円"; // ノルマ金額を表示
        }

        /**
         * 初期化処理
         */
        protected void init()
        {
            gameValue.money = 1000;
            gameValue.firstOfTheDayMoney = 1000;
            gameValue.salesAmountOfTheDay = 0;
            gameValue.appleCount = 0;
            gameValue.orangeCount = 0;
        }

        /********************************************************
         * GameObjectイベント処理
         * GameObjectのアクティベート時の処理をここに記述
         ********************************************************/

        public void OnEnable()
        {
            Debug.Log("OnEnableが起動しました");

            GameEvents.OnResultPanelActivated += HandleResultPanelActivated;

        }

        private void OnDisable()
        {
            GameEvents.OnResultPanelActivated -= HandleResultPanelActivated;
        }

        /**
         * resultPanelアクティベート処理
         */
        public void HandleResultPanelActivated()
        {
            salesCalculation(); // 売上金額を計算
            UpdateUI();
        }

        /********************************************************
         * ボタン押下イベント処理
         * ボタン押下時の処理をここに記述
         ********************************************************/

        public void SelectApple()
        {
            gameValue.selectedItem = GameValue.ItemType.Apple;
            uiObjectValue.selectedItemImage.sprite = uiObjectValue.appleSprite;
            UpdateUI();
        }

        public void SelectOrange()
        {
            gameValue.selectedItem = GameValue.ItemType.Orange;
            uiObjectValue.selectedItemImage.sprite = uiObjectValue.orangeSprite;
            UpdateUI();
        }

        public void BuyItem()
        {
            if (!gameValue.isBusinessTime) return; // 営業時間外は購入できない
            if (gameValue.money <= 0) return; // 所持金が0以下なら購入できない
            switch (gameValue.selectedItem)
            {
                case GameValue.ItemType.Apple:
                    if (gameValue.money >= 50)
                    {
                        gameValue.money -= 50;
                        gameValue.appleCount++;
                    }
                    break;
                case GameValue.ItemType.Orange:
                    if (gameValue.money >= 80)
                    {
                        gameValue.money -= 80;
                        gameValue.orangeCount++;
                    }
                    break;
            }
            UpdateUI();
        }

        public void SellItem()
        {
            if (!gameValue.isBusinessTime) return; // 営業時間外は売却できない

            switch (gameValue.selectedItem)
            {
                case GameValue.ItemType.Apple:
                    if (gameValue.appleCount > 0)
                    {
                        gameValue.appleCount--;
                        gameValue.money += 100;
                    }
                    break;
                case GameValue.ItemType.Orange:
                    if (gameValue.orangeCount > 0)
                    {
                        gameValue.orangeCount--;
                        gameValue.money += 150;
                    }
                    break;
            }
            UpdateUI();
        }

        public void nextDay()
        {
            gameValue.day++; // 日数を進める
            gameValue.hour = 9; // 時間を9時にリセット
            gameValue.firstOfTheDayMoney = gameValue.money; // 本日の初期所持金を記録
            gameValue.salesAmountOfTheDay = 0; // 本日の売上金額をリセット

            uiObjectValue.resultPanel.SetActive(false); // 結果パネルを非表示にする
            UpdateUI();
        }

        /********************************************************
         * その他処理
         ********************************************************/

        /**
         * 売上金額を計算するメソッド
         * 本日の初期所持金を引いて売上金額を計算
         */
        public void salesCalculation()
        {
            gameValue.salesAmountOfTheDay = (gameValue.money - gameValue.firstOfTheDayMoney); // 売上金額を計算
        }

        /**
         * 目標金額を達成した場合の処理
         */
        public void targetAmountCollected()
        {
            Debug.Log("ノルマ達成！目標金額を徴収します");
            gameValue.money -= gameValue.currentNorma; // ノルマ金額を引く
        }
    }
}