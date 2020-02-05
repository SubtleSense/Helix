using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallPhysicAndInteractions : MonoBehaviour
{
    public Rigidbody rb;                //обращение к Rigidbody объекта.
    public Text ScoreText;              //компонент для вывод текста.
    public bool ColliderOff = false;    //элеиент компонента, отображает состояние коллайдера объекта. по дефолту коллайдер отключен.

    private int shield = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    private int points = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).

    public void OnTriggerEnter(Collider other) //взаимодействие объекта с триггерами.
    {
        if (other.tag == "Black")           //тригер на обычнную секцию платформы.
        {
            if (shield >= 3)        //условие "активации брони" 
            {
                BlockReaction();
                ScoreUpdate();
                other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();  //ссылка на метод родителя триггера.("включил броню")
                shield = 0;         //"отключил броню"

            }

            else
            {
                BlockReaction();
                shield = 0;
            }
        }

        if (other.tag == "Fail")            //тригер на проигрышную секцию платформы.
        {
            if (shield >= 3)       //условие "активации брони"
            {
                BlockReaction();
                ScoreUpdate();
                other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();  //ссылка на метод родителя триггера.("включил броню")
                shield = 0;        //"отключил броню"

            }

            else
            {
                DieReaction();
                shield = 0;
            }

        }

        if (other.tag == "Transparent")     //тригер на "пустую" секцию платфонмы.
        {
            other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();      //ссылка на метод родителя триггера.
            ScoreUpdate();
        }

        if (other.tag == "Finish")          //тригер на жёлтую платфонму.(пока пуст)
        {
            shield = 0;
            Finishing();
            BlockReaction();
        }
    }

    void BlockReaction()        //физика отскока объекта.
    {
        if (!ColliderOff)
        {
            ColliderOff = true;                                 //включил коллайдер.
            rb.velocity = Vector3.zero;                         //сбросил ускорение.
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);    //указал вектор силы, ее мощность и характер воздействия.

            StartCoroutine(ClliderOff());
        }
    }
    IEnumerator ClliderOff()    //отключил колайдер объекта через 0.1 секунды после BlockReaction.
    {
        yield return new WaitForSeconds(0.1f);  // таймер.
        ColliderOff = false;                    // откл.
    }
    void DieReaction()          //перезапустил сцену Game.
    {
        SceneManager.LoadScene("game");
    }

    void ScoreUpdate()          //посчитал столуновения с триггером Transparent, записал в текстовое поле очки.
    {
        points++;                       //кол-во столкновений объекта с триггером Transparent(фактическое).
        shield++;                       //кол-во столкновений объекта с триггером Transparent(фактическое).
        ScoreText.text = points + "";   //вывел на эеран кол-во столкновений объекта с триггером Transparent(фактическое).
    }

    void Finishing()            //... пока пуст.
    {
        ScoreText.text = points + " total!";
    }
}
