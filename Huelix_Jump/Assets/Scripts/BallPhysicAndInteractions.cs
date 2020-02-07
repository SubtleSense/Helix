using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BallPhysicAndInteractions : MonoBehaviour
{
    public int shield = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public int points = 0;             //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public int score = 0;              //кол-во столкновений объекта с триггером Transparent(начальное значение).
    public Text ScoreText;             //компонент для вывод текста.
    public GameObject FailPanel;
    private void Start()
    {
        FailPanel.SetActive(false);
        Time.timeScale = 1.8f;
    }
    public void OnTriggerEnter(Collider other) //взаимодействие объекта с триггерами.
    {
        if (other.tag == "Respawn") 
        {
            SceneManager.LoadScene("game");
        }
        //Time.timeScale = 1.8f;
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

        if (other.tag == "Fail")           //тригер на проигрышную секцию платформы.
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
                FailPanel.SetActive(true);
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

    public void ScoreUpdate()          //посчитал столkновения с триггером Transparent, записал в текстовое поле очки.
    {
        if(shield == 0)
        {
            points = points +5;         // +5 очков за одну платформу
        }
        if(shield != 0)
        {
            points = points + (5 * (shield + 1)); // + 10 очков за каждую следующую платформу, если мяч летит nonStop.
        }
        score = points; // переменная для хранения результатов.

        ScoreText.text =  score + "";   //вывел на эеран результат расчета формулы.
    }

}

