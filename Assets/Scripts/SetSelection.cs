using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelection : MonoBehaviour
{
    public GameObject contents;
    public GameObject selection;
    public GameObject[] selections;
    // Start is called before the first frame update

    public void Initiate(int count)
    {
        contents.GetComponent<RectTransform>().offsetMin = new Vector2(contents.GetComponent<RectTransform>().offsetMin.x, 800f - 90f * count );
        selections = new GameObject[count];
        selections[0] = selection;
        for(int i = 1; i<count; i++)
        {
            selections[i] = GameObject.Instantiate(selection, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, contents.transform);
            selections[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(selections[i-1].GetComponent<RectTransform>().anchoredPosition.x, selections[i-1].GetComponent<RectTransform>().anchoredPosition.y - 90f);
        }
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }
}
