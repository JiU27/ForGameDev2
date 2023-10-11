using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Material originalMaterial; // ���ǡ�Bowl����������Ĳ���

    private void Update()
    {
        if (AllBowlsMaterialChanged())
        {
            // ������С�Bowl���Ĳ��ʶ��Ѹ��ģ������¼��س���
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool AllBowlsMaterialChanged()
    {
        // �ҵ����������б�ǩΪ��Bowl���Ķ���
        GameObject[] bowls = GameObject.FindGameObjectsWithTag("Bowl");

        // ����ÿһ����Bowl������
        foreach (GameObject bowl in bowls)
        {
            // ����ÿһ�����󣬼�����Ĳ����Ƿ��Ѹ���
            if (bowl.GetComponent<Renderer>().sharedMaterial == originalMaterial)
            {
                // ���������һ������Ĳ���δ���ģ��򷵻� false
                return false;
            }
        }

        // ���ѭ��������û�з��� false����˵�����С�Bowl������Ĳ��ʶ��Ѹ��ģ����� true
        return true;
    }
}
