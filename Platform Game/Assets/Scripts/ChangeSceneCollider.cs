using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        SceneManager.LoadScene(4);
    }
}
