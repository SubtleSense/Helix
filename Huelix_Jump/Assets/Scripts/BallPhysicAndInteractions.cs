using UnityEngine;
using UnityEngine.UI;


public class BallPhysicAndInteractions : MonoBehaviour
{
    public int shield = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public int points = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public int score = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public Text ScoreText;              //компонент для вывод текста.
    void Speed()
    {
        
    }
    public void OnTriggerEnter(Collider other) //взаимодействие объекта с триггерами.
    {
        Time.timeScale = 1.8f;
        if (other.tag == "Black")           //тригер на обычнную секцию платформы.
        {
            Time.timeScale = 2f;

            if (shield >= 3)        //условие "активации брони" 
            {
                
                ScoreUpdate();           //ссылка на метод родителя триггера(начисли/обноаил очки).
                other.transform.parent.GetComponent<ForceAndDestroy>().BlockReaction();         //ссылка на метод родителя триггера("отскок").
                other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();     //ссылка на метод родителя триггера("включил броню").
                shield = 0;        //"отключил броню".

            }

            else
            {
                other.transform.parent.GetComponent<ForceAndDestroy>().BlockReaction();
                shield = 0;        //"отключил броню".
            }
        }

        if (other.tag == "Fail")            //тригер на проигрышную секцию платформы.
        {
            
            if (shield >= 3)       //условие "активации брони"
            {
                ScoreUpdate();           //ссылка на метод родителя триггера(начисли/обноаил очки).
                other.transform.parent.GetComponent<ForceAndDestroy>().BlockReaction();         //ссылка на метод родителя триггера("отскок").
                other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();     //ссылка на метод родителя триггера("включил броню").
                shield = 0;        //"отключил броню".

            }

            else
            {
                Time.timeScale = 0;
                shield = 0;        //"отключил броню".
            }

        }

        if (other.tag == "Transparent")     //тригер на "пустую" секцию платфонмы.
        {
            ScoreUpdate();           //ссылка на метод родителя триггера(начисли/обноаил очки).
            other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();     //ссылка на метод родителя триггера("включил броню").
            shield++;                                                                       //кол-во столкновений объекта с триггером Transparent(фактическое).
        }

        if (other.tag == "Finish")          //тригер на жёлтую платфонму.(пока пуст)
        {
            shield = 0;        //"отключил броню".
            other.transform.parent.GetComponent<ForceAndDestroy>().BlockReaction();         //ссылка на метод родителя триггера("отскок").
        }
    }

    public void ScoreUpdate()          //посчитал столуновения с триггером Transparent, записал в текстовое поле очки.
    { 
        points++;                       //кол-во столкновений объекта с триггером Transparent(фактическое).
        if ((shield >= 3) && (shield != 0))
        {
            score = score + shield;
        }
        ScoreText.text = points + score + "";   //вывел на эеран кол-во столкновений объекта с триггером Transparent(фактическое).
    }

}

