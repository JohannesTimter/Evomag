using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject DropZone;
    private List<Transform> connectionPoints = new List<Transform>();
    private List<GameObject> connectionPointImages = new List<GameObject>();

    private RectTransform rectTransform;
    private Vector2 startingPosition;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    public GameObject player;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.position;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        FindPossibleConnectionPoints();
        GenerateUIImagesForConnectionPoints();
    }

    private void FindPossibleConnectionPoints()
    {
        for (int i = 0; i < player.gameObject.transform.childCount; i++)
        {
            Transform childTransform = player.gameObject.transform.GetChild(i);

            if (childTransform.CompareTag("ConnectionPoint") && childTransform.childCount == 0)
            {
                connectionPoints.Add(childTransform);
                Debug.Log("Child name: " + childTransform.name);
            }

        }
    }
    private void GenerateUIImagesForConnectionPoints()
    {
        for (int i = 0; i < connectionPoints.Count; i++)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(connectionPoints[i].position);
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
            Vector2 canvasPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out canvasPosition))
            {
                //Image dropZoneImage = DropZone.GetComponent<Image>();
                GameObject generatedDropZone = Instantiate(DropZone, canvas.transform);
                ItemDrop itemDrop = generatedDropZone.GetComponent<ItemDrop>();
                itemDrop.setData(connectionPoints[i].gameObject, player);

                connectionPointImages.Add(generatedDropZone);
                RectTransform rectTransform = generatedDropZone.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = canvasPosition;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.position = startingPosition;
        foreach (GameObject connectionPointImage in connectionPointImages)
        {
            if(!connectionPointImage.GetComponent<ItemDrop>().hasBodypart)
                Destroy(connectionPointImage);
        }

        connectionPoints = new List<Transform>();
        connectionPointImages = new List<GameObject>();

    Debug.Log("Amount of Connetion points left: " + connectionPointImages.Count);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
