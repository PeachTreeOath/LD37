using UnityEngine;
using UnityEngine.UI;

public class DismissPanelButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(delegate { ClosePanel(); });
    }

    private void ClosePanel()
    {
        UpgradePanelShowHide.instance.ShowHide(false);
        SceneTransitionManager.instance.ReloadRoom();
    }
}