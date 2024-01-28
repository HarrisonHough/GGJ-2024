using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Sprite arrowIcon; // Assign the arrow icon sprite in the Inspector
    [SerializeField] private Sprite speechIcon;
    [SerializeField] private Image iconImage;
    
    // Start is called before the first frame update
    private void Start()
    {
        iconImage.sprite = arrowIcon;
    }

    private void Update()
    {
        Cursor.visible = false; 
        Vector3 mousePosition = Input.mousePosition;
        iconImage.transform.position = mousePosition;
    }
    
    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
    
    public void SetIconToArrow()
    {
        if (iconImage.sprite == arrowIcon) return;
        iconImage.sprite = arrowIcon;
    }
    
    public void SetIconToSpeech()
    {
        iconImage.sprite = speechIcon;
    }
}
