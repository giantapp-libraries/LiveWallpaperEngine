﻿using System;
using System.Windows.Forms;
using System.Reflection;
using LiveWallpaperEngine.Common.Renders;

namespace LiveWallpaperEngineRender.Forms
{
    public partial class VideoControl : UserControl, IRenderControl
    {
        public VideoControl()
        {
            InitializeComponent();
        }

        private Mpv.NET.Player.MpvPlayer _player;
        private string _lastPath;

        public void InitRender()
        {
            var assembly = Assembly.GetEntryAssembly();
            string appDir = System.IO.Path.GetDirectoryName(assembly.Location);
            string dllPath = $@"{appDir}\lib\mpv-1.dll";

            //单元测试
            _player = new Mpv.NET.Player.MpvPlayer(Handle, dllPath)
            {
                Loop = true,
                Volume = 0
            };
            //防止视频黑边
            _player.API.SetPropertyString("panscan", "1.0");
            _player.AutoPlay = true;
            Load(_lastPath);
        }

        public new void Load(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            _lastPath = path;

            _player?.Pause();
            _player?.Load(path);
            _player?.Resume();
        }

        public void Stop()
        {
            _player?.Stop();
        }

        public void DisposeRender()
        {
            _player?.Dispose();
            _player = null;
        }

        public void Pause()
        {
            _player?.Pause();
        }

        public void Resum()
        {
            _player?.Resume();
        }

        public void SetVolume(int v)
        {
            throw new NotImplementedException();
        }
    }
}