namespace MyGame
{
    using UnityEngine;
    using TMPro;
    using MyGame.value;

    [System.Serializable]
    public class GameRuleManager : MonoBehaviour
    {
        protected UIObjectValue uIObjectValue; // UIオブジェクトValue
        protected GameValue gameValue; // ゲーム内変数Value

        protected GameManager gameManager; // GameManagerのインスタンス

        protected void Awake()
        {
        }

        protected void Start()
        {
            uIObjectValue = GameManager.Instance.UIObjValue;
            gameValue = GameManager.Instance.GmValue;

            gameManager = GameManager.Instance; // GameManagerのインスタンスを取得

            init();

        }

        protected void Update()
        {

        }

        /**
         * 初期化処理
         */
        protected void init()
        {
            gameValue.normInterval = 5;
            gameValue.nextNormaDay = 5;
            gameValue.currentNorma = 2000; // 初回ノルマ金額
        }

        public void CheckNorma()
        {
            if (gameValue.money < gameValue.currentNorma)
            {
                // 足りない場合：ゲームオーバー、もしくは警告＆マイナスイベントなど
            }
            else
            {
                gameManager.targetAmountCollected(); // 目標金額を達成した場合の処理
                changeNorma();
            }
        }

        protected void changeNorma()
        {
            gameValue.nextNormaDay += gameValue.normInterval;
            gameValue.currentNorma += 1000; // ノルマを段階的に上げる例
        }

        public void TrySellItem()
        {
            // ランダムに商品が売れるか決める
            float randomChance = Random.Range(0f, 1f); // 0~1のランダム値を生成

            if (randomChance < 0.5f) // 50%の確率で売れる
            {
                SellRandomItem(); // ランダムでアイテムを売る
            }
        }

        protected void SellRandomItem()
        {
            // どの商品を売るかランダムに決定
            float randomChance = Random.Range(0f, 1f);

            if (randomChance < 0.5f) // 50%でりんご
            {
                SellApple();
            }
            else if (randomChance < 1f) // 50%でみかん
            {
                SellOrange();
            }
        }

        protected void SellApple()
        {
            if (gameValue.appleCount > 0)
            {
                int randomAmount = Random.Range(1, gameValue.appleCount + 1); // 売れる個数（1～所持数以内でランダム）
                gameValue.appleCount -= randomAmount;
                gameValue.money += randomAmount * 100; // 1個あたり100円
                gameManager.UpdateUI();
            }
        }

        protected void SellOrange()
        {
            if (gameValue.orangeCount > 0)
            {
                int randomAmount = Random.Range(1, gameValue.orangeCount + 1); // 売れる個数（1～所持数以内でランダム）
                gameValue.orangeCount -= randomAmount;
                gameValue.money += randomAmount * 150; // 1個あたり150円
                gameManager.UpdateUI();
            }
        }
    }
}