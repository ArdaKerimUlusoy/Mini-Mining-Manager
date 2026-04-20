using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text moneyText;
    public Text codeText;

    [Header("Panels")]
    public GameObject moneyUpgradePanel;
    public GameObject codeUpgradePanel;

    [Header("Characters")]
    public GameObject character1; // Para karakteri
    public GameObject character2; // Kod karakteri

    [Header("Floating Icons")]
    public GameObject moneyIconPrefab;
    public GameObject codeIconPrefab;

    private float money = 0f;
    private float code = 0f;
    private int activeCharacter = 0; // 0 = money, 1 = code

    [Header("Money Upgrades")]
    public float moneyPerClick = 0.5f;
    public float moneyPassiveRate = 0.2f;

    public float mClickCost = 50f;
    public float mClickBaseIncrease = 0.5f;
    public float mClickCostMultiplier = 1.8f;

    public float mPassiveCost = 100f;
    public float mPassiveBaseIncrease = 0.3f;
    public float mPassiveCostMultiplier = 1.7f;

    [Header("Code Upgrades")]
    public float codePerClick = 0.5f;
    public float codeAutoInterval = 2f;

    public float cClickCost = 50f;
    public float cClickBaseIncrease = 0.5f;
    public float cClickCostMultiplier = 1.8f;

    public float cSpeedCost = 120f;
    public float cSpeedBaseDecrease = 0.1f;
    public float cSpeedCostMultiplier = 2f;

    private float codeTimer = 0f;

    [Header("Button Texts")]
    public Text moneyClickButtonText;
    public Text moneyPassiveButtonText;
    public Text codeClickButtonText;
    public Text codeSpeedButtonText;

    void Start()
    {
        moneyUpgradePanel.SetActive(false);
        codeUpgradePanel.SetActive(false);
        UpdateUIVisibility();
        UpdateButtonTexts();
    }

    void Update()
    {
        // Panel aç/kapat (M tuşu)
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleUpgradePanel();
        }

        // Karakter değişimi (← →)
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activeCharacter = 1 - activeCharacter;
            UpdateUIVisibility();
        }

        // Sol click = kazanç
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        // Pasif para kazancı
        money += moneyPassiveRate * Time.deltaTime;

        // Kod otomatik üretimi
        codeTimer += Time.deltaTime;
        if (codeTimer >= codeAutoInterval)
        {
            code += 1f;
            codeTimer = 0f;
        }

        // UI Güncelle
        moneyText.text = $"Para: {money:F1}₺";
        codeText.text = $"Kod: {code:F1}";
    }

    void HandleClick()
    {
        if (activeCharacter == 0)
        {
            money += moneyPerClick;
            SpawnFloatingIcon(moneyIconPrefab);
        }
        else
        {
            code += codePerClick;
            SpawnFloatingIcon(codeIconPrefab);
        }
    }

    // EKRANA UÇAN İKON EKLEME
    void SpawnFloatingIcon(GameObject prefab)
    {
        if (prefab == null) return;

        // Mouse pozisyonunu dünya pozisyonuna çevir
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        // Prefabı oluştur
        GameObject icon = Instantiate(prefab);
        FloatingIcon fi = icon.GetComponent<FloatingIcon>();

        fi.startPosition = worldPos;
        fi.endPosition = worldPos + new Vector3(0f, 1.5f, 0f);
    }

    // PANEL AÇ/KAPAT
    void ToggleUpgradePanel()
    {
        bool isMoneyChar = activeCharacter == 0;
        bool isCodeChar = activeCharacter == 1;

        if (isMoneyChar)
        {
            bool newState = !moneyUpgradePanel.activeSelf;
            moneyUpgradePanel.SetActive(newState);
            codeUpgradePanel.SetActive(false);
        }
        else
        {
            bool newState = !codeUpgradePanel.activeSelf;
            codeUpgradePanel.SetActive(newState);
            moneyUpgradePanel.SetActive(false);
        }
    }

    // --- MONEY UPGRADES ---
    public void UpgradeMoneyClick()
    {
        if (money >= mClickCost)
        {
            money -= mClickCost;
            moneyPerClick += mClickBaseIncrease;
            mClickCost *= mClickCostMultiplier;
            mClickBaseIncrease *= 1.5f;
            UpdateButtonTexts();
        }
    }

    public void UpgradeMoneyPassive()
    {
        if (money >= mPassiveCost)
        {
            money -= mPassiveCost;
            moneyPassiveRate += mPassiveBaseIncrease;
            mPassiveCost *= mPassiveCostMultiplier;
            mPassiveBaseIncrease *= 1.4f;
            UpdateButtonTexts();
        }
    }

    // --- CODE UPGRADES ---
    public void UpgradeCodeClick()
    {
        if (code >= cClickCost)
        {
            code -= cClickCost;
            codePerClick += cClickBaseIncrease;
            cClickCost *= cClickCostMultiplier;
            cClickBaseIncrease *= 1.5f;
            UpdateButtonTexts();
        }
    }

    public void UpgradeCodeSpeed()
    {
        if (code >= cSpeedCost && codeAutoInterval > 0.3f)
        {
            code -= cSpeedCost;
            codeAutoInterval -= cSpeedBaseDecrease;
            cSpeedCost *= cSpeedCostMultiplier;
            cSpeedBaseDecrease *= 1.2f;
            UpdateButtonTexts();
        }
    }

    // --- UI GÜNCELLEMELERİ ---
    void UpdateUIVisibility()
    {
        moneyText.gameObject.SetActive(activeCharacter == 0);
        codeText.gameObject.SetActive(activeCharacter == 1);

        character1.SetActive(activeCharacter == 0);
        character2.SetActive(activeCharacter == 1);

        // Karakter değişince yanlış panel açıksa kapat
        if (activeCharacter == 0)
        {
            codeUpgradePanel.SetActive(false);
        }
        else
        {
            moneyUpgradePanel.SetActive(false);
        }
    }

    void UpdateButtonTexts()
    {
        moneyClickButtonText.text = $"{mClickCost:F0}₺";
        moneyPassiveButtonText.text = $"{mPassiveCost:F0}₺";
        codeClickButtonText.text = $"{cClickCost:F0}</>";
        codeSpeedButtonText.text = $"{cSpeedCost:F0}</>";
    }
}
