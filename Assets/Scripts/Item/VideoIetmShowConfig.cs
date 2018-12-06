using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class VideoIetmShowConfig : MonoBehaviour {
    public DataBase videoData;
    public InputField starttime;
    public InputField endTime;
    public InputField durationTime;
    public InputField extentTime;
    public Text ShowVideoName;

    public RawImage playmovie;
    public MovieTexture movieTexture;
    public AudioSource audio;
    // Use this for initialization
    void Start () {
        videoData = new DataBase();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangestartVaule()
    {
        if (starttime.text != "" && endTime.text != "" && extentTime.text != "")
        {
            endTime.text = (float.Parse(starttime.text) + float.Parse(extentTime.text)).ToString();
        }

    }
    public void OpenFileDll(Text text)
    {
        // ofn.filter = "音频文件(*.WAV*.MP3*.OGG)\0*.WAV;*.MP3;*.OGG";
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "视频文件(*.mov;*.mpg;*.mpeg;*.mp4;*avi;*asf)|*.mov;*.mpg;*.mpeg;*.mp4;*avi;*asf";  //过滤文件类型
        //dialog.InitialDirectory = "C:\\";  //定义打开的默认文件夹位置，可以在显示对话框之前设置好各种属性
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Debug.Log(dialog.FileName);
            StartCoroutine(WaitLoad(dialog.FileName, text));
            //StartCoroutine(DownLoadMovie(dialog.FileName));
        }
    }




    private IEnumerator DownLoadMovie(string url )
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + url);
        WWW www = new WWW(files[0]);
        Debug.Log(Time.time);
        movieTexture = www.GetMovieTexture();
        audio.clip = movieTexture.audioClip;

        playmovie.texture = movieTexture;
        movieTexture.Play();
        audio.Play();
        //renderer.material.mainTexture = movieTexture;
        movieTexture.loop = true;
        yield return www;


    }



    public void ChangeConfig()
    {
        videoData.start = float.Parse(starttime.text) * 100;
        videoData.end = float.Parse(endTime.text) * 100;
        videoData.duration = float.Parse(durationTime.text);
        videoData.extent = float.Parse(extentTime.text);

        Globaldata.Global.showItemManger.ChangeVideoItem(videoData);
    }


    public void ShowVideoConfig(float start, float end, float duration, string name)
    {

        starttime.text = (start / 100).ToString();
        endTime.text = (end / 100).ToString();
        durationTime.text = duration.ToString();

   //     Debug.LogError(duration);
        extentTime.text = ((end - start) / 100).ToString();

        ShowVideoName.text = name;

        // ChangeConfig();
    }

    public void Openfile(Text text)
    {



        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);

        //ofn.filter = "All Files\0*.*\0\0";
        //.mov, .mpg, .mpeg, .mp4, .avi, .asf
        ofn.filter = "视频文件(*.mov *.mpg *.mpeg *.mp4 *avi *asf)";
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

    IEnumerator WaitLoad(string fileName, Text text)
    {
        string[] str = fileName.Split('\\');
        string name = str[str.Length - 1];
        text.text = name;
        videoData.videoPath = fileName;
        videoData.videoName = name;
        Globaldata.Global.videomanager.GetTime(fileName);
        Globaldata.Global.showItemManger.setPathAndName(videoData);
        yield return null;

       // ChangeConfig();

        // plane.GetComponent<Renderer>().material.mainTexture = wwwTexture.texture;
    }


    public void ChangeDuration(float time)
    {
        if (videoData!=null )
        {


            Globaldata.Global.showItemManger.videoItem.setTime(time);
            ChangeConfig();
            //starttime.text = (start / 100).ToString();
            //endTime.text = (end / 100).ToString();
            //durationTime.text = duration.ToString();
            //extentTime.text = ((end - start) / 100).ToString();


            //  Globaldata.Global.showItemManger.ChangeVideoItem(videoData);
            //      Globaldata.Global.showItemManger.ShowVideo(videoData);
            //ChangeConfig();
        }
    }



}
