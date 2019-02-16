using UnityEngine;

public class NextNode : MonoBehaviour
{
    [SerializeField]
    private NextNode[] next;

    public NextNode GetNextNode()
    {
        if (next.Length == 0)
            return (this);
        return (next[Random.Range(0, next.Length)]);
    }
}
