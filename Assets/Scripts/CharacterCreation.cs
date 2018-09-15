   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreation : MonoBehaviour {
    public GameObject[] characterprefbs;
    public UIInput nameInput;
    private GameObject[] characterCreations;
    private int length = 0;
    private int selectIndex = 0;

    // Use this for initialization
    void Start () {
        length = characterprefbs.Length;
        characterCreations = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            characterCreations[i]=GameObject.Instantiate(characterprefbs[i],this.transform.position,this.transform.rotation);
            characterCreations[i].transform.Rotate(new Vector3(0, 180, 0));
        }
        showCharacter();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void showCharacter()
    {
        characterCreations[selectIndex].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if (i != selectIndex)
            {
                characterCreations[i].SetActive(false);
            }
        }
    }

    public void OnNextButton()
    {
        selectIndex++;
        selectIndex %= length;
        showCharacter();
    }

    public void OnPrevButton()
    {
        selectIndex--;
        if (selectIndex < 0)
            selectIndex = length - 1;
        showCharacter();

    }
    public void OnOK()
    {
        PlayerPrefs.SetInt("selectindex",selectIndex);
        PlayerPrefs.SetString("nameInput",nameInput.value);
    }
}
