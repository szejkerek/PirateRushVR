using UnityEngine;

[System.Serializable]
public struct Interval<T> where T : struct, System.IComparable<T>
{
    public T BottomBound => _bottomBound;
    [SerializeField] private T _bottomBound;

    public T UpperBound => _upperBound;
    [SerializeField] private T _upperBound;

    public T GetValueBetween()
    {
        if (BottomBound is int && UpperBound is int)
        {
            return (T)(object)Random.Range(Mathf.Min((int)(object)_bottomBound, (int)(object)_upperBound), Mathf.Max((int)(object)_bottomBound, (int)(object)_upperBound));
        }
        else if (BottomBound is float && UpperBound is float)
        {
            return (T)(object)Random.Range(Mathf.Min((float)(object)_bottomBound, (float)(object)_upperBound), Mathf.Max((float)(object)_bottomBound, (float)(object)_upperBound));
        }
        else
        {
            throw new System.NotSupportedException("Unsupported type");
        }
    }
}
