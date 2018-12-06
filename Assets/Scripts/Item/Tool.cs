using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tool  {
    //将wav字节数组转为AudioClip
    public static AudioClip FromWavData(byte[] data)
    {
        WAV wav = new WAV(data);
        AudioClip audioClip = AudioClip.Create("wavclip", wav.SampleCount, 1, wav.Frequency, false);
        audioClip.SetData(wav.LeftChannel, 0);
        return audioClip;
    }

    public static AudioClip FromMp3Data(byte[] data)
    {
        // Load the data into a stream
        System.IO.MemoryStream mp3stream = new MemoryStream(data);
        // Convert the data in the stream to WAV format
        Mp3FileReader mp3audio = new Mp3FileReader(mp3stream);

        WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(mp3audio);
        // Convert to WAV data
        WAV wav = new WAV(AudioMemStream(waveStream).ToArray());
        //Debug.Log(wav);
        AudioClip audioClip = AudioClip.Create("testSound", wav.SampleCount, 1, wav.Frequency, false);
        audioClip.SetData(wav.LeftChannel, 0);
        // Return the clip
        return audioClip;
    }

    private static MemoryStream AudioMemStream(WaveStream waveStream)
    {
        MemoryStream outputStream = new MemoryStream();
        using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, waveStream.WaveFormat))
        {
            byte[] bytes = new byte[waveStream.Length];
            waveStream.Position = 0;
            waveStream.Read(bytes, 0, Convert.ToInt32(waveStream.Length));
            waveFileWriter.Write(bytes, 0, bytes.Length);
            waveFileWriter.Flush();
        }
        return outputStream;
    }
  


}
