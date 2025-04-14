namespace MyGame.value
{
    using UnityEngine;

    [System.Serializable]
    public class GameValue
    {

        /**
         * アイテムの種類を列挙型で定義
         * Apple: りんご
         * Orange: みかん
         */
        public enum ItemType
        {
            Apple,
            Orange
        }

        /** 選択中のアイテム */
        public ItemType selectedItem;

        /** 所持金 */
        public int money = 0;

        /** 本日の初期所持金 */
        public int firstOfTheDayMoney = 0;

        /** 本日の売上金額 */
        public int salesAmountOfTheDay = 0;

        /** りんごの在庫 */
        public int appleCount = 0;     // りんごの在庫

        /** みかんの在庫 */
        public int orangeCount = 0;    // みかんの在庫

        /** 時間 */
        public int hour = 0; // 時間

        /** 時間の進行度（秒） */
        public float progSeconds = 0; // 時間の進行度（秒）

        /** 現在営業中かどうかのフラグ */
        public bool isBusinessTime = true; // 現在営業中かどうかのフラグ

        /** 経過日数 */
        public int day = 1;

        /** ノルマ間隔 */
        public int normInterval = 0;

        /** 次回売上徴収日 */
        public int nextNormaDay = 0;

        /** ノルマ金額 */
        public int currentNorma = 0;

        /** 売却間隔 */
        public float sellInterval = 5f;

        /** 売却タイマー */
        public float sellTimer = 0f;
    }
}