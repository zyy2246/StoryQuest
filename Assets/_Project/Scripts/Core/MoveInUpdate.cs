using UnityEngine;

public class MoveInUpdate : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}