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

        public void changeNorma()
        {
            gameValue.nextNormaDay += gameValue.normInterval;
            gameValue.currentNorma += 1000; // ノルマを段階的に上げる例
        }
    }
}