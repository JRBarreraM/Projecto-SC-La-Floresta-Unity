using UnityEngine;

public class MiniMapWorldObject : MonoBehaviour
{
    // make sure this is true for the player object, this is what is used to center the map
    [SerializeField]
    private bool isPlayer = false;

    public Sprite Icon;
    public Color IconColor = Color.green;
    public string Text;
    public int TextSize = 10;

    private void Start()
    {
        if(isPlayer)
            MiniMap.Instance.RegisterMiniMapWorldObject(this, isPlayer);
    }

    public void Show(){
        MiniMap.Instance.RegisterMiniMapWorldObject(this, isPlayer);
    }

    public void Hide(){
        MiniMap.Instance.DestroyCorrespondingMiniMapIcon(this);
    }

    private void OnDestroy()
    {
        MiniMap.Instance.DestroyCorrespondingMiniMapIcon(this);
    }
}