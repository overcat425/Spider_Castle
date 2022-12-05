using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest1 : MonoBehaviour
{
   int Nest_event= 0;
  
    int  randomNest () { 
   
    
    int random1 = Random.Range(0,2); //0부터 3미만 난수 생성
            return random1;
    }

void Update()    {
        
    

void OnCollisionEnter (Collision col) {

if(gameObject.CompareTag("player")) // 플레이어와 충돌시  랜덤 효과

 {
     Nest_event = randomNest ();

  //  if (Nest_event == 0) {
        /*플레이어 체력회복
        체력회복 함수 만들어서 접근하기
        or 직접 참조해서  수치접근

        PlayerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        PlayerStatus.CurrentHP= CurrentHP+10; // 예시 체력 10회복
        
        */
   // }
 //   else if(Nest_event == 1){
        /*단서 아이템 생성?*/

 //   }
    //else //체력감소 ?;
// }


Destroy (gameObject);

            }

    }
}

}

/*
void  OnCollisionExit (Collision col)
{

    Debug.Log("충돌");
    if(other.gameObject.tag == "player") // 플레이어와 충돌시 삭제
{
    Destroy (other.gameObject);
}

}
*/
//    }


