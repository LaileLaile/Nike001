using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class Playaudio : MonoBehaviour
{
    public AudioSource audio;
    public Slider slider;
    string path;
    private List<AudioClip> audioClips = new List<AudioClip>();
    private List<string> names=new List<string> ();
    private int index = 0;
    // Use this for initialization
    void Start()
    {
    
    }
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("volume"))
        {
            audio.volume = PlayerPrefs.GetFloat("volume");
        }
        slider.value = audio.volume;
        slider.onValueChanged.AddListener(Change);
        path = UnityEngine.Application.streamingAssetsPath + "/Audio/";
        DirectoryInfo direction = new DirectoryInfo(path);


        FileInfo[] deletefiles = direction.GetFiles("*.mp3", SearchOption.AllDirectories);
        for (int i = 0; i < deletefiles.Length; i++)
        {
            names.Add(deletefiles[i].ToString());
        }

        StartCoroutine(WaitLoad(names.ToArray()));
    }
    public void Change(float volume)
    {
        audio.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode .M))
        //{
        //    OpenFileDll();
        //}

    }

    IEnumerator WaitLoad(string[] fileName)
    {
        for (int i = 0; i < fileName.Length; i++)
        {
            string[] str = fileName[i].Split('\\');


            if (fileName[i].Substring(fileName.Length - 3).Contains("3"))
            {

                AudioClip clip = Tool.FromMp3Data(File.ReadAllBytes(fileName[i]));

                //audio.clip = clip;
                audioClips.Add(clip);
                //audio.Play();
                //string path = fileName .Replace('\\', '/'); 
                yield return null;
            }
            else if (fileName[i].Substring(fileName.Length - 3).Contains("V") || fileName[i].Substring(fileName.Length - 3).Contains("v"))
            {
                AudioClip clip = Tool.FromWavData(File.ReadAllBytes(fileName[i]));
                audioClips.Add(clip);
                yield return null;
            }
            else
            {
                WWW w = new WWW(fileName[i]);
                yield return w;
                AudioClip clip = w.GetAudioClip();
                audioClips.Add(clip);
            }

        }

        PlayAudio();

        //PlayerPrefs.SetString("path", fileName);

    }




    public void PlayAudio()
    {
        audio.clip = audioClips[index];
        Debug.Log(audio.clip.name + "    " + audio.clip.length + "   " + index);
        audio.Play();
        index++;
        if (index >= audioClips.Count)
        {
            index = 0;
        }
       
        Invoke("PlayAudio", audio.clip .length +1);
    }
    public void Openfile()
    {



        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);

        //ofn.filter = "All Files\0*.*\0\0";
        ofn.filter = "音频文件(*.WAV*.MP3*.OGG)\0*.WAV;*.MP3;*.OGG";
        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;
        string path = UnityEngine.Application.streamingAssetsPath;
        path = path.Replace('/', '\\');
        //默认路径  
        ofn.initialDir = path;
        //ofn.initialDir = "D:\\MyProject\\UnityOpenCV\\Assets\\StreamingAssets";  
        ofn.title = "Open Project";

        ofn.defExt = "";//显示文件的类型  
                        //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

        if (WindowDll.GetOpenFileName1(ofn))
        {

            Debug.Log("Selected file with full path: {0}" + ofn.file);
        }


    }



    //public void OpenFileDll()
    //{
    //    // ofn.filter = "音频文件(*.WAV*.MP3*.OGG)\0*.WAV;*.MP3;*.OGG";
    //    OpenFileDialog dialog = new OpenFileDialog();
    //    dialog.Filter = "音频文件(*.WAV*.MP3*.OGG)|*.WAV;*.MP3;*.OGG";  //过滤文件类型
    //    dialog.InitialDirectory = "C:\\";  //定义打开的默认文件夹位置，可以在显示对话框之前设置好各种属性
    //    if (dialog.ShowDialog() == DialogResult.OK)
    //    {
    //        // Debug.Log(dialog.FileName);
    //        StartCoroutine(WaitLoad(dialog.FileName));
    //    }
    //}
}
