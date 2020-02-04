using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallPhysicAndInteractions : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    public Text ScoreText; // Компонент для вывод текста
    public bool check = false;


    private int points = 0;

   public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Black") //тригер на обычный сектор платформы
        {
            BlockReaction();
        }

        if (other.tag == "Fail") //тригер на проигрышный сектор платформы
        {
            DieReaction();
        }

        if (other.tag == "Transparent") //тригер на выигрышный сектор платформы
        {      
           
            other.transform.parent.GetComponent<ForceAndDestroy>().PlatformDestroyer();
            ScoreUpdate(); 
        }

        if (other.tag == "Finish")
        {
            Finishing();
        }
    }

    void BlockReaction()
    {
        if (!check)
        {
            check = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            transform.Translate((Vector3.up.normalized * force));

            StartCoroutine(bla());
        }
    }

    void DieReaction()
    {
        SceneManager.LoadScene("game");
    }

    //Обновляем очки
    void ScoreUpdate()
    {
        if (!check)
        {
            check = true;
            points++;
            ScoreText.text = points + "";

            StartCoroutine(bla());
        }

    }

    void Finishing()
    {
        ScoreText.text = points + " total!";
    }

    IEnumerator bla()
    {
        yield return new WaitForSeconds(0.1f);
        check = false;
    }
}