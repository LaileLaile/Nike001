
﻿using UnityEngine;
using System.Collections.Generic;
using System;


public class Timer
{
    // 延迟时间
    private float _delay;

    // 已经过去的时间
    private float _elapsedTime = 0f;

    // 延迟事件
    private Action _action;

    // 计时器标记
    private bool _end = false;

    // 是否重复
    private bool _repeat = false;

    public bool end { get { return _end; } }

    public Timer(float delay, Action act, bool repeat = false)
    {
        _delay = delay;
        _action = act;
        _repeat = repeat;
    }
    public void Update(float dt)
    {
        if (_elapsedTime >= _delay)
        {

            if (_repeat)
            {
                _action();
                _elapsedTime = _elapsedTime - _delay;
            }
            else
            {
                _end = true;
                _action();
                // Globaldata.Global.eventmanger.AddAction(_action);
            }
        }
        _elapsedTime += dt;
    }
    public void LightUpdate(float dt)
    {
        if (Globaldata.Global.second >= _delay)
        {
            // Debug.Log(Globaldata.Global.second+"             "+  _delay);
            if (_repeat)
            {
                _action();

            }
            else
            {
                _end = true;
                _action();
                //Globaldata.Global.eventmanger.AddAction(_action);
            }
        }
        //_elapsedTime += dt;
    }

    public void PauseUpdate(float dt)
    {
        if (Globaldata.Global.second >= _delay)
        {
            // Debug.Log(Globaldata.Global.second+"             "+  _delay);
            if (_repeat)
            {
                _action();

            }
            else
            {
                _end = true;
                _action();
                Globaldata.Global.Current += 1;
                //Globaldata.Global.eventmanger.AddAction(_action);
            }
        }
        //_elapsedTime += dt;
    }
}


public class VideoTimer
{
    public float index;

    public Timer timer;
    public VideoTimer(float num, Timer time)
    {
        index = num;
        timer = time;
    }
}

public class TimerManager
{

    private static TimerManager manager;

    private List<Timer> _timers = new List<Timer>();
    private List<Timer> _light_timers = new List<Timer>();

    private List<VideoTimer> _video_times = new List<VideoTimer>();

    private List<Timer> _pause_timers = new List<Timer>();

    
    public static TimerManager Manager
    {
        get
        {
            if (manager == null)
            {
                manager = new TimerManager();
            }
            return manager;
        }


    }



    public void Invoke(float delay, Action act, bool repeat = false)
    {
        _timers.Add(new Timer(delay, act, repeat));
    }


    public void LightInvoke(float delay, Action act, bool repeat = false)
    {
        _light_timers.Add(new Timer(delay, act, repeat));
    }
    public void VideoInvoke(float delay, Action act, float key, bool repeat = false)
    {
        _video_times.Add(new VideoTimer(key, new Timer(delay, act, repeat)));
    }
    public void PauseInvoke(float delay, Action act, bool repeat = false)
    {
        _pause_timers.Add(new Timer(delay, act, repeat));
    }
    public void Update(float dt)
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            Timer timer = _timers[i];
            if (timer.end)


                _timers.Remove(timer);
            else
                timer.Update(dt);
        }
        // Debug.Log("运行中");
    }
    public void LightUpdate(float dt)
    {
        for (int i = 0; i < _light_timers.Count; i++)
        {
            Timer timer = _light_timers[i];
            if (timer.end)
                _light_timers.Remove(timer);
            else
                timer.LightUpdate(dt);
        }
    }

    public void VideoUpdate(float dt)
    {


        for (int i = 0; i < _video_times.Count; i++)
        {
            VideoTimer timer = _video_times[i];
            if (timer.timer.end)
            {
                _video_times.Remove(timer);
            }
            else
            {
                timer.timer.Update(dt);
            }
        }
    }

    public void RemoveVideo(int index)
    {

        for (int i = 0; i < _video_times.Count; i++)
        {
            if (index == _video_times[i].index)
            {
                _video_times.Remove(_video_times[i]);
            }
        }
    }

    public void PauseUpdate(float dt)
    {
        for (int i = 0; i < _pause_timers.Count; i++)
        {
            Timer timer = _pause_timers[i];
            if (timer.end)
                _pause_timers.Remove(timer);
            else
                timer.PauseUpdate(dt);
        }
    }

    public void Clear()
    {
        _light_timers.Clear();
        _timers.Clear();
        _video_times.Clear();
        _pause_timers.Clear();
    }

    public void ResultPause()
    {
        _pause_timers.Clear();
        _light_timers.Clear();
        _pause_timers.Clear();
    }



}