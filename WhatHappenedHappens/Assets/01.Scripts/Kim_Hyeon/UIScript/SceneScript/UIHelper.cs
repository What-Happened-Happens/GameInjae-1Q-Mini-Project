using UnityEngine;
using UnityEngine.UI;

namespace Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript
{
    public class UIHelper : MonoBehaviour
    {
        // 이미지 오브젝트 셋팅 
        public void SetImageColor(Image targetImage, Color color) => targetImage.color = color;
        public void SetImagePosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
        public void SetImageScale(Image targetImage, Vector3 imageScale) => targetImage.rectTransform.localScale = imageScale;

        // 이미지 스프라이트 셋팅 
        public void SetImageSprite(Image targetImage, Sprite newSprite) => targetImage.sprite = newSprite;

    }
}
