using System;
using System.Windows.Forms;

public class PlaybackController
{
    private const string PAUSE_ICON = "⏸";
    private const string PLAY_ICON = "⏵";

    private readonly Button playPauseButton;
    private readonly Button nextFrameButton;
    private readonly Button lastFrameButton;
    private PlaybackState state;

    public event EventHandler? NextFrameRequested;
    public event EventHandler? LastFrameRequested;

    public enum PlaybackState
    {
        Loading,
        Ready,
        Playing,
        Paused,
        Finished
    }

    public PlaybackController(Button playPause, Button next, Button last)
    {
        playPauseButton = playPause;
        nextFrameButton = next;
        lastFrameButton = last;

        nextFrameButton.Click += (s, e) => NextFrameRequested?.Invoke(this, e);
        lastFrameButton.Click += (s, e) => LastFrameRequested?.Invoke(this, e);

        UpdateControlsState();
    }

    public PlaybackState State
    {
        get => state;
        set
        {
            state = value;
            UpdateControlsState();
        }
    }

    private void UpdateControlsState()
    {
        switch (State)
        {
            case PlaybackState.Loading:
                playPauseButton.Text = PLAY_ICON;
                playPauseButton.Enabled = nextFrameButton.Enabled = lastFrameButton.Enabled = false;
                break;
            case PlaybackState.Ready:
                playPauseButton.Text = PLAY_ICON;
                playPauseButton.Enabled = true;
                nextFrameButton.Enabled = lastFrameButton.Enabled = false;
                break;
            case PlaybackState.Playing:
                playPauseButton.Text = PAUSE_ICON;
                playPauseButton.Enabled = true;
                nextFrameButton.Enabled = lastFrameButton.Enabled = false;
                break;
            case PlaybackState.Paused:
            case PlaybackState.Finished:
                playPauseButton.Text = PLAY_ICON;
                playPauseButton.Enabled = nextFrameButton.Enabled = lastFrameButton.Enabled = true;
                break;
        }
    }

    public void TogglePlayPause() => 
        State = (State == PlaybackState.Playing) ? PlaybackState.Paused : PlaybackState.Playing;
} 