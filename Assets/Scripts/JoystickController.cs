using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image imgBG;
    private Image imgJoystick;
    private Vector2 posInput;

    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isMobilePlatform)
        {
            this.gameObject.SetActive(false);
            return;
        }

        imgBG = this.GetComponent<Image>();
        imgJoystick = this.transform.GetChild(0).GetComponent<Image>();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().joystick = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgBG.rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
        {
            posInput.x = posInput.x / (imgBG.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgBG.rectTransform.sizeDelta.y);

            if (posInput.magnitude > 1f)
                posInput = posInput.normalized;

            imgJoystick.rectTransform.anchoredPosition = new Vector2(
                posInput.x * imgBG.rectTransform.sizeDelta.x / 2, posInput.y * imgBG.rectTransform.sizeDelta.y / 2);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public float InputHorizontal()
    {
        if (posInput.x != 0)
            return posInput.x;
        return Input.GetAxis("Horizontal");
    }

    public float InputVertical()
    {
        if (posInput.y != 0)
            return posInput.y;
        return Input.GetAxis("Vertical");
    }
}
