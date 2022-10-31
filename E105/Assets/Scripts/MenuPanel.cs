using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    // public LinkedList<GameObject> eventList = new LinkedList<GameObject>();
    public GameObject[] eventList;
    public GameObject questButton;
    private int cnt = 0;
    private Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));
    public void setQuest()
    {
        Vector3 posit = new Vector3(cnt * 30 + 10, -10, 0);
        cnt++;

        GameObject[] newList = new GameObject[cnt];
        for (int i = 0; i < cnt - 1; i++)
        {
            newList[i] = eventList[i];
        }

        GameObject qb = Instantiate(questButton, this.transform);

        RectTransform qbRect = qb.GetComponent<RectTransform>();
        qbRect.SetLocalPositionAndRotation(posit, rotateZero);

        newList[cnt - 1] = qb;
        qb.GetComponent<QuestButton>().idx = cnt;

        eventList = new GameObject[cnt];

        newList.CopyTo(eventList, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
        setQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void deleteObject(GameObject child)
    {
        int removeIdx = child.GetComponent<QuestButton>().idx - 1;

        Destroy(child);

        Debug.Log("==================================");
        for (int i = removeIdx; i < cnt - 1; i++)
        {
            Vector3 posit = new Vector3(i * 30 + 10, -10, 0);

            GameObject qb = eventList[i + 1];

            RectTransform qbRect = qb.GetComponent<RectTransform>();
            qbRect.SetLocalPositionAndRotation(posit, rotateZero);

            qb.GetComponent<QuestButton>().idx = i + 1;

            eventList[i] = qb;
        }
        cnt--;
    }
}
