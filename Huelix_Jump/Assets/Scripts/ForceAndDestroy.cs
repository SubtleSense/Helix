using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;




public class ForceAndDestroy : BallPhysicAndInteractions
{

    public GameObject[] items;          //массив сеуций
    public Rigidbody rb;                //обращение к Rigidbody объекта.


    public bool ColliderOff = false;    //элеиент компонента, отображает состояние коллайдера объекта. по дефолту коллайдер отключен.
    void Start()
    {
        
    }

    public void PlatformDestroyer()     //метод ломающий плаьформы
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.AddComponent<Rigidbody>();                                                                                  //добавил компонент Rigidbody.
            items[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));      //крутящий момент для Rigidbody.
            items[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(5, 10), ForceMode.Impulse);                    //указал вектор силы, ее мощность и характер воздействия.
            items[i].gameObject.GetComponent<Collider>().enabled = false;                                                                   //отключил коллайдер.
            StartCoroutine(Destroyer());
        }
    }
    public void BlockReaction()        //физика отскока объекта.
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
    public void Restart()          //перезапустил сцену Game.
    {
        Debug.Log("Restart");
        FailPanel.SetActive(false);
        Time.timeScale = 1.8f;
        SceneManager.LoadScene("game");
        
    }

    public void Finishing()            //... пока пуст.
    {

    }
    IEnumerator Destroyer()             //уничтожил текущий платформу через секунду после срабатывания PlatformDestroyer().
    {
        yield return new WaitForSeconds(1f);   //таймер
        Destroy(this.gameObject);              //дестроер
    }
}