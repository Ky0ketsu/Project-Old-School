using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SortElements : MonoBehaviour
{
    List<Transform> children = new List<Transform>();
    [Header("SORT CHILDREN BY")]
    [SerializeField] bool XPosition;
    [SerializeField] bool YPosition, ZPosition; 

    void OnValidate()
    {
        if (XPosition)
            SortByX();
        else if (ZPosition)
            SortByZ();
        else if (YPosition)
            SortByY();

        XPosition = YPosition = ZPosition = false; // reset buttons
    }

    void SortByX()
    {
        GetChildren();
        children.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        SetSiblingIndex();
    }

    void SortByY()
    {
        GetChildren();
        children.Sort((a, b) => b.position.y.CompareTo(a.position.y));
        SetSiblingIndex();
    }

    void SortByZ()
    {
        GetChildren();
        children.Sort((a, b) => b.position.z.CompareTo(a.position.z));
        SetSiblingIndex();
    }



    void GetChildren()
    {
        for (int i = 0; i < transform.childCount; i++) children.Add(transform.GetChild(i));
    }

    void SetSiblingIndex()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i] != null) children[i].SetSiblingIndex(i);
        }
        children = new List<Transform>();
    }

    void Awake()
{
    if (Application.isPlaying)
    {
        Destroy(this);
    }
}

} // script end
