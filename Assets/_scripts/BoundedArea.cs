using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedArea : MonoBehaviour
{
    private float widthMagnitude;
    private float deepMagnitude;

    //private Vector2 centerPoint;

    [SerializeField]
    private BoxCollider m_collider;

    //public Vector2 CenterPoint { get => centerPoint; private set { centerPoint = value; } }
    public float GetUpperBound()
    {
        return m_collider.center.z + deepMagnitude / 2;
    }
    public float GetLowerBound()
    {
        return m_collider.center.z - deepMagnitude / 2;
    }
    public float GetLeftBound()
    {
        return m_collider.center.x - widthMagnitude / 2;
    }
    public float GetRightBound()
    {
        return m_collider.center.x + widthMagnitude / 2;
    }

    public void CheckBounds(Transform comparedTransform)
    {

        // revisar
        if (comparedTransform.position.x > GetLeftBound())
            comparedTransform.position = new Vector3(GetLeftBound(), comparedTransform.position.y, comparedTransform.position.z);
        if (comparedTransform.position.x < GetLeftBound())
            comparedTransform.position = new Vector3(GetRightBound(), comparedTransform.position.y, comparedTransform.position.z);
        if (comparedTransform.position.z < GetLowerBound())
            comparedTransform.position = new Vector3(comparedTransform.position.x, comparedTransform.position.y, GetUpperBound());
        if (comparedTransform.position.z > GetUpperBound())
            comparedTransform.position = new Vector3(comparedTransform.position.x, comparedTransform.position.y, GetLowerBound());

    }

    private void OnAwake()
    {
        if (!m_collider)
            m_collider = GetComponent<BoxCollider>();

        this.deepMagnitude = (m_collider.bounds.max.z - m_collider.center.z) * 2;
        this.widthMagnitude = (m_collider.bounds.max.x - m_collider.center.x) * 2;
        Debug.Log(string.Format("deep {0} , width {1}", deepMagnitude, widthMagnitude));
    }

    // Start is called before the first frame update
    void Awake()
    {
        OnAwake();
    }
}
