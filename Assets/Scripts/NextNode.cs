using UnityEngine;

public class NextNode : MonoBehaviour
{
    [SerializeField]
    private NextNode[] next;

    public NextNode GetNextNode()
    {
        return (next[Random.Range(0, next.Length)]);
    }
}
