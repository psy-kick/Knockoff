using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SelectableEffectType
{
    ScaleY,
    ScaleXY
}

public class SelectableEffect : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
    public SelectableEffectType selectableEffectType;

    string AudioClipFileName = "Button";

    public void OnPointerDown(PointerEventData eventData)
    {
        Button component = base.transform.GetComponent<Button>();
        if (!(component != null) || component.interactable)
        {
            Sequence s = DOTween.Sequence();

            switch (selectableEffectType)
            {
                case SelectableEffectType.ScaleY:
                    s.Append(base.transform.DOScaleY(1.1f, 0.12f));
                    s.Append(base.transform.DOScaleY(0.85f, 0.09f));
                    s.Append(base.transform.DOScaleY(1f, 0.09f));

                    break;
                case SelectableEffectType.ScaleXY:
                    s.Append(base.transform.DOScaleX(0.95f, 0.1f));
                    s.Join(base.transform.DOScaleY(1.05f, 0.1f));
                    s.Append(base.transform.DOScaleX(1.05f, 0.1f));
                    s.Join(base.transform.DOScaleY(0.95f, 0.1f));
                    s.Append(base.transform.DOScaleX(1f, 0.1f));
                    s.Join(base.transform.DOScaleY(1f, 0.1f));
                    break;
            }

            // Add a check for the object being destroyed before accessing it during tweening
            s.OnStepComplete(() =>
            {
                if (base.transform == null)
                {
                    return;
                }
            });
        }
    }
}
