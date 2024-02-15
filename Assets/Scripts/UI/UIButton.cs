using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool Interactable;

    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerUpEvent;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable == false) return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false) return;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable == false) return;
        OnPointerDownEvent?.Invoke();
    }

    

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (Interactable == false) return;
        OnPointerUpEvent?.Invoke();
    }
}
