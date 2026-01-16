using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [Header("対象オブジェクト（このオブジェクトが入ったらシーン移行）")]
    public GameObject targetObject;

    [Header("遷移先のシーン名")]
    public string nextSceneName = "NextScene";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.name);

        if (other.gameObject == targetObject)
        {
            Debug.Log("対象オブジェクトが入った！ シーン移行します。");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
