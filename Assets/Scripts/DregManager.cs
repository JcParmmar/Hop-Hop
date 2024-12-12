using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// this is script which manage the input from user
/// for touch and mouse input we are going to use IDragHandler, IBeginDragHandler, IEndDragHandler class methods can give a correct input point
/// </summary>
public class DragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 lastPosition = Vector2.zero;// last point user touch on screen
    public Vector2 dragDir = Vector2.zero; // direction where user touching
    
    //drag method will give point where user touching in between Drag start and end
    public void OnDrag(PointerEventData eventData)
    {
        //event data is data which provide where user is touching screen
        Vector2 direction = eventData.position - lastPosition;// we will find direction here, and we will normalize that value
        direction = direction.normalized;
        dragDir = direction;// this value we will move to public value

        lastPosition = Vector2.Lerp(lastPosition, eventData.position, Time.deltaTime / 2f);// this line will make currant touch point to last touch point
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //when user start touching screen save that point to find dircetion
        lastPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //restart values
        lastPosition = Vector2.zero;
        dragDir = Vector2.zero;
    }
}
