using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    // Camera Setup..
    // worldPoint 
    // Viewport Point
    // Screen Point

    // same in inspector.
    [SerializeField] List<Button> ListBtns;

    private void Awake()
    {
        initBtns();
    }

    private void initBtns() {
        ListBtns[0].onClick.AddListener(onStart);
        ListBtns[1].onClick.AddListener(() => { });
        ListBtns[2].onClick.AddListener(onExit);
    }

    private void onStart() {
        // SceneManager.LoadSceneAsync((int)SceneNums.MainScene); 
    }

    private void onRank() { 
    
    }

    private void onExit() {
        //preprocessing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
#endif
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
