using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Importa a biblioteca de UI (User Interface) para poder usar os texts
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Variável pública recebe speed da bolinha
    public float speed;

    private int totalPickups;
	
	public Transform portal;
	
	//String com o nome do próximo level
    public string nextLevel;
	
    
    //Váriavel pública que armazena a posição inicial do player
    //Precisa configurar em cada fase
    public Transform initialPlayerPos;

    //Variável pública que recebe o efeito sonoro do pickup
    public AudioClip pickupSound;
    public AudioClip fallsSound;

    //Variáveis do tipo Text para mostrar informações de texto
    public Text scoreText;
    //public Text winText;
	public Text timerText;

    //Váriavel int que armazena o score do player
    private int score = 0;
	
	//Variável float que armazena o tempo do jogo
	private float playTime = 0;

    //Variável rb para receber p componente físico da bolinha
    private Rigidbody rb;
	

    //Função start executa uma única vez no início
    void Start()
    {

        DefineScore();

        //Variável rb recebe o componente RigiBody da bolinha
        rb = GetComponent<Rigidbody>();
        //Coloca a string "Score: 0 " na tela
        scoreText.text = "Score: 0";

        ResetPlayerPosition();
    }

    //Função FixedUpdate é utilizada somente com simulação física
    //Roda o loop do jogo (várias e várias vezes por segundo)
    void FixedUpdate()
    {
        //As duas variáveis abaixo recebem a leitura dos eixos virtuais
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Cria um vetor com a direção para aplicação da força, baseado nos inputs
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Debug.Log(movement);
        
        //Aplica a força sobre vetor movement e multiplica pelo valor de speed
        rb.AddForce(movement * speed);
		
		playTime += Time.deltaTime;
		timerText.text = "Time: " + ((int)playTime).ToString();
    }
	
	private void Update()
	{
        //Tecla R para voltar ao spawnpoint
        if (Input.GetKeyDown(KeyCode.R))
        {
			RestartLevel();
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
			Application.Quit();
		}
		
		if (Input.GetKeyDown(KeyCode.T))
        {
			ResetPlayerPosition();
		}
		
		
	}

    //Função para detectar a colisão do player com o trigger do pickup
    private void OnTriggerEnter(Collider other)
    {
        //Verifica a tag do objeto com qual houve a colisão
        if (other.gameObject.CompareTag ("Pickup"))
        {
            //Desativa o objeto com qual o player colidiu (pickup)
            other.gameObject.SetActive(false);
            //Chama a função SetScore
            SetScore();
            AudioSource.PlayClipAtPoint(pickupSound, other.transform.position);
               
        }

        if (other.gameObject.CompareTag ("DeathSound"))
        {
            AudioSource.PlayClipAtPoint(fallsSound, other.transform.position);
        }

        if (other.gameObject.CompareTag("Death"))
        {
            ResetPlayerPosition();
        }
    }
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("PortalPanel"))
		{
			SceneManager.LoadScene(nextLevel);
		}		
	}

  
    //Função para aumentar o Score em 1
    void SetScore()
    {
        //Aumenta o Score em 1
        score++;
        //Atualiza o valor mostrado no scoreText (na tela)
        scoreText.text = "Score: " + score.ToString();
        
        
        if (score == totalPickups)
        {
            //winText.gameObject.SetActive(true);
			portal.gameObject.SetActive(true);
        }
    }

    void ResetPlayerPosition()
    {
        //Posição do player recebe posição do objeto InitialPlayerPos
        transform.position = initialPlayerPos.transform.position;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    //Função que define que define a quantidade de pickups que deve ser coletada por cada level
    void DefineScore()
    {
        Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Level0")
        {
            totalPickups = 3;
        }
        else if (scene.name == "Level1")
        {
            totalPickups = 11;
        }
		else if (scene.name == "Level2")
		{
			totalPickups = 11;
		}
		else if (scene.name == "Level3")
		{
			totalPickups = 17;
		}
		else if (scene.name == "Level4")
		{
			totalPickups = 12;
		}


    }
	
    //Função para resetar fase
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
	

}
