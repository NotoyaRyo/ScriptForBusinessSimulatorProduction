namespace MyGame.value
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using MyGame;

    [System.Serializable]
    public class UIObjectValue
    {
        protected GameManager gameManager = GameManager.Instance; // GameManagerのインスタンス

        public TextMeshProUGUI daytext; // 日付表示用のUI
        public TextMeshProUGUI clockText; // 時間表示用のUI
        public TextMeshProUGUI moneyText; // 所持金表示用のUI
        public TextMeshProUGUI appleText; // りんごの在庫表示用のUI
        public TextMeshProUGUI orangeText; // みかんの在庫表示用のUI
        public Image selectedItemImage; // 選択中のアイテム表示用のUI
        public GameObject resultPanel; // 結果パネルのUIオブジェクト
        public TextMeshProUGUI resultMoneyText; // 結果パネルの所持金表示用のUI
        public TextMeshProUGUI resultSelesText; // 結果パネルの売上金額表示用のUI
        public TextMeshProUGUI normaText; // ノルマ表示用のUI
        public Sprite appleSprite;  // りんごのUIオブジェクト
        public Sprite orangeSprite; // みかんのUIオブジェクト

        public void SetResultPanelActive(bool active)
        {
            if (active)
            {
                resultPanel.SetActive(active);
                GameEvents.OnResultPanelActivated?.Invoke(); // イベント呼び出し
            }
        }
    }
}