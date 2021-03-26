using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints; //array recebe o nome de waypoints, que são os pontos que o cilindro andará
    int currentWP = 0; //valor atribuido inicialmente para o início do meu array
    float speed = 1.0f; //velocidade que o cilindro terá
    float accuracy = 1.0f; //valor atribuido para proximação 
    float rotSpeed = 0.4f; //valor pra velocidade da rotação pra um novo ponto 
    // Start is called before the first frame update20:06 25/03/2021
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //acha o cilindro no mapa e calcula rota pro ponto a seguir
    }

    // Update is called once per frame
    void LateUpdate() //movimento do cilindro sendo atualizado quadro por quadro
    {
        if (waypoints.Length == 0) return; //nessa condição, é feita a análise do tamanho do array criado, sendo necessário o retorno zerado, pra poder startar o movimento
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, //Instancia um objeto como ele Vector3, dentro do waypoints pega o valor do current s posições de X e Z sem o Y
        this.transform.position.y, //Movimentação feita num plano, não necessita alterar o valor de Y 
        waypoints[currentWP].transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position; //Direção dos pontos definidos sendo instanciada pro cilindro
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, //Quaternion.Slerp ajuda o cilindro a rotacionar de maneira mais natural e amena
 Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed); //Ao chegar no ponto atual, o cilindro faz a rotação pra ir pro próximo ponto


        if (direction.magnitude < accuracy) // condição: se direção.magnitude for menor que o valor da aproxição
        {
            currentWP++; // soma waypoint para ir pro próximo ponto
            if (currentWP >= waypoints.Length) // se o valor inicial for maior ou igual aos pontos
            {
                currentWP = 0; //current retorna zero
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime); //Time.deltaTime usado para atualização quadro após quadro 
    }
}

