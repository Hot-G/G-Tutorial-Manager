using UnityEngine;

public class UnmaskRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    [Tooltip("Target unmask component. The ray passes through the unmasked rectangle.")]
    [SerializeField] private RectTransform targetRectTransform;


    /// <summary>
    /// Given a point and a camera is the raycast valid.
    /// </summary>
    /// <returns>Valid.</returns>
    /// <param name="sp">Screen position.</param>
    /// <param name="eventCamera">Raycast camera.</param>
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (!isActiveAndEnabled || !targetRectTransform || !targetRectTransform.gameObject.activeSelf)
        {
            return true;
        }

        if (eventCamera)
        {
            return !RectTransformUtility.RectangleContainsScreenPoint(targetRectTransform, sp, eventCamera);
        }
        else
        {
            return !RectTransformUtility.RectangleContainsScreenPoint(targetRectTransform, sp);
        }
    }
}