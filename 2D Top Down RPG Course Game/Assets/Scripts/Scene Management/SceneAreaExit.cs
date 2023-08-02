using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAreaExit : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
