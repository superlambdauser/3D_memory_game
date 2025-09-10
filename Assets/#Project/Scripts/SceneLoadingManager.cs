using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class SceneLoadingManager : MonoBehaviour
{
    private string scene;

    [SerializeField] private float loadingDelay;


    public void Initialize(string scene, float loadingDelay)
    {
        this.scene = scene;
        this.loadingDelay = loadingDelay;
    }

    // Update is called once per frame
    void GetScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void Load()
    {
        Invoke("GetScene", loadingDelay);
    }

}
