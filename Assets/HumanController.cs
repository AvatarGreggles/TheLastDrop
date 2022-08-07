using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanController : MonoBehaviour
{

    [SerializeField] Dialog dialog;

    [SerializeField] Dialog subliminalMessage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !DialogManager.Instance.IsShowing)
        {
            DialogManager.Instance.dialogBox.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            DialogManager.Instance.lettersPerSecond = 30;
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(SubliminalDialog());
        }
    }

    public IEnumerator SubliminalDialog()
    {
        yield return new WaitUntil(() => DialogManager.Instance.IsTyping == false);
        DialogManager.Instance.lettersPerSecond = 100;
        DialogManager.Instance.dialogBox.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        yield return DialogManager.Instance.ShowDialog(subliminalMessage);

        yield return new WaitForSeconds(1f);
        DialogManager.Instance.CloseDialog();
    }

}
