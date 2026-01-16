using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("判定対象オブジェクト")]
    public GameObject targetObject;

    [Header("判定するY座標")]
    public float targetY = 0f;

    [Header("移動させるオブジェクトと移動先")]
    public GameObject[] objectsToMove;
    public Vector3[] positionsToMove;

    void Update()
    {
        if (targetObject.transform.position.y <= targetY)
        {
            if (objectsToMove.Length != positionsToMove.Length)
            {
                Debug.LogError("objectsToMove と positionsToMove の配列の長さが一致していません");
                return;
            }

            for (int i = 0; i < objectsToMove.Length; i++)
            {
                if (objectsToMove[i] != null)
                {
                    // 慣性を消す（Rigidbodyがある場合のみ）
                    Rigidbody rb = objectsToMove[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }

                    // 瞬間移動
                    objectsToMove[i].transform.position = positionsToMove[i];
                }
            }
        }
    }
}
