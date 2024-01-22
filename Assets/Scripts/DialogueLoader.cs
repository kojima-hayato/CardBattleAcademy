using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    [System.Serializable]
    public class DialogueData
    {
        public string[] lines;
    }

    private string[] LoadDialogueDataFromFile(string fileName)
    {
        TextAsset fileData = Resources.Load<TextAsset>(fileName);
        if (fileData == null)
        {
            Debug.LogError("�t�@�C����������Ȃ�: " + fileName);
            return new string[0];
        }

        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(fileData.text);
        return dialogueData.lines;
    }

    // ���̃��\�b�h�⃍�W�b�N�������ɒǉ�
}
