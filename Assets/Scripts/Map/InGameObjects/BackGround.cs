using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
    [SerializeField]
    private SpriteFader[] myLights;
    static public BackGround instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitBackGroundMusic();
       
    }

    public void LightOn()
    {
        StartCoroutine(LightOnRoutine());
        transform.Find("Light").GetComponent<SoundEffectCtrl>().Play();
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    IEnumerator LightOnRoutine()
    {
        for (int i = 0; i < myLights.Length; i++)
        {
            myLights[i].Opaque(2);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void InitBackGroundMusic()
    {
        MusicCtrl backGroundMusic = transform.Find("BackgroundMusic").GetComponent<MusicCtrl>();
        backGroundMusic.Play();
        backGroundMusic.Opaque(5);
    }
}
