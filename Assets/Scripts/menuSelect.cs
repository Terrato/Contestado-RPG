using UnityEngine;
using System.Collections;

public class menuSelect : MonoBehaviour {
	public int total;
	public int current;
	public int inScreen;
	public GameObject panel;
	public int countInScreen;
	// Use this for initialization
	void Start () {
		total = 7;//numero total de itens na minha lista
		inScreen=4;//numero de itens que aparecem na tela
		current=0;//posicao do "cursor" pra saber qual item sera selecionado
		countInScreen=0;//posicao do "cursor" na tela, no meu exemplo vai de 0 a 3 (4 posiçoes)
	}
	
	// Update is called once per frame
	void Update () {
		print (countInScreen);
		if(Input.GetKeyDown(KeyCode.S)){
			//is going down
			if(current<total-1){//nao deixa ele vazar da ultima posicao
				current++;
				
				if(countInScreen>=3){//equivalente a countInScreen>=inScreen-1
					//mexe so o panel, o "cursor" fica parado
					panel.transform.Translate(0,1.75f,0);
				}else{
					//mexe so "cursor", o panel fica parado
					transform.Translate(0,-1.75f,0);
					countInScreen++;
				}
			}
		}
		
		if(Input.GetKeyDown(KeyCode.W)){
			//is going up
			if(current>0){//nao deixa ele vazar da primeira posicao
				current--;
				
				if(countInScreen>0){//ja que eh a primeira posicao pode ser um 0, nao precisa de calculo
					//mexe so "cursor", o panel fica parado
					transform.Translate(0,1.75f,0);
					countInScreen--;
				}else{
					//mexe so o panel, o "cursor" fica parado
					panel.transform.Translate(0,-1.75f,0);
				}
			}
		}
	
	
	
	}
}
