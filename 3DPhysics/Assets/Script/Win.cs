using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Material targetMaterial; // ������ϣ�����С�Bowl�����󶼱�ɵĲ���
    public int sceneIndexToLoad; // ��ϣ�����صĳ���������

    private void Update()
    {
        if (AllBowlsHaveTargetMaterial())
        {
            // ������С�Bowl���Ĳ��ʶ��Ѹ���ΪĿ����ʣ������ָ�������ĳ���
            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }

    private bool AllBowlsHaveTargetMaterial()
    {
        // �ҵ����������б�ǩΪ��Bowl���Ķ���
        GameObject[] bowls = GameObject.FindGameObjectsWithTag("Bowl");

        // ����ÿһ����Bowl������
        foreach (GameObject bowl in bowls)
        {
            // ����ÿһ�����󣬼�����Ĳ����Ƿ���Ŀ�����ƥ��
            if (bowl.GetComponent<Renderer>().sharedMaterial != targetMaterial)
            {
                // ���������һ������Ĳ��ʲ�ƥ��Ŀ����ʣ��򷵻� false
                return false;
            }
        }

        // ���ѭ��������û�з��� false����˵�����С�Bowl������Ĳ��ʶ���Ŀ�����ƥ�䣬���� true
        return true;
        //11
    }
}
