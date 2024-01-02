using UnityEngine;

/// <summary>
/// A struct representing an interval with a bottom and upper bound.
/// </summary>
/// <typeparam name="T">The type of values within the interval, must implement IComparable.</typeparam>
[System.Serializable]
public struct Interval<T> where T : struct, System.IComparable<T>
{
    /// <summary>
    /// Gets the bottom bound of the interval.
    /// </summary>
    public T BottomBound => _bottomBound;
    [SerializeField] private T _bottomBound;

    /// <summary>
    /// Gets the upper bound of the interval.
    /// </summary>
    public T UpperBound => _upperBound;
    [SerializeField] private T _upperBound;

    /// <summary>
    /// Gets a random value within the interval.
    /// </summary>
    /// <returns>A random value within the interval.</returns>
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
