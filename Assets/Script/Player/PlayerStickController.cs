using UnityEngine;

public class PlayerStickController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveRange = 10f; // 移動できる範囲
    [SerializeField] private float diagonalMoveInfluence = 0.2f; // 傾きによる横移動の影響度(0-1)

    [Header("傾き設定")]
    [SerializeField] private float maxTiltAngle = 30f; // 最大傾き角度
    [SerializeField] private float tiltSpeed = 20f; // 傾く速度

    private float currentRotationX = 0f; // 現在の回転角度

    private void Update()
    {
        // W/S/A/Dキー入力
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        else if (Input.GetKey(KeyCode.S)) verticalInput = -1f;

        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
        else if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;

        bool isXMoveMode = Input.GetKey(KeyCode.E); // EキーでX軸移動モード
        bool isZMoveMode = Input.GetKey(KeyCode.Q); // QキーでZ軸移動モード

        if (isXMoveMode)
        {
            // Eキー押しながら W/S で X軸移動
            // Wキーで後ろ（-X）、Sキーで前（+X）
            transform.position += new Vector3(-verticalInput * moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (isZMoveMode)
        {
            // Qキー押しながら A/D で Z軸移動
            // Aキーで後ろ（-Z）、Dキーで前（+Z）
            transform.position += new Vector3(0, 0, horizontalInput * moveSpeed * Time.deltaTime);
        }
        else
        {
            // 通常移動(Y軸) + 傾きに応じたZ軸斜め移動
            float depthMovement = (currentRotationX / maxTiltAngle) * diagonalMoveInfluence * verticalInput;
            Vector3 movement = new Vector3(
                0,
                verticalInput * moveSpeed * Time.deltaTime,
                -depthMovement * moveSpeed * Time.deltaTime
            );
            transform.position += movement;

            // A/DキーでX軸回転
            if (Input.GetKey(KeyCode.D))
            {
                currentRotationX -= tiltSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                currentRotationX += tiltSpeed * Time.deltaTime;
            }
            else
            {
                // キーを離したら徐々に元に戻る
                currentRotationX = Mathf.Lerp(currentRotationX, 0, tiltSpeed * 0.5f * Time.deltaTime);
            }
        }

        // 傾き角度を制限
        currentRotationX = Mathf.Clamp(currentRotationX, -maxTiltAngle, maxTiltAngle);

        // X軸回転を適用
        transform.rotation = Quaternion.Euler(currentRotationX, 0, 0);

        // 移動範囲を制限
        float clampedX = Mathf.Clamp(transform.position.x, -moveRange, moveRange);
        float clampedY = Mathf.Clamp(transform.position.y, -moveRange, moveRange);
        float clampedZ = Mathf.Clamp(transform.position.z, -moveRange, moveRange);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}
