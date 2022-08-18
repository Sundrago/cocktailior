using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler

{
    private Vector3 defaultPosition;
    private Vector3 targetPosition;
    private float targetRotation;
    private Vector2 defaultMousePosition;
    private float velocity = 0.125f;

    float defaultSize;
    float targetSize;

    public bool destroySelf = false;

    public Button Xbtn, Obtn;
    public bool searchMode;
    public bool qMode;
    public bool ansMode;
    bool started = false;
    bool centerize = false;
    public GameObject closeBtn_ui, index_ui;

    public void Start()
    {
        if(started) return;
        started = true;
        destroySelf = false;
        defaultPosition = transform.position;
        targetPosition = defaultPosition;
        defaultSize = 1;
        targetSize = defaultSize;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (destroySelf) return;

        if(!qMode) Destroy(gameObject.GetComponent<Animator>());
        defaultMousePosition = eventData.position;
        centerize = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (destroySelf) return;

        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - defaultMousePosition;

        targetPosition = defaultPosition + new Vector3(diff.x, diff.y, 0);
        targetRotation = diff.y / Screen.height * 50f;

        if (transform.position.x < Screen.width / 2) targetRotation *= -1f;
        if (Mathf.Abs(transform.position.x - Screen.width / 2f) >= Screen.width / 4f)
        {
            targetSize = map(Mathf.Abs(transform.position.x - Screen.width / 2f), Screen.width / 4f, Screen.width / 2f, 1f, 4 / 5f);
        }

        transform.position = targetPosition;
        transform.localScale = new Vector3(targetSize, targetSize, 1);
        UpdateTransform();
    }

    public void Centerize(){
        targetPosition = defaultPosition;
        targetRotation = 0;
        transform.localScale = new Vector3(1, 1, 1);
        targetSize = defaultSize;

        transform.position = defaultPosition;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        destroySelf = false;
        centerize = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (destroySelf) return;

        OnDrag(eventData);
        //Back to Default Position
        if (transform.position.x  >= Screen.width / 5f * 2f && transform.position.x <= Screen.width / 5f * 3f)
        {
            Debug.Log("Centered");
            //Center
            targetPosition = defaultPosition;
            targetRotation = 0;
            targetSize = defaultSize;
        }
        else if (transform.position.x   < Screen.width / 5f * 2f)
        {
            Debug.Log("LEFTED");
            // Left
            Vector3 diff = (targetPosition - defaultPosition) * 2.5f;
            targetSize = 2 / 3f;
            targetPosition = transform.position + diff;
            destroySelf = true;
            GameObject mainCam = GameObject.Find("Main Camera");
            if(ansMode) mainCam.GetComponent<MainControl>().AnswerClosed();
            if(!searchMode) { 
                if(qMode) {
                    mainCam.GetComponent<MainControl>().QLeft();
                    if(centerize) return;
                }
                else mainCam.GetComponent<MainControl>().Left(); 
            }
            if (targetPosition.x > -Screen.width / 3f) targetPosition = new Vector3(-Screen.width / 3f, targetPosition.y, 0);
        }
        else
        {
            Debug.Log("RIGHTED");
            //Right
            Vector3 diff = (targetPosition - defaultPosition) * 2.5f;
            targetSize = 2 / 3f;
            targetPosition = transform.position + diff;
            destroySelf = true;
            GameObject mainCam = GameObject.Find("Main Camera");
            if(ansMode) mainCam.GetComponent<MainControl>().AnswerClosed();
            if (!searchMode)
            {
                if(qMode) {
                    mainCam.GetComponent<MainControl>().QRight();
                    if(centerize) return;
                }
                else mainCam.GetComponent<MainControl>().Right();
            }
            if (targetPosition.x < Screen.width + Screen.width / 3f) targetPosition = new Vector3(Screen.width + Screen.width/3f, targetPosition.y, 0);
        }

    }

    public void Lefted()
    {
        print(qMode);
        targetSize = 2 / 3f;
        targetPosition = new Vector3(-1000, -500, 0);
        targetRotation = 10f;
        destroySelf = true;
        GameObject mainCam = GameObject.Find("Main Camera");
        if (ansMode) mainCam.GetComponent<MainControl>().AnswerClosed();
        if (!searchMode)
        {
            if (qMode)
            {
                mainCam.GetComponent<MainControl>().QLeft();
                if (centerize) return;
            }
            else mainCam.GetComponent<MainControl>().Left();
        }
    }

    public void Righted()
    {
        targetSize = 2 / 3f;
        targetPosition = new Vector3(2000, -500, 0);
        targetRotation = -10f;
        destroySelf = true;
        GameObject mainCam = GameObject.Find("Main Camera");
        if(qMode) mainCam.GetComponent<MainControl>().QRight();
        else mainCam.GetComponent<MainControl>().Right();
    }

    public void Update()
    {
        float dist = Vector3.Distance(transform.position, targetPosition);
        if (dist <= 1.1f)
        {
            if (destroySelf)
            {
                if(qMode) gameObject.SetActive(false);
                else Destroy(gameObject);
            }
            else return;
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        }
        else
        {
            UpdateTransform();
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    void UpdateTransform()
    {
        Vector3 diff = targetPosition - transform.position;
        Vector3 target = transform.position + new Vector3(diff.x * velocity / (1 - Time.deltaTime), diff.y * velocity / (1 - Time.deltaTime), 0);
        transform.position = target;
        //Debug.Log("target : " + targetPosition + " current : " + transform.position);

        float currentAngle = transform.eulerAngles.z;
        if (currentAngle >= 200) currentAngle -= 360;

        float rDiff = targetRotation - currentAngle;
        float rTarget = currentAngle + rDiff * velocity / (1 - Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rTarget);

        //Debug.Log("target : " + targetRotation + " current : " + rTarget);
        //Debug.Log(transform.rotation);
        float sDiff = targetSize - transform.localScale.x;
        float sTarget = transform.localScale.x + sDiff * velocity / (1 - Time.deltaTime);
        transform.localScale = new Vector3(sTarget, sTarget, 1);
    }

    public void SetCloseBtn()
    {
        if (searchMode)
        {
            closeBtn_ui.SetActive(true);
            index_ui.SetActive(false);
        } else if (ansMode)
        {
            closeBtn_ui.SetActive(true);
            index_ui.SetActive(false);
        }
        else
        {
            closeBtn_ui.SetActive(false);
            index_ui.SetActive(true);
        }
    }
}