using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniMapMode
{
    Mini, Fullscreen
}
public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;

    [SerializeField]
    Vector2 miniSizeDimensions = new Vector2(15, 15);

    [SerializeField]
    Vector2 fullScreenDimensions = new Vector2(15, 15);

    [SerializeField]
    float scrollSpeed = 0.1f;

    [SerializeField]
    float maxZoom = 5f;

    [SerializeField]
    float minZoom = 1f;

    // [SerializeField]
    // Terrain terrain;

    [SerializeField]
    RectTransform scrollViewRectTransform;

    [SerializeField]
    RectTransform contentRectTransform;

    [SerializeField]
    MiniMapIcon miniMapIconPrefab;

    Matrix4x4 transformationMatrix;
    Vector2 halfVector2 = new Vector2(0.5f, 0.5f);
    private MiniMapMode currentMiniMapMode = MiniMapMode.Mini;
    private MiniMapIcon playerMiniMapIcon;
    Dictionary<MiniMapWorldObject, MiniMapIcon> miniMapWorldObjectsLookup = new Dictionary<MiniMapWorldObject, MiniMapIcon>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CalculateTransformationMatrix();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // toggle mode
            SetMiniMapMode(currentMiniMapMode == MiniMapMode.Mini ? MiniMapMode.Fullscreen : MiniMapMode.Mini);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ScrollMap(scroll);
        UpdateMiniMapIcons();
        CenterMiniMapOnPlayer();
    }

    public void RegisterMiniMapWorldObject(MiniMapWorldObject miniMapWorldObject, bool isPlayer = false)
    {
        var miniMapIcon = Instantiate(miniMapIconPrefab);
        miniMapIcon.transform.SetParent(contentRectTransform);
        miniMapIcon.SetIcon(miniMapWorldObject.Icon);
        miniMapIcon.SetColor(miniMapWorldObject.IconColor);
        miniMapIcon.SetText(miniMapWorldObject.Text);
        miniMapIcon.SetTextSize(miniMapWorldObject.TextSize);
        miniMapWorldObjectsLookup[miniMapWorldObject] = miniMapIcon;

        if (isPlayer)
            playerMiniMapIcon = miniMapIcon;
    }

    public void DestroyCorrespondingMiniMapIcon(MiniMapWorldObject miniMapWorldObject)
    {
        if (miniMapWorldObjectsLookup.TryGetValue(miniMapWorldObject, out MiniMapIcon icon))
        {
            miniMapWorldObjectsLookup.Remove(miniMapWorldObject);
            Destroy(icon.gameObject);
        }
    }

    public void SetMiniMapMode(MiniMapMode mode)
    {
        if (mode == currentMiniMapMode)
            return;

        switch (mode)
        {
            case MiniMapMode.Mini:
                scrollViewRectTransform.sizeDelta = miniSizeDimensions;
                scrollViewRectTransform.anchorMin = Vector2.one;
                scrollViewRectTransform.anchorMax = Vector2.one;
                scrollViewRectTransform.pivot = Vector2.one;
                currentMiniMapMode = MiniMapMode.Mini;
                break;
            case MiniMapMode.Fullscreen:
                scrollViewRectTransform.sizeDelta = fullScreenDimensions;
                scrollViewRectTransform.anchorMin = halfVector2;
                scrollViewRectTransform.anchorMax = halfVector2;
                scrollViewRectTransform.pivot = Vector2.one;
                currentMiniMapMode = MiniMapMode.Fullscreen;
                break;
        }
    }

    void ScrollMap(float scroll)
    {
        if (scroll == 0)
            return;

        float currentMapScale = contentRectTransform.localScale.x;
        // we need to scale the scroll speed by the current map scale to keep the zooming linear
        float scrollAmount = (scroll > 0 ? scrollSpeed : -scrollSpeed) * currentMapScale;
        float newScale = currentMapScale + scrollAmount;
        float clampedScale = Mathf.Clamp(newScale, minZoom, maxZoom);
        contentRectTransform.localScale = Vector3.one * clampedScale;
    }

    void CenterMiniMapOnPlayer()
    {
        if (playerMiniMapIcon != null)
        {
            float mapScale = contentRectTransform.transform.localScale.x;
            // we simply move the map in the opposite direction the player moved, scaled by the mapscale
            contentRectTransform.transform.localPosition = (-playerMiniMapIcon.transform.localPosition * mapScale);
        }
    }

    void UpdateMiniMapIcons()
    {
        // scale icons by the inverse of the mapscale to keep them a consitent size
        foreach (var kvp in miniMapWorldObjectsLookup)
        {
            var miniMapWorldObject = kvp.Key;
            var miniMapIcon = kvp.Value;
            var mapPosition = WorldPositionToMapPosition(miniMapWorldObject.transform.position);

            miniMapIcon.RectTransform.anchoredPosition = mapPosition;
            var rotation = miniMapWorldObject.transform.rotation.eulerAngles;
            miniMapIcon.IconRectTransform.localRotation = Quaternion.AngleAxis(-rotation.y, Vector3.forward);
        }
    }

    Vector2 WorldPositionToMapPosition(Vector3 worldPos)
    {
        var pos = new Vector2(worldPos.x, worldPos.z);
        return transformationMatrix.MultiplyPoint3x4(pos);
    }


    void CalculateTransformationMatrix()
    {
        var miniMapDimensions = contentRectTransform.rect.size;
        Debug.Log(miniMapDimensions);
        var terrainDimensions = new Vector2(80, 80);
        var scaleRatio = miniMapDimensions / terrainDimensions;
        Debug.Log(scaleRatio);
        var translation = new Vector2(-15, -1);
        // var translation = new Vector2(1,0);
        transformationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, scaleRatio);

        //  {scaleRatio.x,   0,           0,   translation.x},
        //  {  0,        scaleRatio.y,    0,   translation.y},
        //  {  0,            0,           0,            0},
        //  {  0,            0,           0,            0}
    }
}