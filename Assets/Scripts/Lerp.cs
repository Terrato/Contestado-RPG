using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lerp : MonoBehaviour {

	float lerpTime = 10f;
	float currentLerpTime;
	
	float moveDistance = 30f;
	
	Vector3 startPos;
	Vector3 endPos;
	
	public int tipo;
	
	List<int> lista;
	
	string[,] matriz;
	
	float graus,grausTop,max;
	float speed;
	Camera camera;
	
	protected void Start() {
	//como declarar matriz em C#
		matriz = new string[5,5];
		matriz[4,4] = "hue";
		print (matriz[4,4]);
		startPos = transform.position;
		//left
		endPos = transform.position + transform.right * moveDistance;
		lista = new List<int>();
		graus=0;
		speed=2;
		max=70;
		grausTop=0;
		camera = GetComponentInChildren<Camera>();
		
		print(Application.persistentDataPath);
		//exemplo de save game salvando um score
		PlayerPrefs.SetFloat("Score", 10.0F);//grava o dado
		print (PlayerPrefs.GetFloat("Score"));//le o dado
		// "/data/data/appname/shared_prefs/appname.xml"
	}
	
	protected void Update() {
		//reset when we press spacebar
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentLerpTime = 0f;
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			graus+=90;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			graus-=90;
		}
		if(Input.GetKeyDown(KeyCode.W)){
			if(grausTop==0){
				grausTop+=max;
				camera.orthographicSize*=2; 
				}
		}
		if(Input.GetKeyDown(KeyCode.S)){
			if(grausTop==max){
				grausTop-=max;
				camera.orthographicSize/=2; 
				}
		}
		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
		//lerp!
		float perc = currentLerpTime / lerpTime;
		//pergunta o tipo
		if(tipo==-1){
			
			
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(grausTop,graus,0), Time.deltaTime*speed);
			
			if(transform.rotation.eulerAngles.x<=67f){
				camera.orthographic=false;
			}
			if(transform.rotation.eulerAngles.x>=68f){
				camera.orthographic=true;
				
			}
		}else{
			if(tipo==1)perc = Mathf.Sin(perc * Mathf.PI * 0.5f);//ease=out
			if(tipo==2)perc = 1f - Mathf.Cos(perc * Mathf.PI * 0.5f);//ease-in
			if(tipo==3)perc = perc*perc;//exponencial cubico
			if(tipo==4)perc = perc*perc*perc;//exponencial quadradico
			if(tipo==5)perc = perc*perc* (3f - 2f*perc);//Smoth step
			if(tipo==6)perc = perc*perc*perc * (perc * (6f*perc - 15f) + 10f);//Smother tep
			if(tipo==7){
				float theta = Time.timeSinceLevelLoad / lerpTime;
				float distance = moveDistance * Mathf.Sin(theta);
				transform.position = startPos + Vector3.right * distance;
			}else{
				transform.position = Vector3.Lerp(startPos, endPos, perc);
			}
		}
		
	}
}
