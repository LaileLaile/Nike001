using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class AudioItemConfig : MonoBehaviour  {

    public DataBase audiodata;
    public InputField starttime;
    public InputField endTime;
    public InputField durationTime;
    public InputField extentTime;
    public Text ShowAudioName;
    // Use this for initialization
    void Awake () {
        audiodata = new DataBase();
        
   // Initbase();
	}
	
	// Update is called once per frame
	void Update () {
        // updateevent();
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Openfile();
        //}
    }
    public void ChangestartVaule()
    {
        if (starttime.text!="" && endTime.text!=""&& extentTime.text !="")
        {
           endTime.text = (float.Parse(starttime.text) + float.Parse(extentTime.text)).ToString();
        }
      
    }

  
   
    public void ChangeConfig()
    {
        if (starttime.text != "")

        {
            audiodata.start = float.Parse(starttime.text) * 100;
            audiodata.end = float.Parse(endTime.text) * 100;
            audiodata.duration = float.Parse(durationTime.text);
            audiodata.extent = float.Parse(extentTime.text);

            Globaldata.Global.showItemManger.ChangeAudioItem(audiodata);
        }
    }


    public void ShowAudioConfig(float start,float end,float duration,string name)
    {
   
        starttime.text = (start/100).ToString ();
        endTime.text = (end /100).ToString();
        durationTime.text = duration.ToString();
        extentTime.text =(( end-start )/100).ToString();

        ShowAudioName.text = name;

       // ChangeConfig();
    }

    public void OpenFileDll(Text text)
    {
       // ofn.filter = "音频文件(*.WAV*.MP3*.OGG)\0*.WAV;*.MP3;*.OGG";
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "音频文件(*.WAV*.MP3*.OGG)|*.WAV;*.MP3;*.OGG";  //过滤文件类型
        dialog.InitialDirectory = "C:\\";  //定义打开的默认文件夹位置，可以在显示对话框之前设置好各种属性
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Debug.Log(dialog.FileName);
            StartCoroutine(WaitLoad(dialog.FileName, text));
        }
    }

    public void BackLoadAudio( string name)
    {
        StartCoroutine(BackWaitLoad(name));
    }

    public void Openfile(Text text)
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
            //加载图片到panle
            StartCoroutine(WaitLoad(ofn.file, text));
            Debug.Log("Selected file with full path: {0}" + ofn.file);
        }


    }

    IEnumerator WaitLoad(string fileName,Text text)
    {
        string[] str = fileName.Split('\\');
        string name = str[str.Length - 1];
        text.text = name;
        audiodata.audioname = name;
        audiodata.audioname = fileName;
        audiodata.audiopath = fileName;
        if (fileName.Substring(fileName.Length - 3).Contains("3"))
        {
        
            AudioClip clip = Tool.FromMp3Data(File.ReadAllBytes(fileName));

            Globaldata.Global.showItemManger.audioitem.SetClip(clip);

            yield return null;
        }
        else if (fileName.Substring(fileName.Length - 3).Contains("V") || fileName.Substring(fileName.Length - 3).Contains("v"))
        {
            AudioClip clip = Tool.FromWavData(File.ReadAllBytes(fileName));
            Globaldata.Global.showItemManger.audioitem.SetClip(clip);
            yield return null;
        }
        else
        {
            WWW w = new WWW(fileName);
            yield return w;
            AudioClip clip = w.GetAudioClip();
            Globaldata.Global.showItemManger.audioitem.SetClip(clip);
        }
       
        ChangeConfig();

        // plane.GetComponent<Renderer>().material.mainTexture = wwwTexture.texture;
    }


    IEnumerator BackWaitLoad(string fileName)
    {
        string[] str = fileName.Split('\\');
        string name = str[str.Length - 1];
        ShowAudioName.text = name;
        audiodata.audioname = name;

        if (fileName.Substring(fileName.Length - 3).Contains("3"))
        { 
        string    path = fileName .Replace('\\', '/');

            AudioClip clip = Tool.FromMp3Data(File.ReadAllBytes(fileName));

            Globaldata.Global.showItemManger.audioitem.SetClip(clip);

            yield return null;
        }
        else if (fileName.Substring(fileName.Length - 3).Contains("V") || fileName.Substring(fileName.Length - 3).Contains("v"))
        {
            AudioClip clip = Tool.FromWavData(File.ReadAllBytes(fileName));
            Globaldata.Global.showItemManger.audioitem.SetClip(clip);
            yield return null;
        }
        else
        {
            WWW w = new WWW(fileName);
            yield return w;
            AudioClip clip = w.GetAudioClip();
            Globaldata.Global.showItemManger.audioitem.SetClip(clip);
        }

        ChangeConfig();

        // plane.GetComponent<Renderer>().material.mainTexture = wwwTexture.texture;
    }
}


