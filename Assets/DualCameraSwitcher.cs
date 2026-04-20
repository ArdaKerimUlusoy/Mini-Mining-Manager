using UnityEngine;

public class DualCameraSwitcher : MonoBehaviour
{
    public Camera characterCamera;
    public Camera upgradeCamera;

    private bool isOnUpgradeScreen = false;

    void Start()
    {
        characterCamera.enabled = true;
        upgradeCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Sað týk
        {
            isOnUpgradeScreen = !isOnUpgradeScreen;

            characterCamera.enabled = !isOnUpgradeScreen;
            upgradeCamera.enabled = isOnUpgradeScreen;
        }
    }
}
