using UnityEngine;
using System.Collections;
using System;

public class MusicManager : MonoBehaviour {

    private Hashtable sounds = new Hashtable();

    private void Add(string key, AudioClip value)
    {
        if (this.sounds[key] != null || value == null)
        {
            return;
        }

        this.sounds.Add(key, value);
    }

    private AudioClip Get(string key)
    {
        if (this.sounds[key] == null)
        {
            return null;
        }

        return this.sounds[key] as AudioClip;
    }

    public AudioClip LoadAudioClip(string path)
    {
        AudioClip audioClip = this.Get(path);
        if (audioClip == null)
        {
            audioClip = (AudioClip)Resources.LoadAssetAtPath(path, typeof(AudioClip));
            this.Add(path, audioClip);
        }

        return audioClip;
    }

    public void PlayBacksound(bool canPlay)
    {
        string backsoundName = io.mapManager.map.backsoundName;
        if (base.audio.clip != null && backsoundName.IndexOf(base.audio.clip.name) > -1)
        {
            if (!canPlay)
            {
                base.audio.Stop();
                base.audio.clip = null;
                Util.ClearMemory();
            }
            return;
        }

        if (canPlay)
        {
            base.audio.loop = true;
            base.audio.clip = this.LoadAudioClip(backsoundName);
            base.audio.Play();
        }
        else
        {
            base.audio.Stop();
            base.audio.clip = null;
            Util.ClearMemory();
        }
    }

    public void PlayBackSound(string name, bool canPlay)
    {
        if (base.audio.clip != null && name.IndexOf(base.audio.clip.name) > -1)
        {
            if (!canPlay)
            {
                base.audio.Stop();
                base.audio.clip = null;
                Util.ClearMemory();
            }
            return;
        }
        if (canPlay)
        {
            base.audio.loop = true;
            base.audio.clip = this.LoadAudioClip(name);
            base.audio.Play();
        }
        else
        {
            base.audio.Stop();
            base.audio.clip = null;
            Util.ClearMemory();
        }
    }

    public void Play(SoundType type, string name)
    {
        string path = string.Empty;
        switch (type)
        {
            case SoundType.GUI:
                path = Const.GuiSoundAssetDir + name;
                break;
            case SoundType.Weapon:
                path = Const.WeaponSoundAssetDir + name;
                break;
            case SoundType.Particle:
                path = Const.ParticleSoundAssetDir + name;
                break;
        }

        AudioClip clip = this.LoadAudioClip(path);
        this.Play(clip, Vector3.zero);
    }

    public void Play(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
