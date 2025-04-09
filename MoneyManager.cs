using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameTimeManager;

public class MoneyManager : MonoBehaviour
{
    public GameTimeManager timeManager; // TimeManagerのインスタンスを参照するための変数

    public TextMeshProUGUI moneyText; // 所持金表示用のUI
    public TextMeshProUGUI appleText; // りんごの在庫表示用のUI
    public TextMeshProUGUI orangeText; // みかんの在庫表示用のUI
    public Image selectedItemImage; // 選択中のアイテム表示用のUI
    public GameObject resultPanel; // 結果パネルのUIオブジェクト
    public TextMeshProUGUI resultMoneyText; // 結果パネルの所持金表示用のUI
    public TextMeshProUGUI resultSelesText; // 結果パネルの売上金額表示用のUI


    public Sprite appleSprite;  // りんごのUIオブジェクト
    public Sprite orangeSprite; // みかんのUIオブジェクト

    private int money = 1000;  // 初期所持金
    private int firstOfTheDayMoney = 1000; // 本日の初期所持金
    private int salesAmountOfTheDay = 0; // 本日の売上金額
    private int appleCount = 0;     // りんごの在庫
    private int orangeCount = 0;    // みかんの在庫

    private ItemType selectedItem = ItemType.Apple; //初期選択

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

    /**
     * 初期化処理
     */
    public void Start()
    {
        resultPanel.SetActive(false); // 結果パネルを非表示にする
        SelectApple(); // 初期選択をりんごにする
        UpdateUI();
    }

    /**
     * UI更新処理
     */
    public void UpdateUI()
    {
        moneyText.text = $"{money}円";
        appleText.text = $"{appleCount}個";
        orangeText.text = $"{orangeCount}個";
        resultMoneyText.text = $"{money}円";
        resultSelesText.text = $"{salesAmountOfTheDay}円";
    }

    /********************************************************
     * GameObjectイベント処理
     * GameObjectのアクティベート時の処理をここに記述
     ********************************************************/

    public void OnEnable()
    {
        Debug.Log("OnEnableが起動しました");

        if (resultPanel.activeSelf) // 結果パネルが表示されたとき
        {
            Debug.Log("Result Panelが表示されました");
            resultPanelActivate();
        }
    }

    /**
     * resultPanelアクティベート処理
     */
    public void resultPanelActivate()
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
        selectedItem = ItemType.Apple;
        selectedItemImage.sprite = appleSprite;
        UpdateUI();
    }

    public void SelectOrange()
    {
        selectedItem = ItemType.Orange;
        selectedItemImage.sprite = orangeSprite;
        UpdateUI();
    }

    public void BuyItem()
    {
        if (!timeManager.isBusinessTime) return; // 営業時間外は購入できない
        if (money <= 0) return; // 所持金が0以下なら購入できない
        switch (selectedItem)
        {
            case ItemType.Apple:
                if (money >= 50)
                {
                    money -= 50;
                    appleCount++;
                }
                break;
            case ItemType.Orange:
                if (money >= 80)
                {
                    money -= 80;
                    orangeCount++;
                }
                break;
        }
        UpdateUI();
    }

    public void SellItem()
    {
        if (!timeManager.isBusinessTime) return; // 営業時間外は売却できない

        switch (selectedItem)
        {
            case ItemType.Apple:
                if (appleCount > 0)
                {
                    appleCount--;
                    money += 100;
                }
                break;
            case ItemType.Orange:
                if (orangeCount > 0)
                {
                    orangeCount--;
                    money += 150;
                }
                break;
        }
        UpdateUI();
    }

    public void nextDay()
    {
        resultPanel.SetActive(false); // 結果パネルを非表示にする
        timeManager.day++; // 日数を進める
        timeManager.hour = 9; // 時間を9時にリセット
        firstOfTheDayMoney = money; // 本日の初期所持金を記録
        salesAmountOfTheDay = 0; // 本日の売上金額をリセット

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
        Debug.Log("所持金：" + money);
        Debug.Log("最初の所持金：" + firstOfTheDayMoney);
        salesAmountOfTheDay = (money - firstOfTheDayMoney); // 売上金額を計算
        Debug.Log("売上金："+ salesAmountOfTheDay);
    }
}
