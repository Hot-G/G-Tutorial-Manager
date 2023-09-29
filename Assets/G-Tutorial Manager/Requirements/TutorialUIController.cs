using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : MonoBehaviour
{
    
    private void Awake()
    {
        _infoMaskImage = infoMaskTransform.GetComponent<Image>();
        _infoMaskDefaultSprite = _infoMaskImage.sprite;

        SetHandActive(false);
        SetInfoActive(false);
    }
    
#region Hand UI Region

    [Header("Hand UI Requirements")]
    [SerializeField] private GameObject handPanel;
    public RectTransform handImageTransform;
    public Animator handAnimator;
    public TMP_Text handText;

    public enum HandAnimation
    {
        Click
    }
    
    /// <summary>
    /// Show or hide hand in tutorial.
    /// </summary>
    /// <param name="isActive">Set Hand Active</param>
    /// <returns></returns>
    public TutorialUIController SetHandActive(bool isActive)
    {
        handPanel.SetActive(isActive);

        if (!isActive)
        {
            handImageTransform.localScale = Vector3.one;
            handText.SetText(string.Empty);
            handAnimator.SetTrigger("Reset");
        }
            
        return this;
    }
    
    /// <summary>
    /// Set Hand World Position in Tutorial Screen.
    /// </summary>
    /// <param name="newPosition">It's new position for hand.</param>
    /// <returns></returns>
    public TutorialUIController SetHandPosition(Vector3 newPosition)
    {
        handImageTransform.position = newPosition;

        return this;
    }
    
    public TutorialUIController SetHandText(string text)
    {
        handText.SetText(text);

        return this;
    }
    
    /// <summary>
    /// Play animation on hand.
    /// </summary>
    /// <param name="handAnimation">It's animator trigger parameter.</param>
    /// <returns></returns>
    public TutorialUIController SetHandAnimation(HandAnimation handAnimation)
    {
        handAnimator.SetTrigger(handAnimation.ToString());

        return this;
    }

    /// <summary>
    /// Animate Hand start point to end point in animation time.
    /// </summary>
    /// <param name="startPoint">Hand animation start point. It's world position.</param>
    /// <param name="endPoint">Hand animation end point. It's world position.</param>
    /// <param name="animationTime">It's between 0 and 1</param>
    public void HandAnimateMoveTwoPoints(Vector3 startPoint, Vector3 endPoint, float animationTime)
    {
        handImageTransform.position =
            Vector3.Lerp(startPoint, endPoint, animationTime);
    }
        
    public void HandAnimateClick(Vector3 maxScale, float animationTime)
    {
        handImageTransform.localScale =
            Vector3.Lerp(Vector3.one, maxScale, Mathf.PingPong(animationTime, 0.5f));
    }

#endregion

#region Info UI Region

    [Header("Info UI Requirements")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private RectTransform infoMaskTransform;
    [SerializeField] private GameObject infoMaskBackground;

    private Image _infoMaskImage;
    private Sprite _infoMaskDefaultSprite;
    
    
    public TutorialUIController SetInfoActive(bool isActive, bool showMask = true)
    {
        infoPanel.SetActive(isActive);

        if (isActive)
        {
            infoMaskBackground.SetActive(showMask);
        }
        else
        {
            infoText.SetText(string.Empty);
            _infoMaskImage.sprite = _infoMaskDefaultSprite;
            infoMaskTransform.anchorMin = Vector2.one / 2;
            infoMaskTransform.anchorMax = Vector2.one / 2;
            infoMaskTransform.pivot = Vector2.one / 2;

            SetInfoMaskSize(Vector2.zero);
        }
            
        return this;
    }
    
    public TutorialUIController SetInfoMaskPosition(Vector3 newPosition)
    {
        infoMaskTransform.position = newPosition;

        return this;
    }
    
    public TutorialUIController SetInfoMaskSize(Vector2 size)
    {
        infoMaskTransform.sizeDelta = size;

        return this;
    }
    
    public TutorialUIController SetInfoText(string newText, TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.Bottom)
    {
        infoText.SetText(newText);
        infoText.alignment = textAlignmentOptions;

        return this;
    }

    public TutorialUIController InfoGlowUI(RectTransform targetUI)
    {
        if (targetUI.TryGetComponent(out Button button))
        {
            var targetImage = (Image)button.targetGraphic;
            _infoMaskImage.sprite = targetImage.sprite;
            targetUI = targetImage.rectTransform;
        }
        else if (targetUI.TryGetComponent(out Image image))
        {
            _infoMaskImage.sprite = image.sprite;
        }
        
        infoMaskTransform.anchorMin = targetUI.anchorMin;
        infoMaskTransform.anchorMax = targetUI.anchorMax;
        infoMaskTransform.pivot = targetUI.pivot;
        infoMaskTransform.position = targetUI.position;
        infoMaskTransform.sizeDelta = targetUI.sizeDelta * targetUI.localScale;

        return this;
    }

#endregion

}
