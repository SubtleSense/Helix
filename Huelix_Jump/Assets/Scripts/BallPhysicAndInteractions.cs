using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallPhysicAndInteractions : MonoBehaviour
{
    public Rigidbody rb;                //обращение к Rigidbody объекта.
    public Text ScoreText;              //компонент для вывод текста.
    public bool ColliderOn = false;     //элеиент компонента, отображает состояние коллайдера объекта. по дефолту коллайдер отключен.


    private int points = 0;             //кол-во столкновений объекта с триггером Transparent.

    public void OnTriggerEnter(Collider other) //взаимодействие объекта с триггерами.
    {
        if (other.tag == "Black")           //тригер на обычнную секцию платформы.
        {
            BlockReaction();
        }

        if (other.tag == "Fail")            //тригер на проигрышную секцию платформы.
        {
            DieReaction();
        }

        if (other.tag == "Transparent")     //тригер на "пустую" секцию платфонмы.
        {      
           
            other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer(); //ссылка на метод родителя триггера.
            ScoreUpdate(); 
        }

        if (other.tag == "Finish")          //тригер на жёлтую платфонму.(пока пуст)
        {
            Finishing();
        }
    }

    void BlockReaction()        //физика отскока объекта.
    {
        if (!ColliderOn)
        {
            ColliderOn = true;                                  //включил коллайдер.
            rb.velocity = Vector3.zero;                         //сбросил ускорение.
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);    //указал вектор силы, ее мощность и характер воздействия.

            StartCoroutine(ClliderOff());
        }
    }
    IEnumerator ClliderOff()    //отключил колайдер объекта через 0.1 секунды после BlockReaction.
    {
        yield return new WaitForSeconds(0.1f); // таймер.
        ColliderOn = false;                    // откл.
    }
    void DieReaction()          //перезапустил сцену Game.
    {
        SceneManager.LoadScene("game");
    }

    void ScoreUpdate()          //посчитал столуновения с триггером Transparent, записал в текстовое поле очки.
    {
        points++;
        ScoreText.text = points + "";
    }

    void Finishing()            //... пока пуст.
    {
        ScoreText.text = points + " total!";
    }
}