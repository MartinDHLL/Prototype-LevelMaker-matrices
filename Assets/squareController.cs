using UnityEngine;
using System.Collections;

public class squareController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip placeSound;
    Material squareMaterial;
    private int _index;
    public int index {
        get => _index;
        set => _index = value;
    }

    private bool _clickState, soundFlag, _busyDefault;
    public bool isBusyByDefault {
        get => _busyDefault;
        set => _busyDefault = value;
    }
    public bool isClicked {
        get => _clickState;
        set => _clickState = value;
    }
    private bool _busy;
    public bool isBusy {
        get => _busy;
        set => _busy = value;
    }

    private bool mouseOver;

    private void Start() {
       squareMaterial = this.GetComponent<Renderer>().material;
       squareMaterial.color = _busy ? Color.black : Color.white ;
       audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        squareMaterial.color = 
        _busy && !mouseOver ? Color.black : 
        _busy && mouseOver ? Color.black : 
        !_busy && mouseOver ? squareMaterial.color = Color.yellow : 
        squareMaterial.color = Color.white ;
    }

    private void OnMouseOver() {
        // change material color, mouseOver = true
        mouseOver = true;
    }

    private void OnMouseExit() {
        mouseOver = false;
    }

    private void OnMouseDown() {
        isClicked = true;
        if(!_busy && !_busyDefault)
        {
            soundFlag = true;
            playSoundOnce(placeSound);
        }
    }

    void playSoundOnce(AudioClip sound)
    {
        if(soundFlag)
        {
            audioSource.PlayOneShot(sound);
            soundFlag = false;
        }
    }
    
    
}
