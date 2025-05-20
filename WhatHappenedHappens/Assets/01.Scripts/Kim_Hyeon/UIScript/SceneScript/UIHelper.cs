using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript
{
    public class UIHelper : MonoBehaviour
    {
        // 이미지 오브젝트 셋팅 
        public void SetImageColor(UnityEngine.UI.Image targetImage, Color color) => targetImage.color = color;
        public void SetImagePosition(UnityEngine.UI.Image targetImage, Vector3 position) => targetImage.transform.position = position;
        public void SetImageScale(UnityEngine.UI.Image targetImage, Vector3 imageScale) => targetImage.rectTransform.localScale = imageScale;
        
        // RectTransform 전용 
        public void SetImageScale(Vector2 targetImageScale, Vector2 imageScale) => targetImageScale = imageScale;
        public void SetImageCanvasPosition(Vector2 anchoredPosition, Vector3 screenPos) => anchoredPosition = screenPos;

        // 이미지 스프라이트 셋팅 
        public void SetImageSprite(UnityEngine.UI.Image targetImage, Sprite newSprite) => targetImage.sprite = newSprite;

    }
}
