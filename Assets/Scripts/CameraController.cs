using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    //Variável do tipo GameObject chamada 'Player'
    public GameObject player;

    //Variável 'offset' do tipo Vector3
    private Vector3 offset;

    void Start()
    {
        //offset recebe posição da câmera - posição do player
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Posição da câmera recebe posição do player + offset
        transform.position = player.transform.position + offset;
    }
}
